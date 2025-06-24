namespace DevHabit.Api.Entities;

public enum HabitType
{
    /// <summary>
    /// No type of the habit 
    /// </summary>
    None = 0,
    /// <summary>
    /// Completing some specific task or not
    /// </summary>
    Binary = 1,
    /// <summary>
    /// Track some values 
    /// </summary>
    Measurable = 2
}
