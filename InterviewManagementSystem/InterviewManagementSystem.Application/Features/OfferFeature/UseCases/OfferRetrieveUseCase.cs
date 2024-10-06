using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Application.Features.OfferFeature.UseCases;

public sealed class OfferRetrieveUseCase : BaseUseCase
{


    public OfferRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    internal async Task<ApiResponse<PageResult<OfferForRetrieveDTO>>> GetListOfferPagingAsync(PaginationRequest paginationRequest)
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




    internal async Task<ApiResponse<OfferForDetailRetrieveDTO>> GetOfferDetailByIdAsync(Guid id)
    {

        Expression<Func<IQueryable<Offer>, IIncludableQueryable<Offer, object>>>[] includes =
            [
                q => q.Include(offer => offer.Candidate)!,
                q => q.Include(offer => offer.Approver)!,
                q => q.Include(offer => offer.RecruiterOwner)!,
                q => q.Include(offer => offer.UpdatedByNavigation)!,
                q => q.Include(offer => offer.InterviewSchedule)!.ThenInclude(i => i!.Interviewers),
            ];


        var offerFoundById = await _unitOfWork.OfferRepository
           .GetWithInclude(offer => offer.Id.Equals(id), includes)
           .FirstOrDefaultAsync();

        ArgumentNullException.ThrowIfNull(offerFoundById, "Offer not found");



        return new ApiResponse<OfferForDetailRetrieveDTO>
        {
            Message = "Offer found",
            Data = _mapper.Map<OfferForDetailRetrieveDTO>(offerFoundById)
        };
    }
}
