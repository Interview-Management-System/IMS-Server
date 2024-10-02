namespace InterviewManagementSystem.Application.DTOs.OfferDTOs
{
    public sealed record OfferForDetailRetrieveDTO : OfferForRetrieveDTO
    {
        public string? Level { get; set; }
        public string? DueDate { get; set; }
        public string? Position { get; set; }
        public string? ContractTo { get; set; }
        public string? ContractFrom { get; set; }
        public decimal BasicSalary { get; set; }
        public string? ContractType { get; set; }
        public string? InterviewNote { get; set; }
        public string? RecruiterOwner { get; set; }
        public string? InterviewTitle { get; set; }
        public List<string> Interviewers { get; set; } = [];
    }

}
