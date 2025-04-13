using apiNET.Data;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Services;

public class BookService : IBookService
{
    private readonly BookDbContext _context;
    private readonly ILogger<BookService> _logger;

    public BookService(BookDbContext context, ILogger<BookService> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Book> PostBookAsync(Book book)
    {
        try
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar el libro: {Book}", book);
            return null;
        }
    }

    public async Task<IEnumerable<BookResponseDto>> GetBooksAsync()
    {
        try
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .Include(b => b.Genre)
                .Include(b => b.BookSubGenres)
                .ThenInclude(bsg => bsg.SubGenre)
                .Include(b => b.BookAwards)
                .ThenInclude(bsg => bsg.Award)
                .Select(b => new BookResponseDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    ISBN = b.ISBN,
                    CoverImage = b.CoverImage,
                    Publisher = b.Publisher,
                    Language = b.Language,
                    PageCount = b.PageCount,
                    Format = b.Format,
                    Price = b.Price,
                    Currency = b.Currency,
                    InStock = b.InStock,
                    Rating = b.Rating,
                    ReviewCount = b.ReviewCount,
                    Synopsis = b.Synopsis,
                    TargetAudience = b.TargetAudience,
                    ReadingTime = b.ReadingTime,
                    PublicationDate = b.PublicationDate,
                    Edition = b.Edition,
                    Dimensions = b.Dimensions,
                    Weight = b.Weight,
                    SalesRank = b.SalesRank,
                    MaturityRating = b.MaturityRating,
                    Series = b.Series,
                    SeriesOrder = b.SeriesOrder,
                    TableOfContents = b.TableOfContents,
                    FileSize = b.FileSize,
                    WordCount = b.WordCount,
                    Genre = new GenreResponseDto
                    {
                        Id = b.Genre.Id,
                        Name = b.Genre.Name
                    },
                    SubGenre = b.BookSubGenres.Select(bsg => new SubGenresResponseDto()
                    {
                        Id = bsg.SubGenre.Id,
                        Name = bsg.SubGenre.Name
                    }).ToList(),
                    Author = new AuthorResponseDto
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Bio = b.Author.Bio,
                        ImageUrl = b.Author.ImageUrl
                    },
                    Tags = b.BookTags.Select(bt => new TagsResponseDto()
                    {
                        Id = bt.Tag.Id,
                        Name = bt.Tag.Name
                    }).ToList(),
                    Awards = b.BookAwards.Select(ba => new AwardsResponseDto()
                    {
                        Id = ba.Award.Id,
                        Name = ba.Award.Name,
                    }).ToList()
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los libros");
            return Enumerable.Empty<BookResponseDto>();
        }
    }

    public async Task<BookResponseDto> GetBookByIdAsync(int id)
    {
        try
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .Include(b => b.Genre)
                .Include(b => b.BookSubGenres)
                .ThenInclude(bsg => bsg.SubGenre)
                .Include(b => b.BookAwards)
                .ThenInclude(bsg => bsg.Award)
                .Select(b => new BookResponseDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    ISBN = b.ISBN,
                    CoverImage = b.CoverImage,
                    Publisher = b.Publisher,
                    Language = b.Language,
                    PageCount = b.PageCount,
                    Format = b.Format,
                    Price = b.Price,
                    Currency = b.Currency,
                    InStock = b.InStock,
                    Rating = b.Rating,
                    ReviewCount = b.ReviewCount,
                    Synopsis = b.Synopsis,
                    TargetAudience = b.TargetAudience,
                    ReadingTime = b.ReadingTime,
                    PublicationDate = b.PublicationDate,
                    Edition = b.Edition,
                    Dimensions = b.Dimensions,
                    Weight = b.Weight,
                    SalesRank = b.SalesRank,
                    MaturityRating = b.MaturityRating,
                    Series = b.Series,
                    SeriesOrder = b.SeriesOrder,
                    TableOfContents = b.TableOfContents,
                    FileSize = b.FileSize,
                    WordCount = b.WordCount,
                    Genre = new GenreResponseDto
                    {
                        Id = b.Genre.Id,
                        Name = b.Genre.Name
                    },
                    SubGenre = b.BookSubGenres.Select(bsg => new SubGenresResponseDto()
                    {
                        Id = bsg.SubGenre.Id,
                        Name = bsg.SubGenre.Name
                    }).ToList(),
                    Author = new AuthorResponseDto
                    {
                        Id = b.Author.Id,
                        Name = b.Author.Name,
                        Bio = b.Author.Bio,
                        ImageUrl = b.Author.ImageUrl
                    },
                    Tags = b.BookTags.Select(bt => new TagsResponseDto()
                    {
                        Id = bt.Tag.Id,
                        Name = bt.Tag.Name
                    }).ToList(),
                    Awards = b.BookAwards.Select(ba => new AwardsResponseDto()
                    {
                        Id = ba.Award.Id,
                        Name = ba.Award.Name,
                    }).ToList()
                })
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el libro con id: {Id}", id);
            return null;
        }
    }

    public async Task<IEnumerable<Book>> SearchByTitleAsync(string title)
    {
        try
        {
            return await _context.Books.Where(book => EF.Functions.Like(book.Title, $"%{title}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al obtener el titulo del libro: {Title}", title);
            return Enumerable.Empty<Book>();
        }
    }

    public async Task<IEnumerable<Book>> SearchByAuthorAsync(string author)
    {
        try
        {
            return await _context.Books.Where(book => EF.Functions.Like(book.Author, $"%{author}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al obtener el autor del libro: {Author}", author);
            return Enumerable.Empty<Book>();
        }
    }

    public async Task<IEnumerable<Book>> SearchByGenreAsync(string genre)
    {
        try
        {
            return await _context.Books.Where(book => EF.Functions.Like(book.Genre, $"%{genre}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al obtener el genero del libro: {Genre}", genre);
            return Enumerable.Empty<Book>();
        }
    }
}