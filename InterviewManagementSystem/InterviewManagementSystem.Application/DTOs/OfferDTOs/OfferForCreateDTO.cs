namespace InterviewManagementSystem.Application.DTOs.OfferDTOs
{
    public record OfferForCreateDTO
    {
        public string? Note { get; set; }
        public LevelEnum LevelId { get; set; }
        public Guid ApproverId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime DueDate { get; set; }
        public PositionEnum PositionId { get; set; }
        public DepartmentEnum DepartmentId { get; set; }
        public DateTime ContractTo { get; set; }
        public decimal BasicSalary { get; set; }
        public ContractTypeEnum ContractTypeId { get; set; }
        public DateTime ContractFrom { get; set; }
        public Guid RecruiterOwnerId { get; set; }
        public Guid InterviewScheduleId { get; set; }

    }
}
