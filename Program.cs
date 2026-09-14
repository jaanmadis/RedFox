using Microsoft.EntityFrameworkCore;
//using System.Text.Json;
using RedFox.Data;
using RedFox.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<RedFoxDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<MessengerImporter>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();

app.MapGet("/hello", () =>
{
    return new 
    { 
        Message = "Hello from RedFox",
        Time = DateTime.Now,
    };
});

app.MapGet("/hello2", async (RedFoxDbContext db) =>
{
	var messages = await db.Messages
        .OrderByDescending(m => m.Timestamp)
        .Take(2)
        .ToListAsync();

    return Results.Ok(messages);
});

app.MapPost("/upload", async (HttpRequest request, MessengerImporter importer) =>
{
    var form = await request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();

    if (file == null)
    {
        return Results.BadRequest("No file uploaded.");
    }

    var imported = await importer.ImportAsync(file.OpenReadStream());

    return Results.Ok(new
    {
        Imported = imported,
        Time = DateTime.Now,
    });
});

app.Run();