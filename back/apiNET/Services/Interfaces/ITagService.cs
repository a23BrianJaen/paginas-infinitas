using apiNET.Models;

namespace apiNET.Services.Interfaces;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetTagsAsync();
    Task<Tag> GetTagByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByTagAsync(int tagId);
}