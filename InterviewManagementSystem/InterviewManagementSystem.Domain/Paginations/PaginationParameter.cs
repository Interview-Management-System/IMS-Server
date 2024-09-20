using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Paginations;

public struct PaginationParameter<T>
{
    public PaginationParameter()
    {
    }

    public int PageSize { get; set; } = 5;
    public int PageIndex { get; set; } = 1;
    public IEnumerable<Expression<Func<T, bool>>>? Filters { get; set; }
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
}
