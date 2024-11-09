using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.CustomClasses.OfferData;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Application.Features.OfferFeature.UseCases;

public sealed class OfferUpdateUseCase : BaseUseCase
{


    public OfferUpdateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    internal async Task<string> ChangeOfferStatusAsync(Guid offerId, OfferStatusEnum offerStatusId)
    {
        Offer? offerFoundById = await _unitOfWork.OfferRepository.GetByIdAsync(offerId, true);


        ArgumentNullException.ThrowIfNull(offerFoundById, "Offer not found to change status");
        ApplicationException.ThrowIfGetDeletedRecord(offerFoundById.IsDeleted);


        offerFoundById.SetStatus(offerStatusId);


        bool changeStatusSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(changeStatusSuccess, "Change status fail");


        return "Change status successfully";
    }



    internal async Task<string> UpdateOfferAsync(OfferForUpdateDTO offerForUpdateDTO)
    {

        Offer? offerFoundById = await _unitOfWork.OfferRepository
            .GetWithInclude(o => o.Id == offerForUpdateDTO.Id, includeProperties: [nameof(Offer.CandidateOfferStatus)], isTracking: true)
            .SingleOrDefaultAsync();


        ArgumentNullException.ThrowIfNull(offerFoundById, "Offer not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(offerFoundById.IsDeleted);



        var candidate = await _unitOfWork.GetBaseRepository<Candidate>()
                    .GetWithInclude(c => c.Id == offerForUpdateDTO.CandidateId, isTracking: true)
                    .SingleOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(candidate, "Candidate not found");




        var interviewSchedule = await _unitOfWork
            .GetBaseRepository<InterviewSchedule>()
            .GetByIdAsync(offerForUpdateDTO.InterviewScheduleId, true);


        ArgumentNullException.ThrowIfNull(interviewSchedule, "Interview schedule not found");



        DataForUpdateOffer dataForUpdateOffer = _mapper.Map<DataForUpdateOffer>(offerForUpdateDTO, opts =>
        {
            opts.Items[nameof(Candidate)] = candidate;
            opts.Items[nameof(InterviewSchedule)] = interviewSchedule;
        });

        Offer.Update(offerFoundById, dataForUpdateOffer);


        bool changeStatusSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(changeStatusSuccess, "Update fail");


        return "Update successfully";
    }
}
