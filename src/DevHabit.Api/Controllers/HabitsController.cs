using DevHabit.Api.Extensions;
using DevHabit.Api.Services.Sorting;

namespace DevHabit.Api.Controllers;

using Database;
using DTOs.Habits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Dynamic.Core;

[ApiController]
[Route("api/habits")]
public sealed class HabitsController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<HabitCollectionDto>> GetHabits(
        [FromQuery] HabitsQueryParameters query,
        SortMappingProvider sortMappingProvider)
    {
        if (!sortMappingProvider.ValidateMappings<HabitDto, Habit>(query.Sort))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: $"The provided sort parameter isn't valid: {query.Sort}");
        }
        
        query.Search ??= query.Search?.Trim().ToLower();
        SortMapping[] sortMappings = sortMappingProvider.GetMappings<HabitDto, Habit>();

        List<HabitDto> habits = await dbContext.Habits
            .Where(h =>
                query.Search == null ||
                EF.Functions.Like(h.Name, $"%{query.Search}%") ||
                h.Description != null && EF.Functions.Like(h.Description, $"%{query.Search}%"))
            .Where(h => query.Type == null || h.Type == query.Type)
            .Where(h => query.Status == null || h.Status == query.Status)
            .ApplySort(query.Sort, sortMappings)
            .Select(HabitQueries.ProjectToDto())
            .ToListAsync();
        var dto = new HabitCollectionDto
        {
            Data = habits
        };
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<HabitWithTagsDto>> GetHabit(string id)
    {
        HabitWithTagsDto? habit = await dbContext
            .Habits
            .Where(h => h.Id == id)
            .Select(HabitQueries.ProjectToDtoWithTagsDto())
            .FirstOrDefaultAsync();
        if (habit is null)
        {
            return NotFound();
        }
        
        return Ok(habit);
    }

    [HttpPost]
    public async Task<ActionResult<HabitDto>> CreateHabit(
        CreateHabitDto createHabitDto,
        IValidator<CreateHabitDto> validator)
    {
        await validator.ValidateAndThrowAsync(createHabitDto);

        Habit habit = createHabitDto.ToEntity();
        await dbContext.Habits.AddAsync(habit);
        await dbContext.SaveChangesAsync();
        HabitDto habitDto = habit.ToDto();
        return CreatedAtAction(nameof(GetHabit), new { id = habit.Id }, habitDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateHabit(string id, [FromBody]UpdateHabitDto updateHabitDto)
    {
        Habit? habit = await dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id);
        if (habit is null)
        {
            return NotFound("Habit not found");
        }
        
        habit.UpdateFromDto(updateHabitDto);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchHabit(
        string id, 
        [FromBody] JsonPatchDocument<HabitDto> patchDocument)
    {
        Habit? habit = await dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id);
        if (habit is null)
        {
            return NotFound("Habit not found");
        }

        HabitDto habitDto = habit.ToDto();
        patchDocument.ApplyTo(habitDto, ModelState);

        if (!TryValidateModel(habitDto))
        {
            return ValidationProblem(ModelState);
        }
        
        habit.PatchFromDto(habitDto);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHabit(string id)
    {
        Habit? habit = await dbContext.Habits.FirstOrDefaultAsync(h => h.Id == id);
        if (habit is null)
        {
            //return StatusCode(StatusCodes.Status410Gone); if soft delete is implemented
            return NotFound("Habit not found");
        }
        
        dbContext.Habits.Remove(habit);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
