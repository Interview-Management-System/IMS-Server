namespace InterviewManagementSystem.Application.DTOs.JobDTOs
{
    public sealed record JobForRetrieveDTO : BaseJobDTO
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? CreateAt { get; set; }
        public string? UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public new string? EndDate { get; set; }
        public string[] Levels { get; set; } = [];
        public new string? StartDate { get; set; }
        public string[] Benefits { get; set; } = [];
        public string[] RequiredSkills { get; set; } = [];
    }
}
