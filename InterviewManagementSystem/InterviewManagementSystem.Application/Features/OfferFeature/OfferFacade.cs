using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature.UseCases;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.OfferFeature;

public sealed class OfferFacade
{


    private readonly OfferRetrieveUseCase _offerRetrieveUseCase;



    public OfferFacade(OfferRetrieveUseCase offerRetrieveUseCase)
    {
        _offerRetrieveUseCase = offerRetrieveUseCase;
    }



    public async Task<ApiResponse<PageResult<JobForRetrieveDTO>>> GetListOfferPagingAsync(PaginationParameter<OfferStatusEnum> paginationParameter)
    {
        return null!;
    }
}
