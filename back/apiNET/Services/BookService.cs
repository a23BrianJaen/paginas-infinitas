using apiNET.Data;
using apiNET.Models;
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

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        try
        {
            var books = await _context.Books
                .ToListAsync();
            return books;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los libros");
            return Enumerable.Empty<Book>();
        }
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        try
        {
            return _context.Books.FirstOrDefault(book => book.Id == id);
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