namespace InterviewManagementSystem.Application.CustomClasses.Helpers;

internal static class MappingHelper
{

    internal static List<T> GetListFromContext<T>(ResolutionContext context, string key)
    {
        if (context.Items.TryGetValue(key, out var value) && value is List<T> list)
        {
            return list;
        }

        return [];
    }

}
