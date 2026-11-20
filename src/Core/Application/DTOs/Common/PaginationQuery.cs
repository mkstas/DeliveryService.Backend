using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Common
{
    public sealed class PaginationQuery
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 1000)]
        public int PageSize { get; set; } = 20;
    }
}
