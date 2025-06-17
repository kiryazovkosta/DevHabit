namespace DevHabit.Api.Controllers;

using Database;
using DTOs.HabitTags;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/habits/{habitId}/tags")]
public sealed class HabitTagsController(ApplicationDbContext dbContext) : ControllerBase
{
    public static readonly string Name = nameof(HabitTagsController).Replace("Controller", string.Empty);
    
    [HttpPut]
    public async Task<ActionResult> AddTagToHabit(string habitId, UpsertHabitTagsDto upsertHabitTagsDto )
    {
        Habit? habit = await dbContext.Habits
            .Include(habit => habit.HabitTags)
            .FirstOrDefaultAsync(h => h.Id == habitId);
        if (habit is null)
        {
            return NotFound("Habit not found");
        }
        
        List<string> existingTagIds = await dbContext.Tags
            .Where(t => upsertHabitTagsDto.TagIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync();
        if (existingTagIds.Count != upsertHabitTagsDto.TagIds.Count)
        {
            return BadRequest();
        }
        
        var currentTagIds = habit.HabitTags.Select(ht => ht.TagId).ToHashSet();
        if (currentTagIds.IsSupersetOf(upsertHabitTagsDto.TagIds))
        {
            return BadRequest("One or more tag IDs is invalid!");
        }

        habit.HabitTags.RemoveAll(ht => !upsertHabitTagsDto.TagIds.Contains(ht.TagId));
        
        string[] tagIdsToAdd = [.. upsertHabitTagsDto.TagIds.Except(currentTagIds)];
        
        habit.HabitTags.AddRange(
            tagIdsToAdd.Select(
                tagId => new HabitTag
                {
                    HabitId = habitId,
                    TagId = tagId,
                    CreatedAtUtc = DateTime.UtcNow
                })
        );

        await dbContext.SaveChangesAsync();
        return NoContent();
    }
    

    [HttpDelete("{tagId}")]
    public async Task<ActionResult> DeleteHabitTag(string habitId, string tagId )
    {
        HabitTag? habitTag = await dbContext.HabitTags
            .SingleOrDefaultAsync(ht => ht.HabitId == habitId && ht.TagId == tagId);
        if (habitTag is null)
        {
            return NotFound("Habit tag not found");
        }
        
        dbContext.HabitTags.Remove(habitTag);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
