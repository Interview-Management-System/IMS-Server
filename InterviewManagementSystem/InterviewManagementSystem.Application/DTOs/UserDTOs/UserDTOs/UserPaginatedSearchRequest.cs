namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs
{
    public sealed partial record UserPaginatedSearchRequest : PaginatedSearchRequest
    {
        public RoleEnum RoleId { get; set; } = RoleEnum.Default;


        private static readonly UserPaginatedSearchRequest _defaultSearchValue = new();

        public static UserPaginatedSearchRequest DefaultSearchValue => _defaultSearchValue with { };
    }
}
