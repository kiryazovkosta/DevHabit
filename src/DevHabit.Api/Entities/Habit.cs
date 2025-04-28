namespace DevHabit.Api.Entities;

/// <summary>
/// 
/// </summary>
public sealed class Habit
{
    /// <summary>
    /// Identifier of the habit as string (usually a prefix + GUID v7).
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Name of the habit.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Description of the habit.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Type of the habit. 
    /// </summary>
    public HabitType Type { get; set; }
    /// <summary>
    /// How often we want to perform this habit
    /// </summary>
    public Frequency Frequency { get; set; }
    /// <summary>
    /// Specific target for a given frequency
    /// </summary>
    public Target Target { get; set; }
    /// <summary>
    /// Status of the habit
    /// </summary>
    public HabitStatus Status { get; set; }
    /// <summary>
    /// Data archiving purpose
    /// </summary>
    public bool IsArchived { get; set; }
    /// <summary>
    /// End date of the habit
    /// </summary>
    public DateTime? EndDate { get; set; }
    /// <summary>
    /// Milestone
    /// </summary>
    public Milestone? Milestone { get; set; }
    /// <summary>
    /// Gets or sets the UTC timestamp indicating when the habit was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }
    /// <summary>
    /// Gets or sets the UTC timestamp of the last update made to the habit.
    /// </summary>
    public DateTime UpdatedAtUtc { get; set; }
    /// <summary>
    /// When the last completed task id added to the habit's milestone
    /// </summary>
    public DateTime LastCompletedAtUtc { get; set; }
}

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

/// <summary>
/// Frequency of the habit
/// </summary>
public sealed class Frequency
{
    /// <summary>
    /// Type of the frequency 
    /// </summary>
    public FrequencyType Type { get; set; }
    /// <summary>
    /// Count how often we want to perform specific habit for a given frequency 
    /// </summary>
    public int TimesPerPeriod { get; set; }
}

public enum FrequencyType
{
    /// <summary>
    /// No frequency
    /// </summary>
    None = 0,
    /// <summary>
    /// Daily frequency
    /// </summary>
    Daily = 1,
    /// <summary>
    /// Weekly frequency
    /// </summary>
    Weekly = 2,
    /// <summary>
    /// Monthly frequency
    /// </summary>
    Monthly = 3
}

/// <summary>
/// Target of the habit's frequency
/// </summary>
public sealed class Target
{
    /// <summary>
    /// Target value of the habit 
    /// </summary>
    public int Value { get; set; }
    /// <summary>
    /// Unit of the target
    /// </summary>
    public string Unit { get; set; }
}

public sealed class Milestone
{
    /// <summary>
    /// How many unit of works we want to achieve
    /// </summary>
    public int Target { get; set; }
    /// <summary>
    /// Current status of the milestone
    /// </summary>
    public int Current { get; set; }
}
