namespace InterviewManagementSystem.Domain.CustomClasses
{
    public struct DataForCreateOffer
    {
        public string? Note { get; private set; }
        public short LevelId { get; private set; }
        public Guid ApproverId { get; private set; }
        public Guid CandidateId { get; private set; }
        public DateTime DueDate { get; private set; }
        public short PositionId { get; private set; }
        public short DepartmentId { get; private set; }
        public decimal BasicSalary { get; private set; }
        public short ContractTypeId { get; private set; }
        public Guid RecruiterOwnerId { get; private set; }
        public DatePeriod DatePeriod { get; private set; }
        public Guid InterviewScheduleId { get; private set; }
        public Candidate AssociatedCandidate { get; set; }
        public InterviewSchedule AssociatedInterviewSchedule { get; set; }
    }
}
