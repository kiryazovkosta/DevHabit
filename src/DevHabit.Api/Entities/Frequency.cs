namespace DevHabit.Api.Entities;

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
