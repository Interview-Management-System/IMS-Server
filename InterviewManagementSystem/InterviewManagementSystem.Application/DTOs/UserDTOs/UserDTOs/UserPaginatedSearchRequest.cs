using System.Text.Json.Serialization;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs
{
    public sealed record UserPaginatedSearchRequest : PaginatedSearchRequest
    {
        public RoleEnum RoleId { get; set; } = RoleEnum.Default;

        [JsonIgnore]
        public static UserPaginatedSearchRequest DefaultSearchValue => new();
    }
}
