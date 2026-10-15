using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DeliveryService.Infrastructure.Common
{
    /// <summary>
    /// Applies a common route prefix to all controllers in the application.
    /// </summary>
    /// <param name="prefix">The route prefix to prepend, e.g. <c>api/v1</c>.</param>
    public class RoutePrefixConvention(string prefix) : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _prefix = new(new RouteAttribute(prefix));

        /// <summary>
        /// Prepends the route prefix to every controller selector route.
        /// </summary>
        /// <param name="application">The application model whose controllers are updated.</param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel != null)
                    {
                        selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_prefix, selector.AttributeRouteModel);
                    }
                    else
                    {
                        selector.AttributeRouteModel = _prefix;
                    }
                }
            }
        }
    }
}
