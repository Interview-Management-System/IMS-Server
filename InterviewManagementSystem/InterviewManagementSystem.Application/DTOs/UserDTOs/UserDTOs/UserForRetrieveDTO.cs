﻿using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Shared.Constant;

namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

public record UserForRetrieveDTO
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateTime Dob { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public string? Role { get; set; }
    public DepartmentEnum DepartmentId { get; set; }
    public string? Department => DepartmentId.GetEnumName();
    public bool? IsActive { get; set; }
    public string? StatusText => IsActive.GetValueOrDefault() ? StatusConstant.Active : StatusConstant.InActive;
    public bool IsDeleted { get; set; }
    public string? Note { get; set; }
}

