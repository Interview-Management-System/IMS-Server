namespace InterviewManagementSystem.Domain.Paginations
{
    public sealed class PageResult<T>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

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
