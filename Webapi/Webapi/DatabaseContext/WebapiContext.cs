using Microsoft.EntityFrameworkCore;
using Webapi.Models.Events;

namespace Webapi.DatabaseContext;

public class WebapiContext : DbContext
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
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
  }
}