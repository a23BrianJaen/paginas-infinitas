using Microsoft.AspNetCore.Mvc;
using apiNET.Services.Interfaces;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;
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

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validación adicional
            if (!bookCreateDto.AuthorId.HasValue && bookCreateDto.NewAuthor == null)
            {
                return BadRequest("Se requiere proporcionar ExistingAuthorId o NewAuthor");
            }

            var newBook = await _bookService.CreateBookAsync(bookCreateDto);
            if (newBook == null)
            {
                return BadRequest("No se pudo crear el libro. Verifica que los datos proporcionados sean válidos.");
            }

            return CreatedAtAction(
                nameof(GetBook),
                new { id = newBook.Id },
                newBook
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new book");
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

    [HttpGet("subGenre/{subGenre}")]
    public async Task<IActionResult> SearchBySubGenre(string subGenre)
    {
        try
        {
            var books = await _bookService.SearchBySubGenreAsync(subGenre);
            if (books == null)
            {
                return NotFound($"Book with genre {subGenre} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by genre {SubGenre}", subGenre);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("tag/{tag}")]
    public async Task<IActionResult> SearchByTag(string tag)
    {
        try
        {
            var books = await _bookService.SearchByTagAsync(tag);
            if (books == null)
            {
                return NotFound($"Book with tag {tag} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by genre {Tag}", tag);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("award/{award}")]
    public async Task<IActionResult> SearchByAward(string award)
    {
        try
        {
            var books = await _bookService.SearchByAwardAsync(award);
            if (books == null)
            {
                return NotFound($"Book with award {award} not found");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for books by genre {Award}", award);
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

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound($"Book with ID {id} not found");
            }

            return Ok(new { message = $"Book with ID {id} deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting book with id {Id}", id);
            return BadRequest(ex.Message);
        }
    }
}