using System.ComponentModel.DataAnnotations;

namespace ExamALSEA.ViewModels
{
    public class CityByUserViewModel
    {
        [Required(ErrorMessage = "El userId es requerido ")]
        public int userId { get; set; }
        [Required(ErrorMessage = "El cityId es requerido ")]
        public int cityId { get; set; }
    }
}


