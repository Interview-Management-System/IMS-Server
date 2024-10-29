using InterviewManagementSystem.Application.DTOs.OfferDTOs;
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

        Offer? offerFoundById = await _unitOfWork.OfferRepository.GetByIdAsync(offerForUpdateDTO.Id);


        ArgumentNullException.ThrowIfNull(offerFoundById, "Offer not found to update");
        ApplicationException.ThrowIfGetDeletedRecord(offerFoundById.IsDeleted);


        offerFoundById = _mapper.Map<Offer>(offerForUpdateDTO);
        _unitOfWork.OfferRepository.Update(offerFoundById);


        bool changeStatusSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(changeStatusSuccess, "Update fail");


        return "Update successfully";
    }
}
