using Microsoft.EntityFrameworkCore;
using VentasDB.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlify",
        policy =>
        {
            policy.WithOrigins("https://proyect-group2deweb.netlify.app", //dominio de Netlify
                             "http://localhost:4200")          //ambiente local
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<VentasDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para manejar solicitudes de favicon
app.MapGet("/favicon.ico", () => Results.NoContent());
app.UseCors("AllowNetlify");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Configura Swagger tambi�n para producci�n si lo deseas
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VentasDB API v1"));
}

app.UseCors("AllowNetlify");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();