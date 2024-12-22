using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OfferController(OfferFacade offerFacade) : ControllerBase
{


    [HttpPost("list-paging")]
    public async Task<IActionResult> GetListOfferPagingAsync(OfferPaginatedSearchRequest paginatedSearchRequest)
    {
        var apiResponse = await offerFacade.GetListOfferPagingAsync(paginatedSearchRequest);
        return Ok(apiResponse);

    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateOfferAsync([FromBody] OfferForCreateDTO offerForCreateDTO)
    {
        var apiResponse = await offerFacade.CreateOfferAsync(offerForCreateDTO);
        return Created("", apiResponse);
    }



    [HttpGet("detail")]
    public async Task<IActionResult> GetOfferDetailAsync([FromQuery] Guid id)
    {
        var apiResponse = await offerFacade.GetOfferDetailByIdAsync(id);
        return Ok(apiResponse);
    }



    [HttpPatch("change-status")]
    public async Task<IActionResult> ChangeOfferStatusAsync(Guid offerId, OfferStatusEnum offerStatusId)
    {
        var apiResponse = await offerFacade.ChangeOfferStatusAsync(offerId, offerStatusId);
        return Ok(apiResponse);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateOfferAsync(OfferForUpdateDTO offerForUpdateDTO)
    {
        var apiResponse = await offerFacade.UpdateOfferAsync(offerForUpdateDTO);
        return Ok(apiResponse);
    }
}
