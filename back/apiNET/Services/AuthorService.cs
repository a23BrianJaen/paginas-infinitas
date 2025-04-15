using apiNET.Data;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;
using apiNET.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Services;

public class AuthorService : IAuthorService
{
    private readonly BookDbContext _context;
    private readonly ILogger<AuthorService> _logger;

    public AuthorService(BookDbContext context, ILogger<AuthorService> logger)
    {
        _logger = logger;
        _context = context;
    }

    private IQueryable<AuthorResponseDto> GetAuthorsQuery()
    {
        return _context.Authors
            .Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                ImageUrl = a.ImageUrl
            });
    }
    
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

    // TODO: Method to post a new author
    
    public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync()
    {
        try
        {
            return await GetAuthorsQuery()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los autores");
            return Enumerable.Empty<AuthorResponseDto>();
        }
    }
    
    public async Task<IEnumerable<BookResponseDto>> GetBooksByAuthorAsync(int authorId)
    {
        try
        {
            return await GetBookQuery()
                .Where(b => b.Author.Id == authorId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el autor {id}", authorId);
            return null;
        }
    }
    
    public async Task<AuthorResponseDto> GetAuthorByIdAsync(int id)
    {
        try
        {
            return await GetAuthorsQuery()
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el autor con id {Id}", id);
            return null;
        }
    }
}