using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;

namespace InterviewManagementSystem.Application.Features.OfferFeature.UseCases;

public sealed class OfferCreateUseCase : BaseUseCase
{

    public OfferCreateUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }




    public async Task<string> CreateOfferAsync(OfferForCreateDTO offerForCreateDTO)
    {

        var offer = _mapper.Map<Offer>(offerForCreateDTO);

        await _unitOfWork.OfferRepository.AddAsync(offer);
        bool createSuccess = await _unitOfWork.SaveChangesAsync();

        ApplicationException.ThrowIfOperationFail(createSuccess, "Create offer failed");

        return "Offer created successfully";
    }
}
