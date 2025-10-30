using HockeyPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HockeyPlatform.Persistence.Configurations;

public class EventUserConfiguration : IEntityTypeConfiguration<EventUserModel>
{
    public void Configure(EntityTypeBuilder<EventUserModel> builder)
    {
        builder.HasKey(eu => new { eu.EventId, eu.UserId });
        
        builder.HasOne<EventModel>()
            .WithMany()
            .HasForeignKey(eu => eu.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(eu => eu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}