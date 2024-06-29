using System.ComponentModel.DataAnnotations;

namespace ApiExamne.Dto
{
    public class ExamenRequestDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder de 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
    }
}
