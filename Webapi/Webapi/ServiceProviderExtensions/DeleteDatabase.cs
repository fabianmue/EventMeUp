using Webapi.DatabaseContext;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void DeleteDatabase(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    context.Database.EnsureDeleted();
  }
}