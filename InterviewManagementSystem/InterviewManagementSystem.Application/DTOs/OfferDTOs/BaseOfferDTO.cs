namespace InterviewManagementSystem.Application.DTOs.OfferDTOs
{
    public abstract record BaseOfferDTO
    {
        public string? Note { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? Approver { get; set; }
        public string? Department { get; set; }
        public string? CandidateName { get; set; }
    }
}
