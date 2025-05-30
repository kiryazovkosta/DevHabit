using System.Linq.Dynamic.Core;
using DevHabit.Api.Services.Sorting;

namespace DevHabit.Api.Extensions;

internal static class QueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> queryable,
        string? sort,
        SortMapping[] mappings,
        string defaultOrderBy = "Id")
    {
        if (string.IsNullOrWhiteSpace(sort))
        {
            return queryable.OrderBy(defaultOrderBy);
        }
        
        string[] sortFields = [.. sort.Split(',')
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))];

        var orderByParts = new List<string>(); 
        foreach (string field in sortFields)
        {
            (string sortField, bool isDescending) = ParseSortField(field);

            SortMapping mapping =
                mappings.First(m => m.SortField.Equals(sortField, StringComparison.OrdinalIgnoreCase));

            string direction = (isDescending, mapping.IsDescending) switch
            {
                (false, false) => "ASC",
                (false, true) => "DESC",
                (true, false) => "DESC",
                (true, true) => "ASC",
            };
            
            orderByParts.Add($"{mapping.PropertyName} {direction}");
        }

        string orderBy = string.Join(",", orderByParts);
        return queryable.OrderBy(orderBy);
    }

    private static (string SortField, bool IsDescending) ParseSortField(string field)
    {
        string[] parts = field.Split(' ');
        string sortField = parts[0];
        bool isDescending = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);
        return (sortField, isDescending);
    }
}
