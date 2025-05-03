using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Data
{
    public class ProgramaData: IProgramaData
    {
        public ProgramaData()
        {

        }

        public List<ProgramaModel> ConsultarPrograma()
        {
            return new List<ProgramaModel>();
        }
    }
}
