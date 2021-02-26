using ALSEA.DATA;
using ALSEA.Entities;
using System.Threading.Tasks;

namespace ALSEA.Busissnes
{
    public class UserBusissnes
    {
        private readonly UserData _userData;

        public UserBusissnes(UserData userData)
        {
            this._userData = userData;
        }

        /// <summary>
        /// Metodo para registrar un usuario 
        /// </summary>
        /// <param name="user">objeto con la informacion del usuario </param>
        /// <returns>Retorna resultado de la accion </returns>
        public async Task<UserDTO> RegisterUser(UserDTO user)
            => await _userData.RegisterUser(user);

        /// <summary>
        /// Metodo para validar el inicio de sesión 
        /// </summary>
        /// <param name="email">correo del usuario</param>
        /// <param name="password">contraseña del usuario </param>
        /// <returns>Retorna mensaje de la acción </returns>
        public async Task<UserDTO> LogIn(string email, string password)
            => await _userData.LogIn(email,password);
    }
}
