using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Logic;
using WebTerminalsServer.Services;
using WebTerminalsServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")), ServiceLifetime.Singleton);
builder.Services.AddSignalR();
//builder.Services.AddCors();

builder.Services.AddSingleton<IAirPortRepository , AirportRepository>();
builder.Services.AddSingleton<IAirport, Airport>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext?.Database.EnsureDeleted();
    dbContext?.Database.EnsureCreated();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();
app.MapHub<AirportHub>("/airportHub");

app.Run();