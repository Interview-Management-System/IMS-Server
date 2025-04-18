﻿using FluentValidation;
using FluentValidation.AspNetCore;
using InterviewManagementSystem.Application.Validations;

namespace InterviewManagementSystem.API.Configurations
{
    internal static class FluentValidationConfiguration
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<ValidationAssemblyMarker>();
        }
    }
}
