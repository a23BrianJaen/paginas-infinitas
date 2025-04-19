using apiNET.Data;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.ResponseDtos.Operation;
using apiNET.DTOs.UpdateDtos;
using apiNET.Utils;
using apiNET.DTOs.CreateDtos;
using apiNET.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Services;

public class GenreService : IGenreService
{
    private readonly BookDbContext _context;
    private readonly ILogger<GenreService> _logger;

    public GenreService(BookDbContext context, ILogger<GenreService> logger)
    {
        _logger = logger;
        _context = context;
    }

    private IQueryable<GenreResponseDto> GetGenresQuery()
    {
        return _context.Genres
            .Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name
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

    public async Task<GenreOperationResponseDto> CreateGenreAsync(GenreCreateDto genreCreateDto)
    {
        try
        {
            _logger.LogInformation("{Green}Creating new genre: {Name}{Reset}", ConsoleColors.GREEN,
                genreCreateDto.Name, ConsoleColors.RESET);

            // Verify if exists genre with that name
            var existingGenre = await _context.Genres
                .FirstOrDefaultAsync(g => g.Name == genreCreateDto.Name);

            if (existingGenre != null)
            {
                _logger.LogWarning("{Red}An genre with the name {Name} already exists{Reset}", ConsoleColors.RED,
                    genreCreateDto.Name, ConsoleColors.RESET);

                // Return the existing genre
                return new GenreOperationResponseDto
                {
                    Genre = new GenreResponseDto
                    {
                        Id = existingGenre.Id,
                        Name = existingGenre.Name,
                    },
                    Message = $"An genre with the name {genreCreateDto.Name} already exists",
                    IsNewGenre = false,
                    Success = false
                };
            }

            // Add new genre
            var newGenre = new Genre
            {
                Name = genreCreateDto.Name,
            };

            _context.Genres.Add(newGenre);
            await _context.SaveChangesAsync();

            // Return the new genre
            return new GenreOperationResponseDto
            {
                Genre = new GenreResponseDto
                {
                    Id = newGenre.Id,
                    Name = newGenre.Name,
                },
                Message = $"Genre '{newGenre.Name}' created successfully.",
                IsNewGenre = true,
                Success = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error creating genre: {Name}{Reset}", ConsoleColors.RED, genreCreateDto.Name,
                ConsoleColors.RESET);
            return null;
        }
    }

    public async Task<IEnumerable<GenreResponseDto>> GetGenresAsync()
    {
        try
        {
            return await GetGenresQuery()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener todos los autores{Reset}", ConsoleColors.RED,
                ConsoleColors.RESET);
            return Enumerable.Empty<GenreResponseDto>();
        }
    }

    public async Task<GenreResponseDto> GetGenreByIdAsync(int id)
    {
        try
        {
            return await GetGenresQuery()
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el autor con id {Id}{Reset}", ConsoleColors.RED, id,
                ConsoleColors.RESET);
            return null;
        }
    }
    
    public async Task<IEnumerable<BookResponseDto>> GetBooksByGenreAsync(int id)
    {
        try
        {
            return await GetBookQuery()
                .Where(b => b.Genre.Id == id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el autor {id}{Reset}", ConsoleColors.RED, id,
                ConsoleColors.RESET);
            return null;
        }
    }
    
    public async Task<IEnumerable<GenreResponseDto>> UpdateGenreAsync(int id, GenreUpdateDto genreUpdateDto)
    {
        try
        {
            _logger.LogInformation($"{ConsoleColors.GREEN}Updating genre with ID {id}{ConsoleColors.RESET}");

            var genreToUpdate = await _context.Genres
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genreToUpdate == null)
            {
                _logger.LogWarning($"{ConsoleColors.RED}Genre not found with ID {id}{ConsoleColors.RESET}");
                return Enumerable.Empty<GenreResponseDto>();
            }

            // Update genre
            genreToUpdate.Name = genreUpdateDto.Name ?? genreToUpdate.Name;

            _context.Genres.Update(genreToUpdate);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                $"{ConsoleColors.GREEN}Genre with ID {id} updated successfully{ConsoleColors.RESET}");

            // Return genre
            return await GetGenresQuery()
                .Where(g => g.Id == id)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el autor {id}{Reset}", ConsoleColors.RED, id,
                ConsoleColors.RESET);
            return null;
        }
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        try
        {
            // Browse genre by id
            var genreToDelete = await _context.Genres
                .FirstOrDefaultAsync(g => g.Id == id);
    
            if (genreToDelete == null)
            {
                _logger.LogWarning("{Red}Genre not found with ID {Id}{Reset}", ConsoleColors.RED, id,
                    ConsoleColors.RESET);
                return false;
            }
    
            // Delete genre (Entity framework will handle delete)
            _context.Genres.Remove(genreToDelete);
            await _context.SaveChangesAsync();
    
            _logger.LogInformation("{Green}Genre with ID {Id} deleted successfully{Reset}", ConsoleColors.GREEN, id,
                ConsoleColors.RESET);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al eliminar el autor con ID {Id}{Reset}", ConsoleColors.RED, id,
                ConsoleColors.RESET);
            return false;
        }
    }
    
    public async Task<IEnumerable<GenreResponseDto>> SearchByGenreAsync(string name) {
        try
        {
            return await GetGenresQuery().Where(genre => EF.Functions.Like(genre.Name, $"%{name}%")).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Red}Error al obtener el autor {name}{Reset}", ConsoleColors.RED, name,
                ConsoleColors.RESET);
            return null;
        }
    }   
}