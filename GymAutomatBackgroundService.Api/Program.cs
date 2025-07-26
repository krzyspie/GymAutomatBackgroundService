using GymAutomatBackgroundService.Api;
using GymAutomatBackgroundService.Api.HttpClients;
using GymAutomatBackgroundService.Api.Services;
using GymAutomatBackgroundService.Api.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IGymClient, GymClient>( client =>
{
    client.BaseAddress = new Uri("https://justgym.pl/");
});

builder.Services.AddSingleton<IDelayCalculator, DelayCalculator>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IGymAccessService, GymAccessService>();
builder.Services.AddSingleton<IGymWorkoutService, GymWorkoutService>();
builder.Services.AddSingleton<IRequestDataFactory, RequestDataFactory>();
builder.Services.AddSingleton<ISecretsManagerService, SecretsManagerService>();
builder.Services.AddSingleton<IJogaWorkoutService, JogaWorkoutService>();

builder.Services.AddHostedService<GymAutomat>();

var app = builder.Build();

app.MapGet("/", () => "Hello Guest!");

app.Run();