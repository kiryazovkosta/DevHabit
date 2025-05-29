namespace DevHabit.Api.DTOs.Habits;

using Microsoft.AspNetCore.Mvc;
using Entities;

public sealed class HabitsQueryParameters
{
    [FromQuery(Name = "q")]
    public string? Search { get; set; }
    public HabitType? Type { get; init; }
    public HabitStatus? Status { get; init; }
}
