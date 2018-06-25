using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Models
{
    public class PaginatedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int PagesLeft { get => TotalPages - Page;}
        public bool HasNext { get => Page < TotalPages; }
        public bool HasPrevious { get => Page > 1; }
        public List<T> Items { get; set; }

        public PaginatedResponse(int page, int pageSize, int totalPages, List<T> items)
        {
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
            Items = items;
        }
    }
}
