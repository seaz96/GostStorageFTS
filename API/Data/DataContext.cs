using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Index = Core.Entities.Index;

namespace API.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Gost> Gosts { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Index> Indexes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gost>().HasIndex(p => p.Id).IsUnique();
        modelBuilder.Entity<Word>().HasIndex(p => new { p.Id, p.Content }).IsUnique();
        modelBuilder.Entity<Index>().HasIndex(p => p.Id).IsUnique();
    }
}