using HockeyPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HockeyPlatform.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.AdminRights).IsRequired();
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Surname).IsRequired();
        builder.Property(u => u.Patronymic).IsRequired();
        builder.Property(u => u.Balance).IsRequired();
        builder.Property(u => u.PlayingPosition).IsRequired();
        builder.Property(u => u.Photo).IsRequired();
    }
}