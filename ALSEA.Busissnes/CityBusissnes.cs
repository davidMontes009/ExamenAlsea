using ALSEA.DATA;
using ALSEA.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALSEA.Busissnes
{
    public class CityBusissnes
    {
        private readonly CityData _cityData;

        public CityBusissnes(CityData cityData)
        {
            this._cityData = cityData;
        }

        /// <summary>
        /// Consulta cuidades 
        /// </summary>
        /// <returns>Retorna lista de cuidades </returns>
        public async Task<IList<CitysDTO>> ConsultCities()
            => await _cityData.ConsultCities();

        /// <summary>
        /// Guarda una ciudad del usuario
        /// </summary>
        /// <param name="cityId">id de la ciudad</param>
        /// <param name="userId">id del usuario</param>
        /// <returns>retorna mensaje de respuesta</returns>
        public async Task<CitysDTO> SaveCitiesUser(int cityId, int userId)
            => await _cityData.SaveCitiesUser(cityId,userId);

        /// <summary>
        /// Cosulta lista de ciuidades por usuario
        /// </summary>
        /// <param name="userId">id del usuario</param>
        /// <returns>Retorna lista de ciudades</returns>
        public async Task<IList<CitysDTO>> ConsultUserCities(int userId)
            => await _cityData.ConsultUserCities(userId);

        /// <summary>
        /// Consultar ciudades del usuario
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CitysDTO> DeleteCituByUser(int cityId, int userId)
            => await _cityData.DeleteCituByUser(cityId,userId);
    }
}
