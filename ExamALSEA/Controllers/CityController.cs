using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALSEA.Busissnes;
using ALSEA.Entities;
using ExamALSEA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamALSEA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> logger;
        private readonly CityBusissnes _cityBusissnes;

        public CityController(ILogger<CityController> _logger, CityBusissnes cityBusissnes)
        {
            this.logger = _logger;
            this._cityBusissnes = cityBusissnes;
        }

        /// <summary>
        /// Servicio para registrar una ciudad a un usuario
        /// </summary>
        /// <param name="cityByUser">MOdelo con la informacion del usuario</param>
        /// <returns>Retorna estatus y mensaje </returns>
        /// <remarks>
        /// Sample Request
        /// 
        ///     POST
        ///     {
        ///      "userId": 1,
        ///      "cityId": 173869
        ///     }
        ///
        /// Sample Response
        ///
        ///     {
        ///      "status": true,
        ///      "message": "La ciudad se registro con exito"
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("SaveCitiesUser")]
        public async Task<IActionResult> SaveCitiesUser(CityByUserViewModel cityByUser)
        {
            try
            {
                CitysDTO citys = await _cityBusissnes.SaveCitiesUser(cityByUser.cityId, cityByUser.userId);
                if (citys.Validations.Count > 0)
                    return BadRequest(new { status = false, message = citys.Validations[0].ErrorMessage });

                return Ok(new { status = true, message = "La ciudad se registro con exito" });
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Api para conusltar las ciudades disponibles 
        /// </summary>
        /// <returns>Retorna lista de ciudades </returns>
        /// <remarks>
        /// Sample request
        ///
        ///     GET
        ///     El servicio no requiere parametros
        ///
        /// Sample Response
        ///
        ///     [
        ///      {
        ///       "cityId": 173869,
        ///       "nameCity": null,
        ///       "state": "Al Ghizlaniyah",
        ///       "country": "",
        ///       "coordLon": "SY",
        ///       "coordLat": "36.456169",
        ///       "validations": []
        ///      },
        ///      {
        ///       "cityId": 3530,
        ///       "nameCity": null,
        ///       "state": "Qabaghlu",
        ///       "country": "",
        ///       "coordLon": "IR",
        ///       "coordLat": "46.168499",
        ///       "validations": []
        ///      }
        ///     ]
        /// </remarks>
        [Authorize]
        [HttpGet]
        [Route("ConsultCities")]
        public async Task<ActionResult<IList<CitysDTO>>> ConsultCities()
        {
            try
            {
                IList<CitysDTO> lisCitys = await _cityBusissnes.ConsultCities();
                return Ok(lisCitys);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Servicio para consultar las ciudades por usuario 
        /// </summary>
        /// <param name="userId">Id del usuario </param>
        /// <returns>Retorna lista de cuidades </returns>
        /// <remarks>
        /// Sample Request
        ///
        ///     GET
        ///     userId = 1
        ///
        /// Sample Response
        ///
        ///     [
        ///      {
        ///       "cityId": 173869,
        ///       "nameCity": null,
        ///       "state": "Al Ghizlaniyah",
        ///       "country": "",
        ///       "coordLon": "SY",
        ///       "coordLat": "36.456169",
        ///       "validations": []
        ///      },
        ///      {
        ///       "cityId": 3530,
        ///       "nameCity": null,
        ///       "state": "Qabaghlu",
        ///       "country": "",
        ///       "coordLon": "IR",
        ///       "coordLat": "46.168499",
        ///       "validations": []
        ///      }
        ///     ]
        /// </remarks>
        [HttpGet]
        [Route("ConsultUserCities")]
        public async Task<ActionResult<IList<CitysDTO>>> ConsultUserCities(int userId)
        {
            try
            {
                IList<CitysDTO> lisCitys = await _cityBusissnes.ConsultUserCities(userId);
                return Ok(lisCitys);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Servicio para eliminar una cuidad del la lista por usuario 
        /// </summary>
        /// <param name="cityByUser">Modelo con la info del la cuidad y usuario</param>
        /// <returns>Retorna estatus y mensaje</returns>
        /// <remarks>
        /// Sample Request
        ///
        ///     DELETE
        ///     {
        ///      "userId": 1,
        ///      "cityId": 173869
        ///     }
        ///
        /// Sample Response
        ///
        ///     {
        ///       "status": true,
        ///       "message": "El registro se elimino con exito"
        ///     }
        /// </remarks>
        [HttpDelete]
        [Route("DeleteCituByUser")]
        public async Task<IActionResult> DeleteCituByUser(CityByUserViewModel cityByUser)
        {
            try
            {
                CitysDTO citys = await _cityBusissnes.DeleteCituByUser(cityByUser.cityId, cityByUser.userId);
                if (citys.Validations.Count > 0)
                    return BadRequest(new { status = false, message = citys.Validations[0].ErrorMessage });

                return Ok(new { status = true, message = "El registro se elimino con exito" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
