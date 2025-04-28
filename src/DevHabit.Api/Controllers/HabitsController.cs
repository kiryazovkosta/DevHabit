namespace DevHabit.Api.Controllers;

using Database;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/habits")]
internal sealed class HabitsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHabits()
    {
        List<Habit> habits = await dbContext.Habits.ToListAsync();
        return Ok(habits);
    }
}
