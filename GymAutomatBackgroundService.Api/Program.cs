using GymAutomatBackgroundService.Api;
using GymAutomatBackgroundService.Api.HttpClients;
using GymAutomatBackgroundService.Api.Services;
using GymAutomatBackgroundService.Api.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GymClient>( client =>
{
    client.BaseAddress = new Uri("https://justgym.pl/");
});
builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IGymAccessService, GymAccessService>();
builder.Services.AddSingleton<IGymWorkoutService, GymWorkoutService>();

builder.Services.AddHostedService<GymAutomat>();

var app = builder.Build();

app.Run();