using apiNET.Models;

namespace apiNET.Services.Interfaces;

public interface IBookService
{
    Task<Book> PostBookAsync(Book book);
    Task<IEnumerable<Book>> GetBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> SearchByTitleAsync(string title);
    Task<IEnumerable<Book>> SearchByAuthorAsync(string author);
    Task<IEnumerable<Book>> SearchByGenreAsync(string genre);
}