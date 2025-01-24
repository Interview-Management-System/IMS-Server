using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Shared.Paginations;

public sealed class PaginationParameter<T> where T : class
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? SearchText { get; set; }
    public SortCriteria? SortCriteria { get; set; } = new();
    public List<Expression<Func<T, bool>>> Filters { get; set; } = [];
}


public sealed class SortCriteria
{
    public bool IsAscending { get; set; } = true;
    public string? SortProperty { get; set; } = string.Empty;
}