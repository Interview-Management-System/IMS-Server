namespace InterviewManagementSystem.Domain.Shared.Paginations
{

    /// <summary>
    /// Use for wrapping neccessary properties for paginating query in DB
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public sealed class PaginationQuery<T, TResult> where T : class
    {
        public IEnumerable<string>? IncludeProperties { get; set; }

        public PaginationParameter<T> PagingParameter { get; set; } = new();

        public Func<IQueryable<T>, IQueryable<TResult>>? Projection { get; set; }
    }
}
