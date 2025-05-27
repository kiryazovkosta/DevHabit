using DevHabit.Api.Database;
using DevHabit.Api.DTOs.HabitTags;
using DevHabit.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevHabit.Api.Controllers;

[ApiController]
[Route("api/habits/{habitId}/tags")]
public sealed class HabitTagsController(ApplicationDbContext dbContext) : ControllerBase
{
        
    // habits/:id/tags/:tagId
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
        if (currentTagIds.IsSubsetOf(upsertHabitTagsDto.TagIds))
        {
            return BadRequest("One or more tag IDs is invalid!");
        }

        habit.HabitTags.RemoveAll(ht => !upsertHabitTagsDto.TagIds.Contains(ht.TagId));
        
        string[] tagIdsToAdd = upsertHabitTagsDto.TagIds.Except(currentTagIds).ToArray();
        
        
        
        return Ok();
    }
    
    // habits/:id/tags/:tagId
    [HttpDelete("{tagId}")]
    public ActionResult DeleteHabitTag(string habitId, string tagId )
    {
        return Ok(new { id, tagId });
    }
}
