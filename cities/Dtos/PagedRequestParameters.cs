using System.ComponentModel.DataAnnotations;

namespace cities.Dtos
{
    public abstract class PagedRequestParameters
    {
        const int maxPageSize = 50;
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        [Range(2, maxPageSize)]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        public string? OrderBy { get; set; }
        public string? SearchText { get; set; }
    }
}
