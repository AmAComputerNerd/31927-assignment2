using HotelSmartManagement.Common.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HotelSmartManagement.Misc
{
    /// <summary>
    /// This class is a design-time factory for DbContext for the 'dotnet ef' tool.
    /// Without this class, the 'dotnet ef' tool fails to create the DbContext as WPF does not explictly use a Startup class of any kind, and hence the tool fails to find the place we set our DbContextOptions and spits a fat red error in the console.
    /// <b>DO NOT DELETE THIS CLASS. DO NOT USE THIS CLASS TO GET A DBCONTEXT IN STANDARD CODE!</b>
    /// </summary>
    public class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
    {
        private IConfiguration BuildConfiguration()
        {
            // Set environment variable (you can set this based on actual environment)
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            // Set the base path to the project root
            var projectRootPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

            // Build configuration
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(projectRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            return configurationBuilder.Build();
        }

        public HotelDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

            return new HotelDbContext(optionsBuilder.Options);
        }
    }
}
