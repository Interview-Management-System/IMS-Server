﻿using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Features.JobFeature.UseCases;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.JobFeature;

public sealed class JobFacade
{


    public JobStatusUseCase JobStatusUseCase { get; private set; }
    public JobCreateUseCase JobForCreateUseCase { get; private set; }
    public JobUpdateUseCase JobUpdateUseCase { get; private set; }
    public JobRetrieveUseCase JobRetrieveUseCase { get; private set; }



    public JobFacade(JobCreateUseCase jobForCreateUseCase, JobStatusUseCase jobStatusUseCase, JobUpdateUseCase jobUpdateUseCase, JobRetrieveUseCase jobRetrieveUseCase)
    {
        JobStatusUseCase = jobStatusUseCase;
        JobUpdateUseCase = jobUpdateUseCase;
        JobForCreateUseCase = jobForCreateUseCase;
        JobRetrieveUseCase = jobRetrieveUseCase;
    }


    public async Task<ApiResponse<PageResult<JobForRetrieveDTO>>> GetListJobPagingAsync(PaginationRequest paginationRequest)
    {
        return await JobRetrieveUseCase.GetListJobPagingAsync(paginationRequest);
    }


    public async Task<string> CreateJobAsync(JobForCreateDTO jobForCreateDTO)
    {
        return await JobForCreateUseCase.CreateNewJobAsync(jobForCreateDTO);
    }



    public async Task<ApiResponse<JobForRetrieveDTO>> GetJobDetailByIdAsync(Guid id)
    {
        return await JobRetrieveUseCase.GetJobDetailByIdAsync(id);
    }



    public async Task<string> UpdateJobAsync(JobForUpdateDTO jobForUpdateDTO)
    {
        return await JobUpdateUseCase.UpdateJobAsync(jobForUpdateDTO);
    }



    public async Task<string> OpenJobAsync(Guid id)
    {
        return await JobStatusUseCase.OpenJob(id);
    }


    public async Task<string> CloseJobAsync(Guid id)
    {
        return await JobStatusUseCase.CloseJob(id);
    }


    public async Task<string> DeleteJobAsync(Guid id)
    {
        return await JobStatusUseCase.DeleteJobAsync(id);
    }


    public async Task<string> SaveDraftAsync(Guid id)
    {
        return await JobStatusUseCase.SaveJobAsDraft(id);
    }
}
