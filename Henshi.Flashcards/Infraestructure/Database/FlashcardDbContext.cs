using System;
using Microsoft.EntityFrameworkCore;
using Henshi.Flashcards.Domain;
using System.Reflection;
using Henshi.Flashcards.Domain.Models;

namespace Henshi.Flashcards.Infraestructure.Database;

public class FlashcardDbContext : DbContext
{
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<FlashcardCollection> FlashcardCollections { get; set; }

    public FlashcardDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("flashcards");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
