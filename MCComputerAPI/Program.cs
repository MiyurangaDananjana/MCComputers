using MCComputerAPI.Data;
using MCComputerAPI.Data.Implementations;
using MCComputerAPI.Data.Interfaces;
using MCComputerAPI.Helpers;
using MCComputerAPI.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog Logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Add more origins as needed
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure strongly typed JwtSettings
builder.Services.Configure<JwtSettingsDTOs>(builder.Configuration.GetSection("JwtSettings"));

// Register JwtSettingsHelper
builder.Services.AddSingleton<JwtSettingsHelper>(sp =>
{
    var jwtOptions = sp.GetRequiredService<IOptions<JwtSettingsDTOs>>().Value;
    return new JwtSettingsHelper(jwtOptions);
});

// Safe JWT configuration using IConfigureOptions
builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtOptions>();

// Authentication & Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer();

builder.Services.AddAuthorization();

// Register Repositories
builder.Services.AddScoped<IUsersRepositories, UsersRepositories>();
builder.Services.AddScoped<ICustomersRepositories, CustomersRepositories>();
builder.Services.AddScoped<IInvoicesRepositories, InvoicesRepositories>();
builder.Services.AddScoped<IProductsRepositories, ProductRepositories>();

// Configure EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Controllers & Swagger support if needed
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Optional: Swagger Gen goes here if needed

var app = builder.Build();

// Middleware Pipeline
app.UseCors("AllowAllOrigins");
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
