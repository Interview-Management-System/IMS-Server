namespace InterviewManagementSystem.Application.Shared.Helpers;

internal static class PaginationHelper
{
    internal static PaginationQuery<T, TResult> CreatePaginationQuery<T, TResult>(PaginationParameter<T> paginationParameter) where T : class
    {
        return new PaginationQuery<T, TResult>
        {
            PagingParameter = paginationParameter,
            Projection = MapperHelper.CreateProjection<T, TResult>()
        };
    }
}
