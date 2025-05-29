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
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.Configure<JwtSettingsDTOs>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<JwtSettingsHelper>(sp =>
{
    var jwtOptions = sp.GetRequiredService<IOptions<JwtSettingsDTOs>>().Value;
    return new JwtSettingsHelper(jwtOptions);
});
builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUsersRepositories, UsersRepositories>();
builder.Services.AddScoped<ICustomersRepositories, CustomersRepositories>();
builder.Services.AddScoped<IInvoicesRepositories, InvoicesRepositories>();
builder.Services.AddScoped<IProductsRepositories, ProductRepositories>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
