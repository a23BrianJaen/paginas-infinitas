using apiNET.DTOs.CreateDtos;
using apiNET.Models;
using apiNET.DTOs.ResponseDtos.Operation;
using apiNET.DTOs.ResponseDtos;
using apiNET.DTOs.UpdateDtos;

namespace apiNET.Services.Interfaces;

public interface IGenreService
{
    Task<GenreOperationResponseDto> CreateGenreAsync(GenreCreateDto genre);
    Task<IEnumerable<GenreResponseDto>> GetGenresAsync();
    Task<GenreResponseDto> GetGenreByIdAsync(int id);
    Task<IEnumerable<BookResponseDto>> GetBooksByGenreAsync(int genreId);
    Task<IEnumerable<GenreResponseDto>> UpdateGenreAsync(int id, GenreUpdateDto updateGenre);
    Task<bool> DeleteGenreAsync(int id);
    Task<IEnumerable<GenreResponseDto>> SearchByGenreAsync(string genre);
}