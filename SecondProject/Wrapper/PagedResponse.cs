namespace SecondProject.Wrapper
{
    public class PagedResponse<T> : Response<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool NextPage { get; set; }
        public bool PreviousPage { get; set; }

        public PagedResponse(T data  , int pageNumber , int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            success = true;

        }

    }
}
