using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Henshi.Flashcards.Domain.Models;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using EntityFrameworkCore.EncryptColumn.Extension;

namespace Henshi.Flashcards.Infraestructure.Database;

public class FlashcardDbContext : DbContext
{
    private readonly IEncryptionProvider _encryptionProvider;

    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<FlashcardCollection> FlashcardCollections { get; set; }

    public FlashcardDbContext(DbContextOptions options) : base(options)
    {
        _encryptionProvider = new GenerateEncryptionProvider(Environment.GetEnvironmentVariable("DATABASE_ENCRYPTION_KEY"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("flashcards");
        modelBuilder.UseEncryption(_encryptionProvider);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}
