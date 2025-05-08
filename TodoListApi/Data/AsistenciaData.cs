using System.Threading.Tasks;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BackSemillero.Data
{
    public class AsistenciaData : IAsistenciaData
    {
        private readonly IMongoCollection<AsistenciaModelRequest> _asistenciaCollection;

        public AsistenciaData(IMongoDatabase db, IOptions<MongoDBSettings> settings)
        {
            _asistenciaCollection = db.GetCollection<AsistenciaModelRequest>("asistencias");
        }

        public async Task<AsistenciaResponse> CrearRegistroAsistencia(AsistenciaModelRequest asistenciaModelRequest)
        {
            await _asistenciaCollection.InsertOneAsync(asistenciaModelRequest);
            return new AsistenciaResponse
            {
                Registrada = true,
                Mensaje = "Asistencia guardada.",
                AsistenciaGuardada = asistenciaModelRequest
            };
        }
    }
}
