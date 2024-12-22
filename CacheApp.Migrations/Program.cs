using CacheApp.Database;
using CacheApp.Migrations;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry().WithTracing(t => t.AddSource(Worker.ActivitySourceName));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("CacheApp.Migrations"))
);

var host = builder.Build();
host.Run();
