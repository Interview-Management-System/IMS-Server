using InterviewManagementSystem.Domain.Entities;

namespace InterviewManagementSystem.Domain.Shared.Utilities;

internal static class EntityComparer
{


    /// <summary>
    /// Use for find, Original list is 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="originalList"></param>
    /// <param name="listToCompare"></param>
    /// <returns></returns>
    internal static IEnumerable<T> GetNonMatchingEntities<T>(IEnumerable<T> originalList, IEnumerable<T> listToCompare)
    {
        return originalList.Where(e => !IsEntityInList(listToCompare, e)).ToList();
    }



    private static bool IsEntityInList<T>(IEnumerable<T> list, T existingEntity)
    {

        static object? GetIdValue(T entity)
        {
            var idProperty = typeof(T).GetProperty(nameof(BaseEntity.Id));
            return idProperty?.GetValue(entity);
        }


        var entityId = GetIdValue(existingEntity);
        return entityId != null && list.Any(e => GetIdValue(e)?.Equals(entityId) == true);
    }
}
