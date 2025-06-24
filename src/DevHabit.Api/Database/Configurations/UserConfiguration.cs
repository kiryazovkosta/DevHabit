namespace DevHabit.Api.Database.Configurations;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id).HasMaxLength(500);
        builder.Property(user => user.Email).HasMaxLength(300).IsRequired();
        builder.Property(user => user.Name).HasMaxLength(100).IsRequired();
        builder.Property(user => user.IdentityId).HasMaxLength(500).IsRequired();

        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.IdentityId).IsUnique();
    }
}
