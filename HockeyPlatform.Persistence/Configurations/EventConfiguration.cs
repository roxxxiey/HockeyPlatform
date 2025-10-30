using HockeyPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HockeyPlatform.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<EventModel>
{
    public void Configure(EntityTypeBuilder<EventModel> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.DeadlineTime).IsRequired();
        builder.Property(e => e.MinCountPlayers).IsRequired();
        
        builder.Ignore(e => e.PlayersList);
    }
}