using Microsoft.EntityFrameworkCore;
using Nexus.Application.Interfaces;
using Nexus.Application.Mappings;
using Nexus.Application.Services;
using Nexus.Domain.Interfaces;
using Nexus.Infrastructure.Data;
using Nexus.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NexusDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IJogoRepositorio, JogoRepositorio>();
builder.Services.AddScoped<IFilmeRepositorio, FilmeRepositorio>();
builder.Services.AddScoped<IGeneroRepositorio, GeneroRepositorio>();
builder.Services.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();

builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IFilmeService, FilmeService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<NexusDbContext>();
    DataSeeder.Seed(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();