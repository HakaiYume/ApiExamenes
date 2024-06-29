using System.ComponentModel.DataAnnotations;

namespace ApiExamne.Dto
{
    public class PreguntaRequestDto
    {
        [Required(ErrorMessage = "Es obligatorio indicar a que examen pertece una pregunta.")]
        public int ExamenId { get; set; }

        [Required(ErrorMessage = "El texto de la pregunta es obligatorio.")]
        [StringLength(500, ErrorMessage = "El texto no puede exceder de 500 caracteres.")]
        public string Texto { get; set; } = string.Empty;
    }
}