using Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
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

                // *** C�DIGO CORREGIDO ***
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<StoreDbContext>();
                        context.Database.Migrate(); // Esto aplica las migraciones autom�ticamente
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Ocurri� un error al aplicar las migraciones");
                    }
                }
                // *** FIN DEL C�DIGO ***

                host.Run(); //  TE FALTABA ESTA L�NEA
            }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(config => {
                        config.UseStartup<Startup>(); //  CORREGIDO: "UseStartup" no "UssStartupsStartup"
                    });
        }
    }