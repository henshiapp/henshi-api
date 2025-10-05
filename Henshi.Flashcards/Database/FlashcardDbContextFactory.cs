using Henshi.Flashcards.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class FlashcardDbContextFactory : IDesignTimeDbContextFactory<FlashcardDbContext>
{
    public FlashcardDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FlashcardDbContext>();

        var jsonFile = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
            ? "appsettings.Development.json"
            : "appsettings.json";

        var configurationManager = new ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configurationManager.GetConnectionString("FlashcardsConnectionString");

        optionsBuilder.UseNpgsql(connectionString);

        return new FlashcardDbContext(optionsBuilder.Options, null);
    }
}