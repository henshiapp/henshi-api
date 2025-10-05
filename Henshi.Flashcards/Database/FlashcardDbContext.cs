using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn.Extension;
using Henshi.Flashcards.Flashcards.Models;
using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Shared.Services;

namespace Henshi.Flashcards.Database;

public class FlashcardDbContext : DbContext
{
    private readonly IEncryptionProvider _encryptionProvider;

    public readonly string? _currentUserId;

    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<FlashcardCollection> FlashcardCollections { get; set; }

    public FlashcardDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
    {
        _encryptionProvider = new GenerateEncryptionProvider(Environment.GetEnvironmentVariable("DATABASE_ENCRYPTION_KEY"));
        _currentUserId = currentUserService.UserId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("flashcards");
        modelBuilder.UseEncryption(_encryptionProvider);

        modelBuilder.Entity<FlashcardCollection>().HasQueryFilter(c => c.UserId == _currentUserId);
        modelBuilder.Entity<Flashcard>().HasQueryFilter(f => f.Collection.UserId == _currentUserId);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}
