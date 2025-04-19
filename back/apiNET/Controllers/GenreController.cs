using Microsoft.AspNetCore.Mvc;
using apiNET.Services.Interfaces;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;
using apiNET.DTOs.ResponseDtos;

namespace apiNET.Controllers;

[ApiController]
[Route("/api/genres")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly ILogger<GenreController> _logger;

    public GenreController(IGenreService genreService, ILogger<GenreController> logger)
    {
        _genreService = genreService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDto genreCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(genreCreateDto.Name))
            {
                return BadRequest("Genre name is required");
            }

            var genre = await _genreService.CreateGenreAsync(genreCreateDto);

            if (!genre.Success)
            {
                return BadRequest(genre.Message);
            }

            if (genre.IsNewGenre)
            {
                return CreatedAtAction(
                    nameof(GetGenres),
                    new { id = genre.Genre.Id },
                    genre
                );
            }

            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating genre");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        try
        {
            var genres = await _genreService.GetGenresAsync();
            if (genres == null)
            {
                return NotFound("No se encontraron géneros");
            }

            return Ok(genres);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting genres");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        try
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound($"Genre with ID {id} not found");
            }

            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting genre with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search-book/{genreId}")]
    public async Task<IActionResult> GetBookByGenre(int genreId)
    {
        try
        {
            var books = await _genreService.GetBooksByGenreAsync(genreId);
            if (books == null)
            {
                return NotFound($"Book by genre {genreId} not found");
            }

            if (!books.Any())
            {
                return NotFound($"No books found for genre with ID {genreId}");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by name {Id}", genreId);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreUpdateDto genreUpdateDto)
    {
        try
        {
            var updatedGenre = await _genreService.UpdateGenreAsync(id, genreUpdateDto);
            if (!updatedGenre.Any())
            {
                return NotFound($"Genre with id {id} not found");
            }

            return Ok(updatedGenre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating genre with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        try
        {
            var result = await _genreService.DeleteGenreAsync(id);
            if (!result)
            {
                return NotFound($"Genre with ID {id} not found");
            }
    
            return Ok(new { message = $"Genre with ID {id} deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting genre with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchByName(string name)
    {
        try
        {
            var genre = await _genreService.SearchByGenreAsync(name);
            if (genre == null)
            {
                return NotFound($"Genre with name {name} not found");
            }
    
            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for genre with name {Name}", name);
            return BadRequest(ex.Message);
        }
    }
}