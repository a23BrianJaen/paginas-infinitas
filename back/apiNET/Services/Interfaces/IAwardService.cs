using apiNET.Models;

namespace apiNET.Services.Interfaces;

public interface IAwardService
{
    Task<IEnumerable<Award>> GetAwardsAsync();
    Task<Award> GetAwardByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByAwardAsync(int awardId);
}