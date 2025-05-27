using GymAutomatBackgroundService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<GymAutomat>();
var app = builder.Build();

app.Run();