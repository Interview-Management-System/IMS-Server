namespace InterviewManagementSystem.Domain.Aggregates
{
    internal class BaseJobAggregate
    {
        public string? Title { get; set; }

        public string? WorkingAddress { get; set; }

        public string? Description { get; set; }

        public SalaryRange? SalaryRange { get; set; }

        public DatePeriod? DatePeriod { get; set; }

        public short? JobStatusId { get; set; }


        public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

        public virtual ICollection<Level> Levels { get; set; } = new List<Level>();

        public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

        public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

        public virtual ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
    }
}
