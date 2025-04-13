using apiNET.Models;

namespace apiNET.Services.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
    Task<Author> AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
}