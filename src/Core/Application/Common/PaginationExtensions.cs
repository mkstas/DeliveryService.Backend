using Application.DTOs.Common;

namespace Application.Common
{
    public static class PaginationExtensions
    {
        public static PagedResponse<T> ToPaged<T>(this IReadOnlyList<T> source, PaginationQuery pagination)
        {
            var page = Math.Max(pagination.Page, 1);
            var pageSize = Math.Clamp(pagination.PageSize, 1, 1000);
            var skip = (long)(page - 1) * pageSize;

            var items = skip > int.MaxValue
                ? []
                : source.Skip((int)skip).Take(pageSize).ToList();

            return new PagedResponse<T>(items, page, pageSize, source.Count);
        }
    }
}
