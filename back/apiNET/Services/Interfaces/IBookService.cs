using apiNET.Models;
using apiNET.DTOs.ResponseDtos;

namespace apiNET.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponseDto>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> SearchByTitleAsync(string title);
    Task<IEnumerable<Book>> SearchByAuthorAsync(string author);
    Task<IEnumerable<Book>> SearchByGenreAsync(string genre);
    Task<Book> PostBookAsync(Book book);
    // Task UpdateBookAsync(Book book);
    // Task DeleteBookAsync(int id);
}