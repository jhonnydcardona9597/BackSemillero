using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoListApi.Context;

namespace BackSemillero.Data
{
    public class ParametrizacionData: IParametrizacionData
    {
        private readonly AppDBContext _context;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IOptions<MongoDBSettings> _settings;
        private readonly IMongoCollection<QrModelRequest> _qrCollection;
        public ParametrizacionData(AppDBContext context, MongoClient mongoClient, IMongoDatabase mongoDatabase, IOptions<MongoDBSettings> settings)
        {
            _context = context;
            _settings = settings;
            _client = mongoClient;
            _database = mongoDatabase;
            _qrCollection = _database.GetCollection<QrModelRequest>("qr_registros");

        }

        public async Task<ProfesorModel> ConsultarProfesorXCedula(string Cedula)
        {
            return _context.Profesores.FromSqlInterpolated($"select * from profesor WHERE idprofesor = '{Cedula}'").FirstOrDefault();
        }
        
        public async Task<QrModelResponse> CrearRegistroQr(QrModelRequest qrModelRequest)
        {
            await _qrCollection.InsertOneAsync(qrModelRequest);
            return new QrModelResponse();
        }
    }
}
