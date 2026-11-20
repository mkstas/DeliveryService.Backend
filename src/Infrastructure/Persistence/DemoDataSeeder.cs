using Application.Common;
using Domain.Abstractions.Permissions;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    internal static class DemoDataSeeder
    {
        public static async Task SeedAsync(PersistenceDbContext context)
        {
            if (await context.Restaurants.AnyAsync()
                || await context.Categories.AnyAsync()
                || await context.Specifications.AnyAsync()
                || await context.Users.AnyAsync(u => u.EmailAddress.StartsWith(DemoDataCatalog.UserEmailPrefix)))
            {
                return;
            }

            var clientRole = await context.Roles.FirstAsync(r => r.Name == RoleNames.Client);
            var managerRole = await context.Roles.FirstAsync(r => r.Name == RoleNames.Manager);
            var passwordHash = PasswordHasher.Generate(DemoDataCatalog.UserPassword);

            var restaurants = DemoDataCatalog.RestaurantNames
                .Select(name => Restaurant.Create(name))
                .ToList();
            context.Restaurants.AddRange(restaurants);

            for (var i = 0; i < DemoDataCatalog.UserNames.Count; i++)
            {
                var user = User.Create(
                    DemoDataCatalog.GetUserEmail(i),
                    passwordHash,
                    DemoDataCatalog.UserNames[i]);
                user.AddRole(i % 4 == 0 ? managerRole : clientRole);
                restaurants[i % restaurants.Count].AddUser(user);
                context.Users.Add(user);
            }

            var categories = DemoDataCatalog.CategoryNames
                .Select(name => Category.Create(name))
                .ToList();
            var specifications = DemoDataCatalog.SpecificationNames
                .Select(name => Specification.Create(name))
                .ToList();
            context.Categories.AddRange(categories);
            context.Specifications.AddRange(specifications);

            await context.SaveChangesAsync();

            for (var i = 0; i < restaurants.Count; i++)
            {
                foreach (var ingredientName in DemoDataCatalog.GetIngredientNames(i))
                {
                    restaurants[i].AddIngredient(ingredientName);
                }
            }

            await context.SaveChangesAsync();

            var dishes = new List<Dish>();

            for (var restaurantIndex = 0; restaurantIndex < restaurants.Count; restaurantIndex++)
            {
                var ingredients = restaurants[restaurantIndex].Ingredients.ToList();

                for (var dishIndex = 0; dishIndex < DemoDataCatalog.Dishes.Count; dishIndex++)
                {
                    var template = DemoDataCatalog.Dishes[dishIndex];
                    var dish = Dish.Create(
                        restaurants[restaurantIndex].Id,
                        categories[template.CategoryIndex].Id,
                        template.Name,
                        template.Price + restaurantIndex * 10m,
                        template.Description);

                    for (var offset = 0; offset < DemoDataCatalog.SpecificationsPerDish; offset++)
                    {
                        var specification = specifications[(dishIndex + restaurantIndex + offset) % specifications.Count];
                        var values = DemoDataCatalog.SpecificationValues[specification.Name];
                        dish.AddSpecification(DishSpecification.Create(
                            dish.Id,
                            specification.Id,
                            values[(dishIndex + restaurantIndex) % values.Count]));
                    }

                    var usedIngredientIds = new HashSet<Guid>();

                    for (var offset = 0; offset < DemoDataCatalog.IngredientsPerDish; offset++)
                    {
                        var ingredient = ingredients[
                            (dishIndex * 2 + offset * 3 + restaurantIndex) % ingredients.Count];

                        if (usedIngredientIds.Add(ingredient.Id))
                        {
                            dish.AddIngredient(DishIngredient.Create(dish.Id, ingredient.Id));
                        }
                    }

                    dishes.Add(dish);
                }
            }

            context.Dishes.AddRange(dishes);
            await context.SaveChangesAsync();
        }
    }
}
