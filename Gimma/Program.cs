using Gimma.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(o =>
{
    o.AllowAnyHeader();
    o.AllowAnyMethod();
    o.WithOrigins(new []{"http://localhost:3000", "https://localhost:3000", "https://localhost:7212"});
    o.AllowCredentials();
    o.SetIsOriginAllowed((string host) =>
    {
        return true;
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
   // app.UseHsts();
}


app.MapControllers();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapHub<GameHub>("/chatHub");

app.Run();