using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public sealed class TaskContext : DbContext
{
    public TaskContext()
    {
        Database.EnsureCreated();
    }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) 
    { 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventEntity>()
            .HasMany(e => e.Participants)
            .WithMany(p => p.Events);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<ParticipantEntity> Participants { get; set; }
    public DbSet<EventEntity> Events { get; set; }
}