using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using BackSemillero.Models.Mongo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class ProfesorData : IProfesorData
    {
        private readonly AppDBContext _context;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IOptions<MongoDBSettings> _settings;
        private readonly IMongoCollection<RankingModelMongo> _qrCollection;
        public ProfesorData(AppDBContext context, MongoClient mongoClient, IMongoDatabase mongoDatabase, IOptions<MongoDBSettings> settings)
        {
            _context = context;
            _settings = settings;
            _client = mongoClient;
            _database = mongoDatabase;
            _qrCollection = _database.GetCollection<RankingModelMongo>("Ranking");
        }

        public async Task<ProfesorModel> ConsultarProfesorXCedula(string Cedula)
        {
            var result = await _context.Profesores.FromSqlInterpolated($"exec Consulta_Profesor @Cedula_Docente = {Cedula}").ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<RankingModelMongo> ObtenerDetalleProfesor(string IdEncuesta)
        {
            var result = await _qrCollection.Find(q => q.EncuestaId == IdEncuesta).FirstOrDefaultAsync();
            return result;
        }
    }
}
