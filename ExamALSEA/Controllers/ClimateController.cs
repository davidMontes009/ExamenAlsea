using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamALSEA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController:ControllerBase
    {
        private readonly ILogger<ClimateController> _logger;

        public ClimateController(ILogger<ClimateController> logger)
        {
            this._logger = logger;
        }

        [HttpGet] 
        [Route("CurrentWeatherData")]
        public async Task<IActionResult> CurrentWeatherData(int cityId)
        {
            try
            {
                var url = "http://api.openweathermap.org/data/2.5/weather?id="+ cityId +"&appid=53a70d043e2dd949b6c1b3817fa99ef8";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //var table = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
                        //model.ReturnedJson = data;
                        return Ok(data);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
