using Application;
using Application.Registrations;
using AutoMapper;
using Core.Application;
using Application.ApplicationServices;
using Filters;
using Infrastructure.Extensions;
using Infrastructure.Factories;
using Infrastructure.Registrations;
using Infrastructure.Repositories.Sql;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddInfrastructureServices(Configuration);
            services.AddApplicationServices();

            services.AddConfiguredDataBase(Configuration);

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<BaseExceptionFilter>();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            //services.AddDbContext<StoreDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hybrid Architecture Project", Version = "v1" });
            });
            
            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add<BaseExceptionFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Infrastructure.Repositories.Sql.StoreDbContext>();

                // Forzar la creación de la base de datos SQLite
                try
                {
                    Console.WriteLine("Creando base de datos SQLite...");
                    context.Database.EnsureCreated();
                    Console.WriteLine("Base de datos creada exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creando base de datos: {ex.Message}");
                }
            }

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            CustomMapper.Instance = app.ApplicationServices.GetRequiredService<IMapper>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();
            //UseEventBus(app);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void UseEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // Aqui se registran las subscripciones a eventos del bus de eventos, vinculando
            //eventos con sus respectivos handlers
            eventBus.Subscribe<DummyEntityCreatedIntegrationEvent, DummyEntityCreatedIntegrationEventHandlerSub>();
        }
    }
}
