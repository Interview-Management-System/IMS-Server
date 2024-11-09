namespace InterviewManagementSystem.Domain.CustomClasses.OfferData
{
    public interface IBaseOfferData
    {
        public string? Note { get; protected set; }
        public short LevelId { get; protected set; }
        public Guid ApproverId { get; protected set; }
        public Guid CandidateId { get; protected set; }
        public DateTime DueDate { get; protected set; }
        public short PositionId { get; protected set; }
        public short DepartmentId { get; protected set; }
        public decimal BasicSalary { get; protected set; }
        public short ContractTypeId { get; protected set; }
        public Guid RecruiterOwnerId { get; protected set; }
        public DatePeriod DatePeriod { get; protected set; }
        public Guid InterviewScheduleId { get; protected set; }
        public Candidate AssociatedCandidate { get; protected set; }
        public InterviewSchedule AssociatedInterviewSchedule { get; protected set; }
    }
}
