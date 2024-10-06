namespace InterviewManagementSystem.Application.DTOs.OfferDTOs
{
    public sealed record OfferForUpdateDTO : OfferForCreateDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
