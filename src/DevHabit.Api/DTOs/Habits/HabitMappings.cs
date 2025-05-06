using DevHabit.Api.Entities;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitMappings
{
    public static Habit ToEntity(this CreateHabitDto createHabitDto)
    {
        Habit habit = new()
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = createHabitDto.Name,
            Description = createHabitDto.Description,
            Type = createHabitDto.Type,
            Frequency = new Frequency
            {
                Type = createHabitDto.Frequency.Type,
                TimesPerPeriod = createHabitDto.Frequency.TimesPerPeriod,
            },
            Target = new Target()
            {
                Unit = createHabitDto.Target.Unit,
                Value = createHabitDto.Target.Value
            },
            Status = HabitStatus.Ongoing,
            IsArchived = false,
            EndDate = createHabitDto.EndDate,
            Milestone = createHabitDto.Milestone is not null ? 
                new Milestone()
                {
                    Current = createHabitDto.Milestone.Current,
                    Target = createHabitDto.Milestone.Target,
                } 
                : null, 
            CreatedAtUtc = DateTime.UtcNow,
            
        };
        return habit;
    }
    public static HabitDto ToDto(this Habit habit)
    {
        return new HabitDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            Type = habit.Type,
            Frequency = new FrequencyDto()
            {
                Type = habit.Frequency.Type,
                TimesPerPeriod = habit.Frequency.TimesPerPeriod,
            },
            Target = new TargetDto()
            {
                Unit = habit.Target.Unit,
                Value = habit.Target.Value,
            },
            Status = habit.Status,
            IsArchived = habit.IsArchived,
            EndDate = habit.EndDate,
            Milestone = habit.Milestone == null
                ? null
                : new MilestoneDto()
                {
                    Current = habit.Milestone.Current,
                    Target = habit.Milestone.Target,
                },
            CreatedAtUtc = habit.CreatedAtUtc,
            UpdatedAtUtc = habit.UpdatedAtUtc,
            LastCompletedAtUtc = habit.LastCompletedAtUtc
        };
    }

    public static void UpdateFromDto(this Habit habit, UpdateHabitDto dto)
    {
        // Basic properties
        habit.Name = dto.Name;
        habit.Description = dto.Description;
        habit.Type = dto.Type;
        habit.EndDate = dto.EndDate;

        // Update frequency ( Create new because record is immutable)
        habit.Frequency = new Frequency()
        {
            Type = dto.Frequency.Type,
            TimesPerPeriod = dto.Frequency.TimesPerPeriod,
        };

        habit.Target = new Target()
        {
            Unit = dto.Target.Unit,
            Value = dto.Target.Value
        };

        if (dto.Milestone is not null)
        {
            habit.Milestone ??= new Milestone();
            habit.Milestone.Target = dto.Milestone.Target;
        }
        
        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}
