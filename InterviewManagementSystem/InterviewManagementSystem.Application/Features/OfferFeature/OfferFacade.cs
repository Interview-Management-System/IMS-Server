using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature.UseCases;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.OfferFeature;

public sealed class OfferFacade
{


    private readonly OfferRetrieveUseCase _offerRetrieveUseCase;
    private readonly OfferCreateUseCase _offerCreateUseCase;



    public OfferFacade(OfferRetrieveUseCase offerRetrieveUseCase, OfferCreateUseCase offerCreateUseCase)
    {
        _offerRetrieveUseCase = offerRetrieveUseCase;
        _offerCreateUseCase = offerCreateUseCase;
    }



    public async Task<ApiResponse<PageResult<OfferForRetrieveDTO>>> GetListOfferPagingAsync(PaginationRequest paginationRequest)
    {
        return await _offerRetrieveUseCase.GetListJobPagingAsync(paginationRequest);
    }



    public async Task<string> CreateOfferAsync(OfferForCreateDTO offerForCreateDTO)
    {
        return await _offerCreateUseCase.CreateOfferAsync(offerForCreateDTO);
    }
}
