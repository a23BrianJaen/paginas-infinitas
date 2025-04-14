using Microsoft.AspNetCore.Mvc;
using apiNET.Services.Interfaces;
using apiNET.DTOs.UpdateDtos;
using apiNET.Models;

namespace apiNET.Controllers;

[ApiController]
[Route("/api/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookController> _logger;

    public BookController(IBookService bookService, ILogger<BookController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        try
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting books");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found");
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting book with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("search/{title}")]
    public async Task<IActionResult> SearchByTitle(string title)
    {
        try
        {
            var books = await _bookService.SearchByTitleAsync(title);
            if (books == null)
            {
                return NotFound($"Book with title {title} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by title {Title}", title);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("author/{author}")]
    public async Task<IActionResult> SearchByAuthor(string author)
    {
        try
        {
            var books = await _bookService.SearchByAuthorAsync(author);
            if (books == null)
            {
                return NotFound($"Book with author {author} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by author {Author}", author);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> SearchByGenre(string genre)
    {
        Console.WriteLine(genre);
        try
        {
            var books = await _bookService.SearchByGenreAsync(genre);
            if (books == null)
            {
                return NotFound($"Book with genre {genre} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by genre {Genre}", genre);
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateBook(int id, BookUpdateDto updateData)
    {
        try
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, updateData);
            if (!updatedBook.Any())
            {
                return NotFound($"Book with id {id} not found");
            }

            return Ok(updatedBook);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating book with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }
}