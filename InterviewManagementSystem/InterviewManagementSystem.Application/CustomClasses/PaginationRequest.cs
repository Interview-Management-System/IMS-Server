namespace InterviewManagementSystem.Application.CustomClasses
{
    public struct PaginationRequest<TEnum> where TEnum : Enum
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public TEnum? EnumToFilter { get; set; }
        public string? SearchName { get; set; } = null;


        public PaginationRequest()
        {
        }
    }
}
