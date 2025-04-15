using Microsoft.AspNetCore.Mvc;
using apiNET.Services.Interfaces;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;
using apiNET.DTOs.ResponseDtos;

namespace apiNET.Controllers;

[ApiController]
[Route("/api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly ILogger<AuthorController> _logger;

    public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
    {
        _authorService = authorService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDto authorCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(authorCreateDto.Name))
            {
                return BadRequest("Author name is required");
            }

            var author = await _authorService.CreateAuthorAsync(authorCreateDto);

            if (!author.Success)
            {
                return BadRequest(author.Message);
            }

            if (author.IsNewAuthor)
            {
                return CreatedAtAction(
                    nameof(GetAuthor),
                    new { id = author.Author.Id },
                    author
                );
            }
            else
            {
                return Ok(author);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new author");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthors()
    {
        try
        {
            var authors = await _authorService.GetAuthorsAsync();
            if (authors == null)
            {
                return NotFound("Authors not found");
            }

            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting authors");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search-book/{authorId}")]
    public async Task<IActionResult> GetBookByAuthor(int authorId)
    {
        Console.WriteLine(authorId);
        try
        {
            var books = await _authorService.GetBooksByAuthorAsync(authorId);
            if (books == null)
            {
                return NotFound($"Book by author {authorId} not found");
            }

            if (!books.Any())
            {
                return NotFound($"No books found for author with ID {authorId}");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by name {Id}", authorId);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthor(int id)
    {
        try
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound($"Author with ID {id} not found");
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting author with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> SearchByName(string name)
    {
        try
        {
            var author = await _authorService.SearchByAuthorAsync(name);
            if (author == null)
            {
                return NotFound($"Author with name {name} not found");
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for author with name {Name}", name);
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto authorUpdateDto)
    {
        try
        {
            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, authorUpdateDto);
            if (!updatedAuthor.Any())
            {
                return NotFound($"Author with id {id} not found");
            }

            return Ok(updatedAuthor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating author with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
            {
                return NotFound($"Author with ID {id} not found");
            }

            return Ok(new { message = $"Author with ID {id} deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting author with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }
}