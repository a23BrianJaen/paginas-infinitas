using apiNET.Data;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;
using apiNET.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Services;

public class BookService : IBookService
{
    private const string GREEN = "\u001b[32m";
    private const string RED = "\u001b[31m";
    private const string RESET = "\u001b[0m";

    private readonly BookDbContext _context;
    private readonly ILogger<BookService> _logger;

    private IQueryable<BookResponseDto> GetBookQuery()
    {
        return _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookTags)
            .ThenInclude(bt => bt.Tag)
            .Include(b => b.Genre)
            .Include(b => b.BookSubGenres)
            .ThenInclude(bsg => bsg.SubGenre)
            .Include(b => b.BookAwards)
            .ThenInclude(ba => ba.Award)
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
            });
    }

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
            return await GetBookQuery().ToListAsync();
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
            return await GetBookQuery().FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el libro con titulo: {Id} {Reset}", RED, id, RESET);
            return null;
        }
    }

    public async Task<IEnumerable<BookResponseDto>> SearchByTitleAsync(string title)
    {
        try
        {
            _logger.LogInformation($"{GREEN}Browsing books by author {title}{RESET}");

            return await GetBookQuery().Where(book => EF.Functions.Like(book.Title, $"%{title}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("{Red}Error al obtener el titulo del libro: {Title} {Rest}", RED, title, RESET);
            return Enumerable.Empty<BookResponseDto>();
        }
    }

    public async Task<IEnumerable<BookResponseDto>> SearchByAuthorAsync(string author)
    {
        try
        {
            _logger.LogInformation($"{GREEN}Browsing books by author {author}{RESET}");

            return await GetBookQuery().Where(book => EF.Functions.Like(book.Author.Name, $"%{author}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("{Red}Error al obtener el autor del libro: {Author} {Rest}", RED, author, RESET);
            return Enumerable.Empty<BookResponseDto>();
        }
    }

    public async Task<IEnumerable<BookResponseDto>> SearchByGenreAsync(string genre)
    {
        try
        {
            _logger.LogInformation($"{GREEN}Browsing books by genre {genre}{RESET}");

            return await GetBookQuery().Where(book => EF.Functions.Like(book.Genre.Name, $"%{genre}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("{Red}Error al obtener el genero del libro: {Genre}{Reset}", RED, genre, RESET);
            return Enumerable.Empty<BookResponseDto>();
        }
    }

    // public async Task<IEnumerable<BookResponseDto>> SearchBySubGenreAsync(string genre)
    // {
    //     try
    //     {
    //         
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError("Error al obtener el genero del libro: {Genre}", genre);
    //         return Enumerable.Empty<BookResponseDto>();
    //     }
    // }

    // public async Task<IEnumerable<BookResponseDto>> SearchByTagAsync(string tag)
    // {
    //     
    // }

    // public async Task<IEnumerable<BookResponseDto>> SearchByAwardAsync(string award)
    // {
    //     
    // }

    public async Task<IEnumerable<BookResponseDto>> UpdateBookAsync(int id, BookUpdateDto updateData)
    {
        try
        {
            _logger.LogInformation($"{GREEN}Updating book with ID {id}{RESET}");

            // Browsing book by id if exists with their all relations necessaries 
            var existingBook = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookSubGenres)
                .Include(b => b.BookTags)
                .Include(b => b.BookAwards)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                _logger.LogWarning($"{RED}Book not found with ID {id}{RESET}");
                return Enumerable.Empty<BookResponseDto>();
            }

            // Update basic properties
            existingBook.Title = updateData.Title ?? existingBook.Title;
            existingBook.Year = updateData.Year > 0 ? updateData.Year : existingBook.Year;
            existingBook.ISBN = updateData.ISBN ?? existingBook.ISBN;
            existingBook.CoverImage = updateData.CoverImage ?? existingBook.CoverImage;
            existingBook.Publisher = updateData.Publisher ?? existingBook.Publisher;
            existingBook.Language = updateData.Language ?? existingBook.Language;
            existingBook.PageCount = updateData.PageCount > 0 ? updateData.PageCount : existingBook.PageCount;
            existingBook.Format = updateData.Format ?? existingBook.Format;
            existingBook.Price = updateData.Price > 0 ? updateData.Price : existingBook.Price;
            existingBook.Currency = updateData.Currency ?? existingBook.Currency;
            existingBook.InStock = updateData.InStock >= 0 ? updateData.InStock : existingBook.InStock;
            existingBook.Rating = updateData.Rating >= 0 ? updateData.Rating : existingBook.Rating;
            existingBook.ReviewCount = updateData.ReviewCount >= 0 ? updateData.ReviewCount : existingBook.ReviewCount;
            existingBook.Synopsis = updateData.Synopsis ?? existingBook.Synopsis;
            existingBook.TargetAudience = updateData.TargetAudience ?? existingBook.TargetAudience;
            existingBook.ReadingTime = updateData.ReadingTime ?? existingBook.ReadingTime;
            existingBook.PublicationDate = updateData.PublicationDate ?? existingBook.PublicationDate;
            existingBook.Edition = updateData.Edition ?? existingBook.Edition;
            existingBook.Dimensions = updateData.Dimensions ?? existingBook.Dimensions;
            existingBook.Weight = updateData.Weight ?? existingBook.Weight;
            existingBook.SalesRank = updateData.SalesRank >= 0 ? updateData.SalesRank : existingBook.SalesRank;
            existingBook.MaturityRating = updateData.MaturityRating ?? existingBook.MaturityRating;
            existingBook.Series = updateData.Series ?? existingBook.Series;
            existingBook.SeriesOrder = updateData.SeriesOrder ?? existingBook.SeriesOrder;
            existingBook.TableOfContents = updateData.TableOfContents ?? existingBook.TableOfContents;
            existingBook.FileSize = updateData.FileSize ?? existingBook.FileSize;
            existingBook.WordCount = updateData.WordCount ?? existingBook.WordCount;

            // Update relations
            if (updateData.Author > 0 && updateData.Author != existingBook.AuthorId)
            {
                existingBook.AuthorId = updateData.Author;
            }

            if (updateData.Genre > 0 && updateData.Genre != existingBook.GenreId)
            {
                existingBook.GenreId = updateData.Genre;
            }

            // Update relations many-to-many
            if (updateData.Tags != null && updateData.Tags != existingBook.BookTags.Select(t => t.TagId).ToList())
            {
                existingBook.BookTags = updateData.Tags.Select(t => new BookTag() { TagId = t }).ToList();
            }

            if (updateData.Awards != null &&
                updateData.Awards != existingBook.BookAwards.Select(a => a.AwardId).ToList())
            {
                existingBook.BookAwards = updateData.Awards.Select(a => new BookAward() { AwardId = a }).ToList();
            }

            if (updateData.SubGenres != null &&
                updateData.SubGenres != existingBook.BookSubGenres.Select(s => s.SubGenreId).ToList())
            {
                existingBook.BookSubGenres =
                    updateData.SubGenres.Select(sb => new BookSubGenre() { SubGenreId = sb }).ToList();
            }

            await _context.SaveChangesAsync();

            // Get book complete updated with all relations
            var updatedBooks = await GetBookQuery()
                .Where(b => b.Id == id)
                .ToListAsync();

            return updatedBooks;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al actualizar el libro con ID {Id}{Reset}", RED, id, RESET);
            return Enumerable.Empty<BookResponseDto>();
        }
    }
    
    public async Task<bool> DeleteBookAsync(int id)
    {
        try
        {
            _logger.LogInformation($"{GREEN}Deleting book with ID {id}{RESET}");

            // Browse book by id
            var bookToDelete = await _context.Books
                .Include(b => b.BookSubGenres)
                .Include(b => b.BookTags)
                .Include(b => b.BookAwards)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookToDelete == null)
            {
                _logger.LogWarning($"{RED}Book not found with ID {id}{RESET}");
                return false;
            }

            // Delete book (Entity framework will handle delete the relationships)
            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"{GREEN}Book with ID {id} deleted successfully{RESET}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al eliminar el libro con ID {Id}{Reset}", RED, id, RESET);
            return false;
        }
    }
}