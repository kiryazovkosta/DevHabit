using DevHabit.Api.Entities;

namespace DevHabit.Api.Controllers;

using Database;
using DTOs.Habits;
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
            .Select(HabitQueries.ProjectToDto())
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
            .Select(HabitQueries.ProjectToDto())
            .FirstOrDefaultAsync();
        if (habit is null)
        {
            return NotFound();
        }
        
        return Ok(habit);
    }

    [HttpPost]
    public async Task<ActionResult<HabitDto>> CreateHabit(CreateHabitDto createHabitDto)
    {
        Habit habit = createHabitDto.ToEntity();
        await dbContext.Habits.AddAsync(habit);
        await dbContext.SaveChangesAsync();
        HabitDto habitDto = habit.ToDto();
        return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habitDto);
    }
}
