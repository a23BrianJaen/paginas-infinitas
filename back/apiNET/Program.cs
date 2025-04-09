using apiNET.Services;
using apiNET.Services.Interfaces;
using apiNET.Data;
using apiNET.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

// Load env vars
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Config services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service register injection 
builder.Services.AddScoped<IBookService, BookService>();

// MySQL Server register
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseMySql(Environment.GetEnvironmentVariable("MYSQL_CONN"),
        ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("MYSQL_CONN"))));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // In development
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
        else
        {
            // In production
            policy.WithOrigins("https://tudominio.com")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});

var app = builder.Build();


// Execute Seder
if (app.Environment.IsDevelopment())
{
    await DbSeeder.SeedDatabase(app.Services);
}

// Config middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Create a group for all book routes with the prefix "/api/books"
var booksGroup = app.MapGroup("/api/books")
    .WithTags("Books"); //Add tag in Swagger for this group
    // .RequireAuthorization(); // When requiere authorization with all routes

// Route root to get all books
booksGroup.MapGet("", async (IBookService bookService) =>
    {
        var books = await bookService.GetBooksAsync();

        if (!books.Any())
        {
            return Results.NotFound("No books were found in the database");
        }

        return Results.Ok(books);
    })
    .WithName("GetBooks")
    .WithOpenApi();

// Post book
booksGroup.MapPost("", async (Book book, IBookService bookService) =>
    {
        var newBook = await bookService.PostBookAsync(book);
        return Results.CreatedAtRoute("GetBookById", new { id = newBook.Id }, newBook);
    })
    .WithName("AddBook")
    .WithOpenApi();

// Get book by ID
booksGroup.MapGet("{id}", async (int id, IBookService bookService) =>
    {
        var book = await bookService.GetBookByIdAsync(id);

        if (book == null)
            return Results.NotFound($"Book with ID {id} not found or an error occurred");

        return Results.Ok(book);
    })
    .WithName("GetBookById")
    .WithOpenApi();

// Get book by title
booksGroup.MapGet("search/{title}", async (string title, IBookService bookService) =>
    {
        var books = await bookService.SearchByTitleAsync(title);
        if (!books.Any())
            return Results.NotFound($"No books found with title containing '{title}'");
        return Results.Ok(books);
    })
    .WithName("SearchByTitle")
    .WithOpenApi();

// Get book by author
booksGroup.MapGet("author/{author}", async (string author, IBookService bookService) =>
    {
        var books = await bookService.SearchByAuthorAsync(author);
        if (!books.Any())
            return Results.NotFound($"No books found for author '{author}'");
        return Results.Ok(books);
    })
    .WithName("SearchByAuthor")
    .WithOpenApi();

// Get book by genre
booksGroup.MapGet("genre/{genre}", async (string genre, IBookService bookService) =>
    {
        var books = await bookService.SearchByGenreAsync(genre);
        if (!books.Any())
            return Results.NotFound($"No books found for genre '{genre}'");
        return Results.Ok(books);
    })
    .WithName("SearchByGenre")
    .WithOpenApi();

app.Run();