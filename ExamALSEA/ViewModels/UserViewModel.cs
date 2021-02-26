using System.ComponentModel.DataAnnotations;

namespace ExamALSEA.ViewModels
{
    /// <summary>
    /// Modelo para recuoerar y validar la información del usuario 
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Nombre
        /// </summary>
        [Required( ErrorMessage = "El nombre es requerido ")]
        [MaxLength(length: 50)]
        public string Name { get; set; }
        /// <summary>
        /// Correo electronico
        /// </summary>
        [MaxLength(length: 30)]
        public string Email { get; set; }
        /// <summary>
        /// Contraseña d
        /// </summary>
        [MinLength(length: 6)]
        [MaxLength(length: 16)]
        public string Password { get; set; }
    }
}
