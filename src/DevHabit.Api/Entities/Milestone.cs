namespace DevHabit.Api.Entities;

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
