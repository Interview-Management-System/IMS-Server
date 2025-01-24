using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Managers;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Shared.EntityData.OfferData;

namespace InterviewManagementSystem.Application.Managers.OfferFeature.UseCases;

public sealed class OfferCreateUseCase : BaseManager
{

    public OfferCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<string> CreateOfferAsync(OfferForCreateDTO offerForCreateDTO)
    {

        var candidate = await _unitOfWork.GetBaseRepository<Candidate>()
            .GetWithInclude(c => c.Id == offerForCreateDTO.CandidateId, isTracking: true)
            .SingleOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(candidate, "Candidate not found");



        var interviewSchedule = await _unitOfWork
            .GetBaseRepository<InterviewSchedule>()
            .GetByIdAsync(offerForCreateDTO.InterviewScheduleId, true);

        ArgumentNullException.ThrowIfNull(interviewSchedule, "Interview schedule not found");



        var dataForCreateOffer = _mapper.Map<DataForCreateOffer>(offerForCreateDTO, opts =>
        {
            opts.Items[nameof(Candidate)] = candidate;
            opts.Items[nameof(InterviewSchedule)] = interviewSchedule;
        });



        var newOffer = Offer.Create(dataForCreateOffer);


        await _unitOfWork.OfferRepository.AddAsync(newOffer);
        bool createSuccess = await _unitOfWork.SaveChangesAsync();


        ApplicationException.ThrowIfOperationFail(createSuccess, "Create offer failed");
        return "Offer created successfully";
    }
}
