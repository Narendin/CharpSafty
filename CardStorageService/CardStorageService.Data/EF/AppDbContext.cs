using CardStorageService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CardStorageService.Data.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Card> Cards { get; set; }

        private string connectionString = "Server=.;Database=CardStorageService;Trusted_Connection=True;MultipleActiveResultSets=True";

        private IConfiguration _configuration;

        private IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = ApplicationContextHelper.GetConfiguration();
                }
                return _configuration;
            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Console.WriteLine("Initialize Application Context with Options");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.ConfigureWarnings(x => x.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));

            var optionExtensionInfo = options.Options.Extensions.FirstOrDefault(f => f.GetType().BaseType.Name.Equals(nameof(RelationalOptionsExtension), StringComparison.InvariantCultureIgnoreCase));
            if (optionExtensionInfo != null) connectionString = ((RelationalOptionsExtension)optionExtensionInfo).ConnectionString;
            Console.WriteLine($"Connection String - {connectionString}");
            Console.WriteLine($"options.IsConfigured {options.IsConfigured}");

            base.OnConfiguring(options);

            // Для миграции, если connectionString не был ранее использован из настроек, то берем закодированнй жестко в классе
            if (!options.IsConfigured)
            {
                ApplicationContextHelper.ConfigureDbContextOptions(options, Configuration);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}