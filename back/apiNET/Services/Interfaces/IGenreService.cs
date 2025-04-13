using apiNET.Models;
namespace apiNET.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetGenresAsync();
    Task<Genre> GetGenreByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
}