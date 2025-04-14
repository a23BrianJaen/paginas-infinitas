using apiNET.Models;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;
using apiNET.DTOs.CreateDtos;

namespace apiNET.Services.Interfaces;

public interface IBookService
{
    Task<BookResponseDto> CreateBookAsync(BookCreateDto bookCreateDto);
    Task<IEnumerable<BookResponseDto>> GetBooksAsync();
    Task<BookResponseDto> GetBookByIdAsync(int id);
    Task<IEnumerable<BookResponseDto>> SearchByTitleAsync(string title);
    Task<IEnumerable<BookResponseDto>> SearchByAuthorAsync(string author);
    Task<IEnumerable<BookResponseDto>> SearchByGenreAsync(string genre);
    Task<IEnumerable<BookResponseDto>> SearchBySubGenreAsync(string subGenre);
    Task<IEnumerable<BookResponseDto>> SearchByTagAsync(string tag);
    
    Task<IEnumerable<BookResponseDto>> SearchByAwardAsync(string award);
    Task<IEnumerable<BookResponseDto>> UpdateBookAsync(int id, BookUpdateDto updateData);
    Task<bool> DeleteBookAsync(int id);
}