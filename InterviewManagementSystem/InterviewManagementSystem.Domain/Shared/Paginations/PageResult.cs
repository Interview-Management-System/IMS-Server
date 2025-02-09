namespace InterviewManagementSystem.Domain.Shared.Paginations
{
    public sealed class PageResult<T>
    {
        public int PageIndex { get; set; } = 1; // Current page (1,2,3)

        public int PageSize { get; set; } = 5;

        public int TotalRecords { get; set; } = 0;

        public List<T> Items { set; get; } = [];

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
