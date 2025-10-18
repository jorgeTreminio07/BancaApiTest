using BancaApi.Application;
using BancaApi.Infrastructure;
using BancaApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddInfrastructure(configuration);
builder.Services.AddApplication(configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();  
    
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();

        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Migrations applied successfully and connection verified.");
    }
    catch (Exception ex)
    {
        logger.LogError($"An error occurred while initializing or connecting to the database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
