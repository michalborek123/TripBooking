namespace TripBooking.WebApp.Helpers
{
    internal class PagingInfo
    {
        public int PageSize { get; set; }
        public int PageNumber { get; }

        public PagingInfo(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
