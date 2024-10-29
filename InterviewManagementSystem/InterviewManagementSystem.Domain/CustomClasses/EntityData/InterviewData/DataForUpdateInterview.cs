namespace InterviewManagementSystem.Domain.CustomClasses.EntityData.InterviewData
{
    public struct DataForUpdateInterview
    {
        public string? Title { get; private set; }
        public Guid? CandidateId { get; private set; }
        public DateTime ScheduleTime { get; private set; }
        public string? Note { get; private set; }
        public string? Location { get; private set; }
        public Guid? JobId { get; private set; }
        public string? MeetingUrl { get; private set; }
        public Guid? RecruiterOwnerId { get; private set; }
        public string EndHourString { get; private set; }
        public string StartHourString { get; private set; }
        public short CandidateStatusId { get; set; }
        public List<AppUser> Interviews { get; set; }
    }
}
