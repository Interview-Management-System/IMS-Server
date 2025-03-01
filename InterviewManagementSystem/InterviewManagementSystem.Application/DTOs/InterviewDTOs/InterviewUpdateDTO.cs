namespace InterviewManagementSystem.Application.DTOs.InterviewDTOs
{
    public sealed record InterviewUpdateDTO
    {
    }


    public sealed record InterviewSubmitResultDTO
    {
        public Guid Id { get; set; }
        public InterviewResultEnum InterviewResultId { get; set; }
    }
}
