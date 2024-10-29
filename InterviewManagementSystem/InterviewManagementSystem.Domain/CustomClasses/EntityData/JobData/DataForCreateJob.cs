namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.JobData
{
    public struct DataForCreateJob
    {
        public string? Title { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal From { get; private set; }
        public decimal To { get; private set; }
        public string? WorkingAddress { get; private set; }
        public string? Description { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<Level> Levels { get; private set; }
        public List<Benefit> Benefits { get; private set; }
        public bool IsSaveAsDraft { get; private set; }
    }

}
