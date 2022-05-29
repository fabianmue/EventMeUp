using Microsoft.EntityFrameworkCore;
using Webapi.DatabaseContext;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void MigrateDatabase(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    context.Database.Migrate();
  }
}