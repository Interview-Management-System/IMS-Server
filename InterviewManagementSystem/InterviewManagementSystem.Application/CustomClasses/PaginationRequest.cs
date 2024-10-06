namespace InterviewManagementSystem.Application.CustomClasses
{
    public struct PaginationRequest
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// For searching by many types of enum (statusId, roleId)
        /// </summary>
        public Dictionary<string, Enum> EnumsToFilter { get; set; } = [];


        /// <summary>
        /// For searching by multiple name (user name, skill name,..) at the same time
        /// </summary>
        public Dictionary<string, string?> FieldNamesToSearch { get; set; } = [];


        public PaginationRequest()
        {
        }
    }
}
