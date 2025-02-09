namespace InterviewManagementSystem.Application.DTOs
{
    public sealed record AuditInformation
    {
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
