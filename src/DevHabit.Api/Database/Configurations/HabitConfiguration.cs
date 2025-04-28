namespace DevHabit.Api.Database.Configurations;

using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id).HasMaxLength(500);

        builder.Property(h => h.Name).HasMaxLength(100).IsRequired();

        builder.Property(h => h.Description).HasMaxLength(500).IsRequired(false);

        builder.OwnsOne(h => h.Frequency);
        
        builder.OwnsOne(h => h.Target, tb =>
        {
            tb.Property(t => t.Unit).HasMaxLength(100);
        });

        builder.OwnsOne(h => h.Milestone);
    }
}
