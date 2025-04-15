using Microsoft.AspNetCore.Mvc;
using apiNET.Services.Interfaces;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;

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
    
    // TODO: Method to post a new author
    
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
    
}