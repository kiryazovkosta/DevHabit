namespace DevHabit.Api.DTOs.Habits;

using Microsoft.AspNetCore.Mvc;
using Entities;

public sealed class HabitsQueryParameters
{
    [FromQuery(Name = "q")]
    public string? Search { get; set; }
    public HabitType? Type { get; init; }
    public HabitStatus? Status { get; init; }
    public string? Sort { get; init; }
    public string? Fields { get; init; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
