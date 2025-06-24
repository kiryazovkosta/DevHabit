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
    public DateOnly? EndDate { get; set; }
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
    public DateTime? UpdatedAtUtc { get; set; }
    /// <summary>
    /// When the last completed task id added to the habit's milestone
    /// </summary>
    public DateTime? LastCompletedAtUtc { get; set; }
    /// <summary>
    /// Collection of associated tags
    /// </summary>
    public List<HabitTag> HabitTags { get; set; }
    /// <summary>
    /// Skip navigation property to the habit's tags
    /// </summary>
    public List<Tag> Tags { get; set; }
}
