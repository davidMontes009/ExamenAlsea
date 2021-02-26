using System.ComponentModel.DataAnnotations;

namespace ExamALSEA.ViewModels
{
    /// <summary>
    /// Modelo para mapear y validar la info del logIn
    /// </summary>
    public class LogInViewModel
    {
        /// <summary>
        /// Correo electronico 
        /// </summary>
        [Required(ErrorMessage = "El email es requerido ")]
        [RegularExpression(@"^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage = "Formato de correo invalido")]
        public string email { get; set; }
        /// <summary>
        /// Contraseña 
        /// </summary>
        [Required(ErrorMessage = "El password es requerido ")]
        public string password { get; set; }
    }
}
