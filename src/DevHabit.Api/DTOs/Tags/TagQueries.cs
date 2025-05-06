namespace DevHabit.Api.DTOs.Tags;

using System.Linq.Expressions;
using Entities;

public static class TagQueries
{
    public static Expression<Func<Tag, TagDto>> ProjectToDto()
    {
        return tag => new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Description = tag.Description,
            CreatedAtUtc = tag.CreatedAtUtc,
            UpdatedAtUtc = tag.UpdatedAtUtc
        };
    }
}
