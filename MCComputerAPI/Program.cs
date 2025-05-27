var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
