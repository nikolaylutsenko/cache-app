var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CacheApp_Server>("cacheapp-server");

builder.Build().Run();
