using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;

namespace apiNET.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponseDto>> GetBooksAsync();
    Task<BookResponseDto> GetBookByIdAsync(int id);
    Task<IEnumerable<BookResponseDto>> SearchByTitleAsync(string title);
    Task<IEnumerable<BookResponseDto>> SearchByAuthorAsync(string author);
    Task<IEnumerable<BookResponseDto>> SearchByGenreAsync(string genre);

    Task<Book> PostBookAsync(Book book);

    Task<IEnumerable<BookResponseDto>> UpdateBookAsync(int id, BookUpdateDto updateData);
    // Task DeleteBookAsync(int id);
}