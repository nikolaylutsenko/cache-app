var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgresql-password", "password", true);

var postgresdb = builder
    .AddPostgres("postgres", password: postgresPassword, port: 5432)
    //.WithDataVolume("cachedb-pgvolume")
    //.WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin()
    .WithOtlpExporter()
    .AddDatabase("cacheappdb");

builder
    .AddProject<Projects.CacheApp_Migrations>("migrations")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

// looks like I don't need to add Class lib to AppHost
//builder.AddProject<Projects.CacheApp_Database>("database", s => s.ExcludeLaunchProfile = true);

builder
    .AddProject<Projects.CacheApp_Server>("api")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.Build().Run();
