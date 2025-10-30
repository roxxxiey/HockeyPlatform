using HockeyPlatform.Domain.Models;
using HockeyPlatform.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HockeyPlatform.Persistence;

public class HockeyPlatformDbContext : DbContext
{
    public HockeyPlatformDbContext(DbContextOptions<HockeyPlatformDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> Users { get; set; }
    public DbSet<EventModel> Events { get; set; }
    public DbSet<EventUserModel> EventUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new EventUserConfiguration());
    
        base.OnModelCreating(modelBuilder);
    }
}