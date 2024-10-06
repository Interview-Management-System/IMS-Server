using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using static InterviewManagementSystem.Application.CustomClasses.Helpers.EntityHelper;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OfferController : ControllerBase
{


    private readonly OfferFacade _offerFacade;


    public OfferController(OfferFacade offerFacade)
    {
        _offerFacade = offerFacade;
    }



    [HttpGet("list-paging")]
    public async Task<IActionResult> GetListOfferPagingAsync(string? searchName, OfferStatusEnum? offerStatusId, DepartmentEnum? departmentId, int pageSize = 5, int pageIndex = 1)
    {

        var pagingRequest = new PaginationRequest()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            EnumsToFilter = EntityEnumMapping.BuildOfferEnumFilter(offerStatusId, departmentId),
            FieldNamesToSearch = EntityPropertyMapping.BuildOfferSearchFieldMapping(searchName)
        };


        var apiResponse = await _offerFacade.GetListOfferPagingAsync(pagingRequest);
        return Ok(apiResponse);

    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateOfferAsync([FromBody] OfferForCreateDTO offerForCreateDTO)
    {
        var apiResponse = await _offerFacade.CreateOfferAsync(offerForCreateDTO);
        return Created("", apiResponse);
    }
}
