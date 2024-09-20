namespace InterviewManagementSystem.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IDisposable
{


    #region Repositories
    #endregion




    public UnitOfWork()
    {

    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
