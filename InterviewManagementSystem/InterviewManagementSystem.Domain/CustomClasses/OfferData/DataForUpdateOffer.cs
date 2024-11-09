namespace InterviewManagementSystem.Domain.CustomClasses.OfferData
{
    public struct DataForUpdateOffer : IBaseOfferData
    {
        public string? Note { get; set; }
        public short LevelId { get; set; }
        public Guid ApproverId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime DueDate { get; set; }
        public short PositionId { get; set; }
        public short DepartmentId { get; set; }
        public decimal BasicSalary { get; set; }
        public short ContractTypeId { get; set; }
        public Guid RecruiterOwnerId { get; set; }
        public DatePeriod DatePeriod { get; set; }
        public Guid InterviewScheduleId { get; set; }
        public Candidate AssociatedCandidate { get; set; }
        public InterviewSchedule AssociatedInterviewSchedule { get; set; }
    }
}
