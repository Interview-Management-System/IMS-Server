﻿using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Application.DTOs.OfferDTOs;
using InterviewManagementSystem.Application.Features.OfferFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using static InterviewManagementSystem.Application.CustomClasses.Helpers.EntityHelper;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OfferController(OfferFacade offerFacade) : ControllerBase
{



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


        var apiResponse = await offerFacade.GetListOfferPagingAsync(pagingRequest);
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
