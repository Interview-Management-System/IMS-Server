namespace InterviewManagementSystem.Application.Shared.Helpers;

public static class FilterHelper
{


    public static IServiceProvider? Service { get; set; }

    /*
    public static List<Expression<Func<TEntity, bool>>> BuildFilters<TEntity>(PaginationRequest paginationRequest, string searchFieldName)
    {
        var listCondition = new List<Expression<Func<TEntity, bool>>>();
        var parameter = Expression.Parameter(typeof(TEntity), "entity");



        //BuildSearchExpressions(listCondition, paginationRequest, parameter);

        //A<TEntity>(typeof(TEntity), parameter);
        BuildNameSearch(listCondition, paginationRequest, parameter);
        BuildEnumSearch(listCondition, paginationRequest, parameter);


        return listCondition;
    }




    private static void BuildNameSearch<TEntity>(List<Expression<Func<TEntity, bool>>> listCondition, PaginationRequest paginationRequest, ParameterExpression parameter)
    {


        // Iterate through each search field in the pagination request
        foreach (var searchField in paginationRequest.FieldNamesToSearch)
        {
            if (string.IsNullOrEmpty(searchField.Value))
                continue;



            // Split the key to handle potential navigation properties
            var propertyNames = searchField.Key.Split('.');
            Expression searchExpression = parameter;


            // Traverse the properties to get to the desired field
            foreach (var propertyName in propertyNames)
            {
                // Access the property (could be a navigation property)
                searchExpression = Expression.Property(searchExpression, propertyName);
            }


            // Ensure we are dealing with a string property before calling Contains
            if (searchExpression.Type == typeof(string))
            {

                // Convert the property and the search value to lowercase for case-insensitive comparison
                var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes);


                var searchFieldLower = Expression.Call(searchExpression, toLowerMethod!); // entity field.ToLower()
                var searchValueLower = Expression.Constant(searchField.Value.ToLower()); // search value.ToLower()


                // Use string.Contains for the case-insensitive comparison
                var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)]);
                var searchCall = Expression.Call(searchFieldLower, containsMethod!, searchValueLower);


                listCondition.Add(Expression.Lambda<Func<TEntity, bool>>(searchCall, parameter));
            }
            else
            {
                throw new InvalidOperationException($"The property '{searchField.Key}' is not a string type.");
            }
        }
    }




    private static void BuildEnumSearch<TEntity>(List<Expression<Func<TEntity, bool>>> listCondition, PaginationRequest paginationRequest, ParameterExpression parameter)
    {
        // Loop through the EnumsToFilter dictionary
        foreach (var enumFilter in paginationRequest.EnumsToFilter)
        {

            var enumValue = enumFilter.Value;

            var enumFieldName = enumFilter.Key;


            var enumField = Expression.PropertyOrField(parameter, enumFieldName);
            var enumFieldAsNullable = Expression.Convert(enumField, typeof(short?));


            var enumId = (short?)Convert.ToInt16(enumValue);
            var enumValueAsNullable = Expression.Constant(enumId, typeof(short?));


            var enumComparison = Expression.Equal(enumFieldAsNullable, enumValueAsNullable);
            listCondition.Add(Expression.Lambda<Func<TEntity, bool>>(enumComparison, parameter));
        }
    }







    private static void A<TEntity>(Type entityType, ParameterExpression parameter)
    {

        var dicProperties = EntityHelper.EntitySearchFieldMappings[entityType];
        //var properties = new List<PropertyInfo>(entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance));

        var s = Service.GetRequiredService<IUnitOfWork>().InterviewManagementSystemContext;


        foreach (var item in dicProperties)
        {

            try
            {
                var sss = EntityHelper.IsNavigationProperty<TEntity>(s, item);
                var enumField = Expression.PropertyOrField(parameter, item);
            }
            catch (Exception)
            {
                continue;
            }
        }

    }



    private static void BuildSearchExpressions<TEntity>(List<Expression<Func<TEntity, bool>>> listCondition, PaginationRequest paginationRequest, ParameterExpression parameter)
    {
        // Define a list of search criteria
        var searchFields = new Dictionary<string, string>
        {
            { "Title", paginationRequest.FieldNamesToSearch.GetValueOrDefault("nameSearch") },
            { "Candidate.UserName", paginationRequest.FieldNamesToSearch.GetValueOrDefault("nameSearch") },
            { "Candidate.Email", paginationRequest.FieldNamesToSearch.GetValueOrDefault("nameSearch") }
        };

        foreach (var searchField in searchFields)
        {
            // If the value to search is not null or empty, create a search expression
            if (!string.IsNullOrEmpty(searchField.Value))
            {
                var propertyNames = searchField.Key.Split('.');
                Expression searchExpression = parameter;

                // Traverse the properties to get to the desired field
                foreach (var propertyName in propertyNames)
                {
                    searchExpression = Expression.Property(searchExpression, propertyName);
                }

                // Create the "Contains" method call
                var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                var containsExpression = Expression.Call(searchExpression, containsMethod!, Expression.Constant(searchField.Value));

                // Add the lambda expression to the list of conditions
                listCondition.Add(Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter));
            }
        }
    }
    */
}




