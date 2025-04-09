using apiNET.Models;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Data;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } //Getter and Setter

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Define the properties for the Book entities
        modelBuilder.Entity<Book>()
            .HasKey(book => book.Id);

        modelBuilder.Entity<Book>()
            .Property(book => book.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        modelBuilder.Entity<Book>()
            .Property(book => book.Author)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(book => book.Genre)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(book => book.Year)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.ISBN)
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(book => book.CoverImage);
            // .HasMaxLength(1000);
    }
}