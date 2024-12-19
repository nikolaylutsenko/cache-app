var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Cache_App_Server>("cache-app-server");

builder.Build().Run();
