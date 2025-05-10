using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class AsistenciaData : IAsistenciaData
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<AsistenciaModelMongo> _asistenciaCollection;

        public AsistenciaData(IMongoDatabase mongoDatabase, IOptions<MongoDBSettings> settings)
        {
            _database = mongoDatabase;
            _asistenciaCollection = _database.GetCollection<AsistenciaModelMongo>("Asistencia");
        }

        public async Task<AsistenciaModelResponse> CrearRegistroAsistencia(AsistenciaModelMongo asistenciaModelMongo)
        {

            // 2) Insert en Mongo (colección Asistencias)
           
            await _asistenciaCollection.InsertOneAsync(asistenciaModelMongo);

            // 3) Devuelves tu respuesta combinada
 
            return new AsistenciaModelResponse
            {
                Exito = true
            };
        }
    }
}
