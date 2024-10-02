﻿global using AutoMapper;
global using FluentValidation;
global using InterviewManagementSystem.Application.CustomClasses;
global using InterviewManagementSystem.Application.CustomClasses.Extensions;
global using InterviewManagementSystem.Application.DTOs;
global using InterviewManagementSystem.Application.Exceptions;
global using InterviewManagementSystem.Domain.Enums;
global using InterviewManagementSystem.Domain.Interfaces;
global using Microsoft.AspNetCore.Identity;
global using System.ComponentModel.DataAnnotations;
global using ApplicationException = InterviewManagementSystem.Application.Exceptions.ApplicationException;

public static class DateUtility
{
    public static string VieDateFormat { get => "dd/MM/yyyy"; }
    public static string VieDateFormatWithTime { get => VieDateFormat + " HH:mm:ss"; }


}



