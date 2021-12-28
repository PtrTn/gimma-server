using System.Reflection;
using Gimma.CommandHandlers;
using Gimma.Dispatchers;
using Gimma.Hubs;
using Gimma.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Gimma websocket API", Version = "v1" });
    options.DocumentFilter<SignalRSwaggerGen.SignalRSwaggerGen>(new List<Assembly> { typeof(GameHub).Assembly });
});
builder.Services.AddTransient<RandomStringRepository, RandomStringRepository>();
builder.Services.AddTransient<EventDispatcher, EventDispatcher>();
builder.Services.AddTransient<CreateGameCommandHandler, CreateGameCommandHandler>();
builder.Services.AddTransient<JoinGameCommandHandler, JoinGameCommandHandler>();
builder.Services.AddTransient<StartGameCommandHandler, StartGameCommandHandler>();
builder.Services.AddSingleton<GameRepository, GameRepository>();

var app = builder.Build();

app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod();
    o.WithOrigins(new []{"http://localhost:3000"});
    o.AllowCredentials();
    o.SetIsOriginAllowed((string host) =>
    {
        return true;
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapHub<GameHub>("/game");

app.Run();