using centro_estudiantes.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("http://localhost:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

builder.Services.AddDbContext<DataContext>(options => {
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)), // <-- Especifica la versión de tu servidor MySQL
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null)
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("api-cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
