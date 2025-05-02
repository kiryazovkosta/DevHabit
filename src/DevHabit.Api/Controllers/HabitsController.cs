using DevHabit.Api.DTOs.Habits;

namespace DevHabit.Api.Controllers;

using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/habits")]
public sealed class HabitsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<HabitCollectionDto>> GetHabits()
    {
        List<HabitDto> habits = await dbContext
            .Habits
            .Select(h => HabitDto.CreateFrom(h))
            .ToListAsync();
        var dto = new HabitCollectionDto
        {
            Data = habits
        };
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HabitDto>> GetHabit(string id)
    {
        HabitDto habit = await dbContext
            .Habits
            .Where(h => h.Id == id)
            .Select(h => HabitDto.CreateFrom(h))
            .FirstOrDefaultAsync();
        if (habit is null)
        {
            return NotFound();
        }
        
        return Ok(habit);
    }
}
