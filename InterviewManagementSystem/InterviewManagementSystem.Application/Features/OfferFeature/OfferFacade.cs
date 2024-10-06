using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature.UseCases;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.OfferFeature;

public sealed class OfferFacade
{


    private readonly OfferCreateUseCase _offerCreateUseCase;
    private readonly OfferRetrieveUseCase _offerRetrieveUseCase;
    private readonly OfferUpdateUseCase _offerUpdateUseCase;



    public OfferFacade(OfferRetrieveUseCase offerRetrieveUseCase, OfferCreateUseCase offerCreateUseCase, OfferUpdateUseCase offerUpdateUseCase)
    {
        _offerRetrieveUseCase = offerRetrieveUseCase;
        _offerCreateUseCase = offerCreateUseCase;
        _offerUpdateUseCase = offerUpdateUseCase;
    }



    public async Task<ApiResponse<PageResult<OfferForRetrieveDTO>>> GetListOfferPagingAsync(PaginationRequest paginationRequest)
    {
        return await _offerRetrieveUseCase.GetListOfferPagingAsync(paginationRequest);
    }



    public async Task<string> CreateOfferAsync(OfferForCreateDTO offerForCreateDTO)
    {
        return await _offerCreateUseCase.CreateOfferAsync(offerForCreateDTO);
    }



    public async Task<ApiResponse<OfferForDetailRetrieveDTO>> GetOfferDetailByIdAsync(Guid id)
    {
        return await _offerRetrieveUseCase.GetOfferDetailByIdAsync(id);
    }



    public async Task<string> ChangeOfferStatusAsync(Guid id, OfferStatusEnum offerStatusEnum)
    {
        return await _offerUpdateUseCase.ChangeOfferStatusAsync(id, offerStatusEnum);
    }



    public async Task<string> UpdateOfferAsync(OfferForUpdateDTO offerForUpdateDTO)
    {
        return await _offerUpdateUseCase.UpdateOfferAsync(offerForUpdateDTO);
    }
}
