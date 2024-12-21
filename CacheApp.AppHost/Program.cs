var builder = DistributedApplication.CreateBuilder(args);

//var username = builder.AddParameter("username", secret: true);
//var password = builder.AddParameter("password", secret: true);

var postgresdb = builder
    .AddPostgres("postgres")
    .WithDataVolume("pgvolume")
    //.WithEnvironment("POSTGRES_DB", "backendDB")
    //.WithBindMount("../Service.API/Seed", "/docker-entrypoint-initdb.d")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin()
    .AddDatabase("cacheappdb");

builder.AddProject<Projects.CacheApp_Database>(
    "cacheapp-database",
    config => config.ExcludeLaunchProfile = true
);

builder.AddProject<Projects.CacheApp_Server>("cacheapp-server").WithReference(postgresdb);

//builder
//    .AddProject<Projects.CacheApp_Database>("cacheapp-database")
//    .WithReference(postgresdb);

builder.Build().Run();
