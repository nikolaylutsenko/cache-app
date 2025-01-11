using Medicine.Database;
using Medicine.Migrations;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry().WithTracing(t => t.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<AppDbContext>(
    "cacheappdb",
    configureDbContextOptions: o =>
        o.UseNpgsql(b => b.MigrationsAssembly(typeof(Program).Assembly.FullName))
);

var host = builder.Build();
host.Run();
