using CardStorageService.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CardStorageService.Data.EF
{
    public static class ApplicationContextHelper
    {
        public static IConfiguration GetConfiguration()
        {
            // получаем конфигурацию
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(Constants.AppSettingsJSON, true);

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (File.Exists($"appsettings.{environmentName}.json"))
                builder.AddJsonFile($"appsettings.{environmentName}.json", true);

            IConfiguration config = builder.Build();
            return config;
        }

        public static DbContextOptionsBuilder ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            Console.WriteLine("ApplicationContextHelper.ConfigureDbContextOptions start");
            // получаем строку подключения
            string connectionString = configuration[Constants.SQLProvider.ConnectionStringPath];

            return ConfigureDbContextOptions(optionsBuilder, connectionString);
        }

        public static DbContextOptionsBuilder ConfigureDbContextOptions(DbContextOptionsBuilder optionsBuilder, string connectionString, string? sqlServerProvider = null)
        {
            Console.WriteLine($"Cofigure MS SQL Provider, connection string {connectionString}");
            optionsBuilder.UseSqlServer(connectionString, bo =>
            {
                bo.CommandTimeout(200);
                bo.MigrationsAssembly("CardStorageServise.Data.MSSQL");
            });

            Console.WriteLine("ApplicationContextHelper.ConfigureDbContextOptions stop");
            return optionsBuilder;
        }
    }
}