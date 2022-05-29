using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webapi.Models.Events;
using Webapi.Models.Identity;

namespace Webapi.DatabaseContext;

public class WebapiContext : IdentityDbContext<WebapiUser>
{
  public DbSet<Event> Events { get; set; } = default!;

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
      .Property(ev => ev.CreatedAt)
      .HasDefaultValueSql("now()");
  }
}