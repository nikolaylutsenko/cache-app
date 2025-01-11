var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgresql-password", "password", true);

var postgresdb = builder
    .AddPostgres("postgres", password: postgresPassword, port: 5432)
    //TODO: uncomment when every model will be ready for longterm storing
    //.WithDataVolume("cachedb-pgvolume")
    //.WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin()
    .WithOtlpExporter()
    .AddDatabase("cacheappdb");

var migration = builder
    .AddProject<Projects.Medicine_Migrations>("migrations")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

// looks like I don't need to add Class lib to AppHost
//builder.AddProject<Projects.CacheApp_Database>("database", s => s.ExcludeLaunchProfile = true);

var api = builder
    .AddProject<Projects.Medicine_Api>("api")
    .WithReference(postgresdb)
    .WaitFor(postgresdb)
    .WaitForCompletion(migration)
    .WithExternalHttpEndpoints();

builder
    .AddNpmApp("angular", "../cacheapp.client")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
