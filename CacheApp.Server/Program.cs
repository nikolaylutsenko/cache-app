using CacheApp.Database;
using CacheApp.Database.Enteties;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
//builder.AddNpgsqlDataSource("postgresdb"); // no need for managing it in appsettings
builder.AddNpgsqlDbContext<AppDbContext>("cacheappdb");
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    // Retrieve an instance of the DbContext class and manually run migrations during development
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        if (!context.Tags.Any())
        {
            var tags = Enumerable
                .Range(1, 1000)
                .Select(x => new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = $"Tag-{Guid.NewGuid()}",
                    Version = 1L,
                });

            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }
}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
