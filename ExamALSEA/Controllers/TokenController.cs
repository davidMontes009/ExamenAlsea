using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;

namespace ExamALSEA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController:ControllerBase
    {
        public TokenController()
        {
        }

        [HttpGet]
        [Route("ConsultCities")]
        public  async Task<IActionResult> ConsultCities()
        {
            return Ok();
        }
    }
}
