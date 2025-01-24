namespace InterviewManagementSystem.Application.Managers.JobFeature.UseCases;

public sealed class JobStatusUseCase(IUnitOfWork unitOfWork)
{


    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    internal async Task<string> OpenJob(Guid id)
    {
        var jobFoundById = await _unitOfWork.JobRepository.GetByIdAsync(id, true);

        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to open");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);

        jobFoundById.OpenJob();

        bool deleteSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to open job");


        return "Open job successfully";
    }



    internal async Task<string> SaveJobAsDraft(Guid id)
    {
        var jobFoundById = await _unitOfWork.JobRepository.GetByIdAsync(id, true);

        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to save draft");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);

        jobFoundById.SaveAsDraft();

        bool deleteSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to save draft");


        return "Save draft successfully";
    }



    internal async Task<string> CloseJob(Guid id)
    {
        var jobFoundById = await _unitOfWork.JobRepository.GetByIdAsync(id, true);

        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to close");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);

        jobFoundById.CloseJob();

        bool deleteSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to close job");


        return "Close job successfully";
    }


    internal async Task<string> DeleteJobAsync(Guid id)
    {
        var jobFoundById = await _unitOfWork.JobRepository.GetByIdAsync(id, true);

        ArgumentNullException.ThrowIfNull(jobFoundById, "Job not found to delete");
        ApplicationException.ThrowIfGetDeletedRecord(jobFoundById.IsDeleted);

        jobFoundById.DeleteJob();

        bool deleteSuccess = await _unitOfWork.SaveChangesAsync();
        ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to delete job");


        return "Delete job successfully";
    }
}
