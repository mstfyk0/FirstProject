namespace SecondProject.Filters
{
    public class PaginationFilter
    {
        private const int MaxPageSize = 50;
        public int PageNumber {  get; set; } 
        public int PageSize { get; set; }

        public PaginationFilter() { 
            
            PageSize = MaxPageSize;
            PageNumber = 1;

        }
        public PaginationFilter(int pageNumber,int pageSize)
        {
            PageNumber = pageNumber <1 ? 1 : pageNumber;
            PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;

        }

    }
}
