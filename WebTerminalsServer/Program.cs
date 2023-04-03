using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebTerminalsServer.Dal;
using WebTerminalsServer.Hubs;
using WebTerminalsServer.Repositories;
using WebTerminalsServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
        });
});
//builder.Services.AddDbContext<DataContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")), ServiceLifetime.Scoped/*, ServiceLifetime.Singleton*/);
builder.Services.AddSignalR();
//builder.Services.AddCors();

builder.Services.AddScoped<IAirPortRepository , AirportRepository>();
//builder.Services.AddSingleton<IAirPortRepository , AirportRepository>();
//builder.Services.AddSingleton<IAirport, Airport>();
builder.Services.AddScoped<IAirportService, AirportService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            var errorResponse = new
            {
                message = exception.Message,
                error = exception.GetType().Name
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        });
    });
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