using Infrastructure.Repositories.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;


namespace API
    {
        public class Program
        {
            protected Program()
            {
            }

            public static void Main(string[] args)
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<StoreDbContext>();
                        if (context.Database.IsRelational()) {
                        context.Database.Migrate(); // Esto aplica las migraciones automáticamente
                    }

                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Ocurrió un error al aplicar las migraciones");
                    }
                }

                host.Run(); 
            }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(config => {
                        config.UseStartup<Startup>();
                    });
        }
    }