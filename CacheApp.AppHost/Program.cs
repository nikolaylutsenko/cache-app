using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgresdb = builder
    .AddPostgres("postgres")
    .WithDataVolume("pgvolume")
    //.WithEnvironment("POSTGRES_DB", "backendDB")
    //.WithBindMount("../Service.API/Seed", "/docker-entrypoint-initdb.d")
    .WithPgAdmin()
    .AddDatabase("cacheappdb");

builder.AddProject<Projects.CacheApp_Server>("cacheapp-server").WithReference(postgresdb);

//builder
//    .AddProject<Projects.CacheApp_Database>("cacheapp-database")
//    .WithReference(postgresdb);

builder.Build().Run();
