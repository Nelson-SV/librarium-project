using Librarium.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Librarium.Data.Database;

public class LibrariumDbContext : DbContext
{
    public LibrariumDbContext(DbContextOptions<LibrariumDbContext> options)
        : base(options) { }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<BookAuthor> BookAuthor { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();
    }

}