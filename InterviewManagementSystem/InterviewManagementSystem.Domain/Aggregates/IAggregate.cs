namespace InterviewManagementSystem.Domain.Aggregates
{

    /// <summary>
    /// Use for marking current entity is an aggregate (Entity contains transactional operations)
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    internal interface IAggregate<TId>
    {
    }
}
