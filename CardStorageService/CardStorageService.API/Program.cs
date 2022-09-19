using CardStorageService.Abstractions.Interfaces.Repositories;
using CardStorageService.Abstractions.Services.Repositories;
using CardStorageService.Data.EF;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace CardStorageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;

            services.AddHttpLogging(log =>
            {
                log.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                log.RequestBodyLogLimit = 4096;
                log.ResponseBodyLogLimit = 4096;
                log.RequestHeaders.Add("Autorizatoin");
                log.RequestHeaders.Add("X-Real_IP");
                log.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(log =>
            {
                log.ClearProviders();
                log.AddConsole();
            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            services.AddDbContext<AppDbContext>(options => ApplicationContextHelper.ConfigureDbContextOptions(options, builder.Configuration));

            services.AddScoped<ICardRepositoryService, CardRepository>();
            services.AddScoped<IClientRepositoryService, ClientRepository>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var app = builder.Build();

            UpdateDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            Console.WriteLine("Start Try DB Migration");
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                    {
                        context?.Database.Migrate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.WriteLine("Stop Try DB Migration");
        }
    }
}