namespace DevHabit.Api.Entities;

public enum HabitStatus
{
    /// <summary>
    /// No status of the habit
    /// </summary>
    None = 0,
    /// <summary>
    /// We haven't completed yet
    /// </summary>
    Ongoing = 1,
    /// <summary>
    /// Completed the habit
    /// </summary>
    Completed = 2
}
