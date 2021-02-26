using System.Threading.Tasks;
using ALSEA.Busissnes;
using ALSEA.Entities;
using AutoMapper;
using ExamALSEA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExamALSEA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserBusissnes _UserBusissnes { get; }

        public UserController(ILogger<UserController> logger, UserBusissnes userBusissnes, IMapper mapper)
        {
            this._logger = logger;
            _UserBusissnes = userBusissnes;
            this._mapper = mapper;
        }

        /// <summary>
        /// API para registrar un usuario 
        /// </summary>
        /// <param name="userView"> modelo con la informacion del usuario </param>
        /// <returns> retorna estatus y mensaje </returns>
        /// <remarks>
        /// Sample Request
        ///
        ///     POST
        ///     {
        ///       "name": "David Montes",
        ///       "email": "david@mail.com",
        ///       "password": "123qwe"
        ///     }
        ///
        /// Sample Response
        ///
        ///     {
        ///       "status": true,
        ///       "message": "El usuario se registro con exito"
        ///     }
        /// </remarks>
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(UserViewModel userView)
        {
            UserDTO user = await _UserBusissnes.RegisterUser(_mapper.Map<UserViewModel,UserDTO>(userView));
            if (user.Validations.Count > 0)
                return Ok(new { status = false, message = user.Validations[0].ErrorMessage });

            return Ok(new { status = true, message = "El usuario se registro con exito" });

        }

        /// <summary>
        /// Api para logear un usuario 
        /// </summary>
        /// <param name="email">correo del usuario</param>
        /// <param name="password">contraseña del usuario</param>
        /// <returns> retorna estatus y mensaje </returns>
        /// Sample Request
        ///
        ///     GET
        ///     email = david@mail.com
        ///     password = 123qwe
        ///
        /// Sample Response
        ///
        ///     {
        ///       "status": true,
        ///       "message": "Usuario con acceso"
        ///     }
        /// </remarks>
        [HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            UserDTO user = await _UserBusissnes.LogIn(email,password);
            if (user.Validations.Count > 0)
                return Ok(new { status = false, message = user.Validations[0].ErrorMessage });

            return Ok(new { status = true, message = "Usuario con acceso" });

        }
    }
}
