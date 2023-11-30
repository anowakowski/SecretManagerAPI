using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace SecretManagerAPI.Infrastructure
{
    public static class DBConnect
    {
        public static void Connect(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PrimaryConnectionString");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    return;
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                 options
                     .UseMySql(
                         connectionString,
                         ServerVersion.AutoDetect(connectionString), 
                         o => o.MigrationsAssembly("SecretManagerAPI")));

                return;
            }

        }
    }
}
