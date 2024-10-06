using InterviewManagementSystem.Application.CustomClasses.Helpers;
using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.OfferFeature.UseCases;

public sealed class OfferRetrieveUseCase : BaseUseCase
{
    public OfferRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<ApiResponse<PageResult<OfferForRetrieveDTO>>> GetListJobPagingAsync(PaginationRequest paginationRequest)
    {


        var filters = FilterHelper.BuildFilters<Offer>(paginationRequest, nameof(Offer.Candidate.UserName));


        PaginationParameter<Offer> paginationParameter = _mapper.Map<PaginationParameter<Offer>>(paginationRequest);
        paginationParameter.Filters = filters;



        string[] includeProperties = [nameof(Offer.Candidate), nameof(Offer.Approver)];
        var pageResult = await _unitOfWork.OfferRepository.GetByPageWithIncludeAsync(paginationParameter, includeProperties);



        return new ApiResponse<PageResult<OfferForRetrieveDTO>>
        {
            Message = pageResult.Items.Count > 0 ? "List job found" : "No jobs found",
            Data = _mapper.Map<PageResult<OfferForRetrieveDTO>>(pageResult)
        };
    }

}
