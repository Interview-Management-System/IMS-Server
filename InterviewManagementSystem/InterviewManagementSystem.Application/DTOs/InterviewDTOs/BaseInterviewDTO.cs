namespace InterviewManagementSystem.Application.DTOs.InterviewDTOs
{
    public abstract record BaseInterviewDTO
    {
        public string? Title { get; set; }
        public DateTime? ScheduleTime { get; set; }
        public HourPeriod? HourPeriod { get; set; }
        public InterviewResult? InterviewResult { get; set; } = new();
        public InterviewStatus? InterviewStatus { get; set; } = new();

    }


    public sealed record InterviewResult
    {
        public InterviewResultEnum InterviewResultId { get; set; }
        public string? InterviewResultText => InterviewResultId.GetEnumName();
    }


    public sealed record InterviewStatus
    {
        public InterviewStatusEnum InterviewScheduleStatusId { get; set; }
        public string? InterviewStatusText => InterviewScheduleStatusId.GetEnumName();
    }


    public sealed record HourPeriod
    {
        public TimeOnly? StartHour { get; set; } = default;
        public TimeOnly? EndHour { get; set; } = default;
    }











}
