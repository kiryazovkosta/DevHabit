namespace DevHabit.Api.Entities;

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
