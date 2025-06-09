using BackSemillero.Business;
using BackSemillero.Business.Interfaces;
using BackSemillero.Data;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoListApi.Context;

var builder = WebApplication.CreateBuilder(args);


//Configuracion del cors
builder.Services.AddCors(options => {
    options.AddPolicy("MyCORS", policy => {
        policy.AllowAnyOrigin() // O elige origen específico
             .AllowAnyMethod()
             .AllowAnyHeader(); // O especifica encabezados específicos
    });
});

// Add services to the container.


var connetionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connetionString));

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<MongoClient>(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<MongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<IParametrizacionData, ParametrizacionData>();
builder.Services.AddScoped<IParametrizacionBusiness, ParametrizacionBusiness>();
// Capa Data
builder.Services.AddScoped<IEstudianteData, EstudianteData>();
builder.Services.AddScoped<IAsistenciaData, AsistenciaData>();
builder.Services.AddScoped<IAsistenciaBusiness, AsistenciaBusiness>();

builder.Services.AddScoped<IAsignaturaData, AsignaturaData>();
builder.Services.AddScoped<IAsignaturaBusiness, AsignaturaBusiness>();

builder.Services.AddScoped<IProgramaData, ProgramaData>();
builder.Services.AddScoped<IProgramaBusiness, ProgramaBusiness>();

builder.Services.AddScoped<IProfesorData, ProfesorData>();
builder.Services.AddScoped<IProfesorBusiness, ProfesorBusiness>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyCORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
