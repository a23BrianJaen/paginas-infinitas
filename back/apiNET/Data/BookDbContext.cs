using apiNET.Models;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Data;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    // Main tables
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<SubGenre> SubGenres { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Award> Awards { get; set; }

    // Intermediate tables
    public DbSet<BookSubGenre> BookSubGenres { get; set; }
    public DbSet<BookTag> BookTags { get; set; }
    public DbSet<BookAward> BookAwards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Book configuration
        modelBuilder.Entity<Book>()
            .HasKey(book => book.Id);

        modelBuilder.Entity<Book>()
            .Property(book => book.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Year)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(book => book.ISBN)
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(book => book.CoverImage)
            .HasMaxLength(1000);
            
        // New book properties
        modelBuilder.Entity<Book>()
            .Property(book => book.Publisher)
            .HasMaxLength(100);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Language)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.PageCount);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Format)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Price)
            .HasColumnType("decimal(10,2)");
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Currency)
            .HasMaxLength(10);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.InStock);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Rating);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.ReviewCount);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Synopsis)
            .HasMaxLength(2000);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.TargetAudience)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.ReadingTime)
            .HasMaxLength(20);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.PublicationDate)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Edition)
            .HasMaxLength(100);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Dimensions)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Weight)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.SalesRank);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.MaturityRating)
            .HasMaxLength(20);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.Series)
            .HasMaxLength(100);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.SeriesOrder)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.TableOfContents)
            .HasMaxLength(500);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.FileSize)
            .HasMaxLength(50);
            
        modelBuilder.Entity<Book>()
            .Property(book => book.WordCount);
        
        // Book-Author relation
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
            
        // Book-Genre relation
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Genre)
            .WithMany(g => g.Books)
            .HasForeignKey(b => b.GenreId);
            
        // Author configuration
        modelBuilder.Entity<Author>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired()
            .IsRequired()
            .HasMaxLength(100);
            
        modelBuilder.Entity<Author>()
            .Property(a => a.Bio)
            .HasMaxLength(1000);
            
        modelBuilder.Entity<Author>()
            .Property(a => a.ImageUrl)
            .HasMaxLength(1000);
            
        // Genre configuration
        modelBuilder.Entity<Genre>()
            .HasKey(g => g.Id);
            
        modelBuilder.Entity<Genre>()
            .Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        // SubGenre configuration
        modelBuilder.Entity<SubGenre>()
            .HasKey(sg => sg.Id);
            
        modelBuilder.Entity<SubGenre>()
            .Property(sg => sg.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        // Tag configuration
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.Id);
            
        modelBuilder.Entity<Tag>()
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        // Award configuration
        modelBuilder.Entity<Award>()
            .HasKey(a => a.Id);
            
        modelBuilder.Entity<Award>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);

        // BookSubGenre intermediate table configuration
        modelBuilder.Entity<BookSubGenre>()
            .HasKey(bs => new { bs.BookId, bs.SubGenreId });
            
        modelBuilder.Entity<BookSubGenre>()
            .HasOne(bs => bs.Book)
            .WithMany(b => b.BookSubGenres)
            .HasForeignKey(bs => bs.BookId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<BookSubGenre>()
            .HasOne(bs => bs.SubGenre)
            .WithMany(sg => sg.BookSubGenres)
            .HasForeignKey(bs => bs.SubGenreId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // BookTag
        modelBuilder.Entity<BookTag>()
            .HasKey(bt => new { bt.BookId, bt.TagId });
            
        modelBuilder.Entity<BookTag>()
            .HasOne(bt => bt.Book)
            .WithMany(b => b.BookTags)
            .HasForeignKey(bt => bt.BookId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<BookTag>()
            .HasOne(bt => bt.Tag)
            .WithMany(t => t.BookTags)
            .HasForeignKey(bt => bt.TagId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // BookAward
        modelBuilder.Entity<BookAward>()
            .HasKey(ba => new { ba.BookId, ba.AwardId });
            
        modelBuilder.Entity<BookAward>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAwards)
            .HasForeignKey(ba => ba.BookId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<BookAward>()
            .HasOne(ba => ba.Award)
            .WithMany(a => a.BookAwards)
            .HasForeignKey(ba => ba.AwardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}