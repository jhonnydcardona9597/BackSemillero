using BackSemillero.Business.Interfaces;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Business
{
    public class ProgramaBusiness: IProgramaBusiness
    {

        private readonly IProgramaData _programaData;


        public ProgramaBusiness(IProgramaData programaData)
        {
            _programaData = programaData;
        }

        public async Task<List<ProgramaModel>> ConsultarPrograma()
        {
            return await _programaData.ConsultarPrograma();
        }
    }
}
