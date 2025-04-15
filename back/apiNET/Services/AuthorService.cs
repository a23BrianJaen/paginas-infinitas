using apiNET.Data;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;
using apiNET.Utils;
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

    public async Task<AuthorOperationResponseDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto)
    {
        try
        {
            _logger.LogInformation("{Green}Creating new author: {Name}{Reset}", ConsoleColors.GREEN,
                authorCreateDto.Name, ConsoleColors.RESET);

            // Verify if exists author with that name
            var existingAuthor = await _context.Authors
                .FirstOrDefaultAsync(a => a.Name == authorCreateDto.Name);

            if (existingAuthor != null)
            {
                _logger.LogWarning("{Red}An author with the name {Name} already exists{Reset}", ConsoleColors.RED,
                    authorCreateDto.Name, ConsoleColors.RESET);

                // Return the existing author
                return new AuthorOperationResponseDto
                {
                    Author = new AuthorResponseDto
                    {
                        Id = existingAuthor.Id,
                        Name = existingAuthor.Name,
                        Bio = existingAuthor.Bio,
                        ImageUrl = existingAuthor.ImageUrl
                    },
                    Message = $"An author with the name {authorCreateDto.Name} already exists",
                    IsNewAuthor = false,
                    Success = false
                };
            }

            // Add new author
            var newAuthor = new Author
            {
                Name = authorCreateDto.Name,
                Bio = authorCreateDto.Bio ?? string.Empty,
                ImageUrl = authorCreateDto.ImageUrl ?? string.Empty
            };

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();

            // Return the new author
            return new AuthorOperationResponseDto
            {
                Author = new AuthorResponseDto
                {
                    Id = newAuthor.Id,
                    Name = newAuthor.Name,
                    Bio = newAuthor.Bio,
                    ImageUrl = newAuthor.ImageUrl
                },
                Message = $"Author '{newAuthor.Name}' created successfully.",
                IsNewAuthor = true,
                Success = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error creating author: {Name}{Reset}", ConsoleColors.RED, authorCreateDto.Name,
                ConsoleColors.RESET);
            return null;
        }
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync()
    {
        try
        {
            return await GetAuthorsQuery()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener todos los autores{Reset}", ConsoleColors.RED,
                ConsoleColors.RESET);
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
            _logger.LogError(ex, "{Red}Error al obtener el autor {id}{Reset}", ConsoleColors.RED, authorId,
                ConsoleColors.RESET);
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
            _logger.LogError(ex, "{Red}Error al obtener el autor con id {Id}{Reset}", ConsoleColors.RED, id,
                ConsoleColors.RESET);
            return null;
        }
    }
    
    public async Task<IEnumerable<AuthorResponseDto>> SearchByAuthorAsync(string name) {
        try
        {
            return await GetAuthorsQuery().Where(author => EF.Functions.Like(author.Name, $"%{name}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el autor {name}{Reset}", ConsoleColors.RED, name,
                ConsoleColors.RESET);
            return null;
        }
    }
    
    public async Task<IEnumerable<AuthorResponseDto>> UpdateAuthorAsync(int id, AuthorUpdateDto updateAuthor)
    {
        try
        {
            _logger.LogInformation($"{ConsoleColors.GREEN}Updating author with ID {id}{ConsoleColors.RESET}");

            var authorToUpdate = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (authorToUpdate == null)
            {
                _logger.LogWarning($"{ConsoleColors.RED}Author not found with ID {id}{ConsoleColors.RESET}");
                return Enumerable.Empty<AuthorResponseDto>();
            }

            // Update author
            authorToUpdate.Name = updateAuthor.Name ?? authorToUpdate.Name;
            authorToUpdate.Bio = updateAuthor.Bio ?? authorToUpdate.Bio;
            authorToUpdate.ImageUrl = updateAuthor.ImageUrl ?? authorToUpdate.ImageUrl;

            _context.Authors.Update(authorToUpdate);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"{ConsoleColors.GREEN}Author with ID {id} updated successfully{ConsoleColors.RESET}");

            return await GetAuthorsQuery()
                .Where(a => a.Id == id)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        try
        {
            // Browse author by id
            var authorToDelete = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (authorToDelete == null)
            {
                _logger.LogWarning($"{ConsoleColors.RED}Author not found with ID {id}{ConsoleColors.RESET}");
                return false;
            }

            // Delete author (Entity framework will handle delete)
            _context.Authors.Remove(authorToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"{ConsoleColors.GREEN}Author with ID {id} deleted successfully{ConsoleColors.RESET}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al eliminar el autor con ID {Id}{Reset}", ConsoleColors.RED, id, ConsoleColors.RESET);
            return false;
        }
    }
}