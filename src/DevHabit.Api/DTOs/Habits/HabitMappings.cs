using DevHabit.Api.Entities;
using DevHabit.Api.Services.Sorting;

namespace DevHabit.Api.DTOs.Habits;

internal static class HabitMappings
{
    public static readonly SortMappingDefinition<HabitDto, Habit> SortMapping = new()
    {
        Mappings =
        [
            new SortMapping(nameof(HabitDto.Name), nameof(Habit.Name)),
            new SortMapping(nameof(HabitDto.Description), nameof(Habit.Description)),
            new SortMapping(nameof(HabitDto.Type), nameof(Habit.Type)),
            new SortMapping(
                $"{nameof(HabitDto.Frequency)}.{nameof(FrequencyDto.Type)}",
                $"{nameof(Habit.Frequency)}.{nameof(Frequency.Type)}"),
            new SortMapping(
                $"{nameof(HabitDto.Frequency)}.{nameof(FrequencyDto.TimesPerPeriod)}",
                $"{nameof(Habit.Frequency)}.{nameof(Frequency.TimesPerPeriod)}"),
            new SortMapping(
                $"{nameof(HabitDto.Target)}.{nameof(TargetDto.Value)}",
                $"{nameof(Habit.Target)}.{nameof(TargetDto.Value)}"),
            new SortMapping(
                $"{nameof(HabitDto.Target)}.{nameof(TargetDto.Unit)}",
                $"{nameof(Habit.Target)}.{nameof(TargetDto.Unit)}"),
            new SortMapping(nameof(HabitDto.Status), nameof(Habit.Status)),
            new SortMapping(nameof(HabitDto.EndDate), nameof(Habit.EndDate)),
            new SortMapping(nameof(HabitDto.CreatedAtUtc), nameof(Habit.CreatedAtUtc)),
            new SortMapping(nameof(HabitDto.UpdatedAtUtc), nameof(Habit.UpdatedAtUtc)),
            new SortMapping(nameof(HabitDto.LastCompletedAtUtc), nameof(Habit.LastCompletedAtUtc)),
            new SortMapping(nameof(HabitDto.IsArchived), nameof(Habit.IsArchived))
        ]
    };
    
    
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

    public static void PatchFromDto(this Habit habit, HabitDto dto)
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
