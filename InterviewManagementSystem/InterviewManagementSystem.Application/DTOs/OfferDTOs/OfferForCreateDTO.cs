namespace InterviewManagementSystem.Application.DTOs.OfferDTOs
{
    public sealed record OfferForCreateDTO
    {
        public string? Note { get; set; }
        public short LevelId { get; set; }
        public Guid ApproverId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime DueDate { get; set; }
        public short PositionId { get; set; }
        public short DepartmentId { get; set; }
        public DateTime ContractTo { get; set; }
        public decimal BasicSalary { get; set; }
        public short ContractTypeId { get; set; }
        public DateTime ContractFrom { get; set; }
        public Guid RecruiterOwnerId { get; set; }
        public Guid InterviewScheduleId { get; set; }

    }
}
