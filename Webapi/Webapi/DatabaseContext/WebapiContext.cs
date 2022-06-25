using Microsoft.EntityFrameworkCore;
using Webapi.Models.Comments;
using Webapi.Models.Events;
using Webapi.Models.Signups;

namespace Webapi.DatabaseContext;

public class WebapiContext : DbContext
{
  public DbSet<Event> Events { get; set; } = default!;

  public DbSet<Signup> Signups { get; set; } = default!;

  public DbSet<Comment> Comments { get; set; } = default!;

  public WebapiContext() : base()
  {
  }

  public WebapiContext(DbContextOptions<WebapiContext> options) : base(options)
  {
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Event>()
      .Property(e => e.CreatedAt)
      .HasDefaultValueSql("now()");
    modelBuilder.Entity<Signup>()
      .Property(e => e.CreatedAt)
      .HasDefaultValueSql("now()");
    modelBuilder.Entity<Comment>()
      .Property(e => e.CreatedAt)
      .HasDefaultValueSql("now()");
  }
}