﻿using BackSemillero.Business.Interfaces;
using BackSemillero.Data;
using BackSemillero.Data.Interfaces;
using BackSemillero.Models;

namespace BackSemillero.Business
{
    public class AsignaturaBusiness : IAsignaturaBusiness
    {
        private readonly IAsignaturaData _asignaturaData;
        public AsignaturaBusiness(IAsignaturaData asignaturaData)
        {
            _asignaturaData = asignaturaData;
        }

        public async Task<List<AsignaturaModelResponse>> ConsultaAsignaturasXPrograma(int IdPrograma)
        {
            List<AsignaturaModelResponse> listAsignaturaModelResponse = await _asignaturaData.ConsultaAsignaturasXPrograma(IdPrograma);
            if (listAsignaturaModelResponse.Count > 0)
            {
                return listAsignaturaModelResponse;
            }
            else
            {
                throw new Exception("No hay asignaturas para ese programa", new Exception("404"));
            }
        }
    }
}
