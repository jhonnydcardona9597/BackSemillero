using BackSemillero.Business;
using BackSemillero.Business.Interfaces;
using BackSemillero.Data;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

builder.Services.AddControllers();
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
