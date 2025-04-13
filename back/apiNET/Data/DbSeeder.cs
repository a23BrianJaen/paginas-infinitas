using System.Text.Json;
using apiNET.DTOs.ResponseDtos;
using apiNET.Models;
using Microsoft.EntityFrameworkCore;

namespace apiNET.Data;

public static class DbSeeder
{
    private const string GREEN = "\u001b[32m";
    private const string RED = "\u001b[31m";
    private const string RESET = "\u001b[0m";

    public static async Task SeedDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
        var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            // Ensure database and migrations are up to date
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.MigrateAsync();

            // Check if data already exists
            if (await dbContext.Books.AnyAsync())
            {
                logger.LogInformation($"{RED}The database already contains books. Skipping import.{RESET}");
                return;
            }

            // Read and validate JSON file
            var jsonPath = Path.Combine(environment.ContentRootPath, "Data", "books.json");
            if (!File.Exists(jsonPath))
            {
                logger.LogWarning($"{RED}JSON file not found at path: {jsonPath}{RESET}");
                return;
            }

            // Parse JSON with robust options
            var json = await File.ReadAllTextAsync(jsonPath);
            var bookDtos = DeserializeJson(json, logger);

            if (bookDtos == null || !bookDtos.Any())
            {
                logger.LogWarning($"{RED}No books found in JSON file.{RESET}");
                return;
            }

            logger.LogInformation($"{GREEN}Starting import of {bookDtos.Count} books...{RESET}");

            // Create lookup dictionaries for better performance
            var authorDict = new Dictionary<string, Author>();
            var genreDict = new Dictionary<string, Genre>();
            var subGenreDict = new Dictionary<string, SubGenre>();
            var tagDict = new Dictionary<string, Tag>();
            var awardDict = new Dictionary<string, Award>();
            
            foreach (var bookDto in bookDtos)
            {
                try
                {
                    // Get or create the author
                    if (!authorDict.TryGetValue(bookDto.Author.Name, out var author))
                    {
                        author = await GetOrCreateAuthor(dbContext, bookDto);
                        authorDict[bookDto.Author.Name] = author;
                    }

                    // Get or create the genre
                    if (!genreDict.TryGetValue(bookDto.Genre.Name, out var genre))
                    {
                        genre = await GetOrCreateGenre(dbContext, bookDto.Genre.Name);
                        genreDict[bookDto.Genre.Name] = genre;
                    }

                    // Create the book
                    // Create Book
                    var book = new Book
                    {
                        Title = bookDto.Title,
                        Year = bookDto.Year,
                        ISBN = bookDto.ISBN,
                        CoverImage = bookDto.CoverImage,
                        Publisher = bookDto.Publisher,
                        Language = bookDto.Language,
                        PageCount = bookDto.PageCount,
                        Format = bookDto.Format,
                        Price = bookDto.Price,
                        Currency = bookDto.Currency,
                        InStock = bookDto.InStock,
                        Rating = bookDto.Rating,
                        ReviewCount = bookDto.ReviewCount,
                        Synopsis = bookDto.Synopsis,
                        TargetAudience = bookDto.TargetAudience,
                        ReadingTime = bookDto.ReadingTime,
                        // AuthorBio = bookDto.AuthorBio,
                        // AuthorImage = bookDto.AuthorImage,
                        PublicationDate = bookDto.PublicationDate,
                        Edition = bookDto.Edition,
                        Dimensions = bookDto.Dimensions,
                        Weight = bookDto.Weight,
                        SalesRank = bookDto.SalesRank,
                        MaturityRating = bookDto.MaturityRating,
                        Series = bookDto.Series,
                        SeriesOrder = bookDto.SeriesOrder,
                        TableOfContents = bookDto.TableOfContents,
                        FileSize = bookDto.FileSize,
                        WordCount = bookDto.WordCount,
                        Author = author,
                        Genre = genre
                    };

                    dbContext.Books.Add(book);
                    await dbContext.SaveChangesAsync();

                    // Process relationships in batches
                    await ProcessRelationships(dbContext, book, bookDto, subGenreDict, tagDict, awardDict);
                    
                    logger.LogInformation($"{GREEN}Successfully imported book: {bookDto.Title}{RESET}");
                }
                catch (Exception ex)
                {
                    logger.LogError($"{RED}Error importing book {bookDto.Title}: {ex.Message}{RESET}");
                    // Continue with next book instead of stopping the entire process
                    continue;
                }
            }

            await dbContext.SaveChangesAsync();
            logger.LogInformation($"{GREEN}Data import completed successfully.{RESET}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{RED}Unexpected error occurred during data import.{RESET}");
            throw;
        }
    }

    private static List<BookResponseDto>? DeserializeJson(string json, ILogger logger)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            return JsonSerializer.Deserialize<List<BookResponseDto>>(json, options);
        }
        catch (JsonException ex)
        {
            logger.LogError($"{RED}Error deserializing JSON: {ex.Message}{RESET}");
            return null;
        }
    }

    private static async Task ProcessRelationships(
        BookDbContext dbContext,
        Book book,
        BookResponseDto bookDto,
        Dictionary<string, SubGenre> subGenreDict,
        Dictionary<string, Tag> tagDict,
        Dictionary<string, Award> awardDict)
    {
        // Process SubGenres
        if (bookDto.SubGenre?.Any() == true)
        {
            foreach (var subGenreName in bookDto.SubGenre)
            {
                if (!subGenreDict.TryGetValue(subGenreName.Name, out var subGenre))
                {
                    subGenre = await GetOrCreateSubGenre(dbContext, subGenreName.Name);
                    subGenreDict[subGenreName.Name] = subGenre;
                }

                dbContext.BookSubGenres.Add(new BookSubGenre
                {
                    Book = book,
                    SubGenre = subGenre
                });
            }
        }

        // Process Tags
        if (bookDto.Tags?.Any() == true)
        {
            foreach (var tagName in bookDto.Tags)
            {
                if (!tagDict.TryGetValue(tagName.Name, out var tag))
                {
                    tag = await GetOrCreateTag(dbContext, tagName.Name);
                    tagDict[tagName.Name] = tag;
                }

                dbContext.BookTags.Add(new BookTag
                {
                    Book = book,
                    Tag = tag
                });
            }
        }

        // Process Awards
        if (bookDto.Awards?.Any() == true)
        {
            foreach (var awardName in bookDto.Awards)
            {
                if (!awardDict.TryGetValue(awardName.Name, out var award))
                {
                    award = await GetOrCreateAward(dbContext, awardName.Name);
                    awardDict[awardName.Name] = award;
                }

                dbContext.BookAwards.Add(new BookAward
                {
                    Book = book,
                    Award = award
                });
            }
        }
    }

    private static async Task<Author> GetOrCreateAuthor(BookDbContext dbContext, BookResponseDto bookDto)
    {
        // Buscar autor existente por nombre
        var author = await dbContext.Authors
            .FirstOrDefaultAsync(a => a.Name == bookDto.Author.Name);

        if (author == null)
        {
            // Crear nuevo autor sin especificar ID
            author = new Author
            {
                Name = bookDto.Author.Name,
                Bio = bookDto.Author.Bio ?? "",
                ImageUrl = bookDto.Author.ImageUrl ?? ""
            };
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();
        }

        return author;
    }

    private static async Task<Genre> GetOrCreateGenre(BookDbContext dbContext, string genreName)
    {
        var genre = await dbContext.Genres
            .FirstOrDefaultAsync(g => g.Name == genreName);

        if (genre == null)
        {
            genre = new Genre { Name = genreName };
            dbContext.Genres.Add(genre);
            await dbContext.SaveChangesAsync();
        }

        return genre;
    }

    private static async Task<SubGenre> GetOrCreateSubGenre(BookDbContext dbContext, string subGenreName)
    {
        var subGenre = await dbContext.SubGenres
            .FirstOrDefaultAsync(sg => sg.Name == subGenreName);

        if (subGenre == null)
        {
            subGenre = new SubGenre { Name = subGenreName };
            dbContext.SubGenres.Add(subGenre);
            await dbContext.SaveChangesAsync();
        }

        return subGenre;
    }

    private static async Task<Tag> GetOrCreateTag(BookDbContext dbContext, string tagName)
    {
        var tag = await dbContext.Tags
            .FirstOrDefaultAsync(t => t.Name == tagName);

        if (tag == null)
        {
            tag = new Tag { Name = tagName };
            dbContext.Tags.Add(tag);
            await dbContext.SaveChangesAsync();
        }

        return tag;
    }

    private static async Task<Award> GetOrCreateAward(BookDbContext dbContext, string awardName)
    {
        var award = await dbContext.Awards
            .FirstOrDefaultAsync(a => a.Name == awardName);

        if (award == null)
        {
            award = new Award { Name = awardName };
            dbContext.Awards.Add(award);
            await dbContext.SaveChangesAsync();
        }

        return award;
    }
}