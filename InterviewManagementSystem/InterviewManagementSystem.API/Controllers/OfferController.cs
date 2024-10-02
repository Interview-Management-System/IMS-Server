using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OfferController : ControllerBase
{





    [HttpGet("list-paging")]
    public async Task<IActionResult> GetListOfferPagingAsync(string? candidateName, OfferStatusEnum? statusEnum, int pageSize = 5, int pageIndex = 1)
    {
        var pagingRequest = new PaginationRequest<OfferStatusEnum>()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            SearchName = candidateName,
            EnumToFilter = statusEnum ?? default
        };


        return Ok("");
    }
}
