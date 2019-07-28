
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SqlServices.ServicesCollection
{
    public static class ServicesCollection
    {
        public static void AddSqlService(this IServiceCollection service)
        {
            service.AddScoped(p =>
            {
                var settings = p.GetService<IOptions<BoutiqueSettings>>().Value;
                return new SqlConnection(settings.ConnectionString);
            });
        }
    }
}
