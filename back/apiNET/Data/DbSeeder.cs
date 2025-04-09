using System.Text.Json;
using apiNET.Models;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Data;

public static class DbSeeder
{
    public static async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        const string GREEN = "\u001b[32m";
        const string RED = "\u001b[31m";
        const string RESET = "\u001b[0m";
        
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
        var envoirment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            await dbContext.Database.MigrateAsync();

            if (await dbContext.Books.AnyAsync())
            {
                logger.LogInformation($"{RED}The database is already contain books. Don't import any data.{RESET}");
                return;
            }
            
            var jsonPath = Path.Combine(envoirment.ContentRootPath, "data", "books.json");
            if (!File.Exists(jsonPath))
            {
                logger.LogWarning($"{RED}Was not found the file json data in the path: {jsonPath} {RESET}");
                return;
            }
            
            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var books = JsonSerializer.Deserialize<List<Book>>(json, options);

            if (books != null && books.Any())
            {
                logger.LogInformation($"{GREEN}Importing {books.Count} books from de json file.{RESET}");
                await dbContext.Books.AddRangeAsync(books);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"{GREEN}Data import successful.{RESET}");
            }
            else
            {
                logger.LogWarning($"{RED}Not found any book to import from file JSON.{RESET}");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{RED}Unexpected error occurred during data import.{RESET}");
        }
    }
}