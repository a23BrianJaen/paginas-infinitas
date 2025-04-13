using System.Text.Json.Serialization;
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

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddScoped<IBookService, BookService>();

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

// app.UseHttpsRedirection();

// Enable endpoint routing
app.MapControllers();

app.Run();