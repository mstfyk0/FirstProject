using SecondProject.Filters;
using SecondProject.Wrapper;

namespace SecondProject.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData, PaginationFilter validPagingFilter , int totalRecords)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, validPagingFilter.PageNumber, validPagingFilter.PageSize);
            var totalPagers = ((double)totalRecords / (double)validPagingFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPagers));
            var currentPage = validPagingFilter.PageNumber;

            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            response.PreviousPage = currentPage >1 ? true : false;
            response.NextPage=currentPage < roundedTotalPages ? true : false;    
            return response;

        }


    }
}
