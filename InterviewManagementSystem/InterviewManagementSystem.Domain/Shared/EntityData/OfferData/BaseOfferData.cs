using InterviewManagementSystem.Domain.Enums;

namespace InterviewManagementSystem.Domain.Shared.EntityData.OfferData;
/*
public interface IBaseOfferData
{
    public string? Note { get; protected set; }
    public LevelEnum LevelId { get; protected set; }
    public Guid ApproverId { get; protected set; }
    public Guid CandidateId { get; protected set; }
    public DateTime DueDate { get; protected set; }
    public PositionEnum PositionId { get; protected set; }
    public DepartmentEnum DepartmentId { get; protected set; }
    public decimal BasicSalary { get; protected set; }
    public ContractTypeEnum ContractTypeId { get; protected set; }
    public Guid RecruiterOwnerId { get; protected set; }
    public DatePeriod DatePeriod { get; protected set; }
    public Guid InterviewScheduleId { get; protected set; }
    public Candidate AssociatedCandidate { get; protected set; }
    public InterviewSchedule AssociatedInterviewSchedule { get; protected set; }
}
*/

public record BaseOfferData
{
    public string? Note { get; protected set; }
    public LevelEnum LevelId { get; protected set; }
    public Guid ApproverId { get; protected set; }
    public Guid CandidateId { get; protected set; }
    public DateTime DueDate { get; protected set; }
    public PositionEnum PositionId { get; protected set; }
    public DepartmentEnum DepartmentId { get; protected set; }
    public decimal BasicSalary { get; protected set; }
    public ContractTypeEnum ContractTypeId { get; protected set; }
    public Guid RecruiterOwnerId { get; protected set; }
    public DatePeriod DatePeriod { get; protected set; } = default!;
    public Guid InterviewScheduleId { get; protected set; }
    public Candidate AssociatedCandidate { get; protected set; } = default!;
    public InterviewSchedule AssociatedInterviewSchedule { get; protected set; } = default!;
}