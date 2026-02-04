using GaiaProject.API.Extensions;
using GaiaProject.Application.Interfaces;
using GaiaProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
// A34D - Program Entry Point
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "Gaia Project API", 
        Version = "v1",
        Description = "API for performing various operations on two fields - A34D Project"
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<GaiaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IGaiaDbContext>(sp =>
    sp.GetRequiredService<GaiaDbContext>());

builder.Services.AddApplicationServices(builder.Configuration);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gaia Project API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at root
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<GaiaDbContext>();
        
        // Ensure database is created and apply migrations
        context.Database.EnsureCreated();
        
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Database initialized successfully - A34D");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database");
    }
}

app.Run();
