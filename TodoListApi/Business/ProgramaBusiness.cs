using BackSemillero.Business.Interfaces;
using BackSemillero.Data;
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

        //public async Task<List<ProgramaModel>> ConsultarPrograma()
        //{
        //    return await _programaData.ConsultarPrograma();
        //}
        public async Task<List<ProgramaModel>> ConsultarPrograma()
        {
            List<ProgramaModel> programaModels = await _programaData.ConsultarPrograma();
            if (programaModels.Count > 0)
            {
                return programaModels;
            }
            else
            {
                throw new Exception("No existen programas");
            }
        }
    }
}
