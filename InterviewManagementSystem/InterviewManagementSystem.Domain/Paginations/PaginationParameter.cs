using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Paginations;

public sealed class PaginationParameter<T> where T : class
{
    public PaginationParameter()
    {
    }

    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public List<Expression<Func<T, bool>>> Filters { get; set; } = [];
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
}
