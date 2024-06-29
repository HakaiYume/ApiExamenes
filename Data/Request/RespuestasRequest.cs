using System.ComponentModel.DataAnnotations;

namespace ApiExamne.Dto
{
    public class RespuestaRequestDto
    {
        [Required(ErrorMessage = "El texto de la respuesta es obligatorio.")]
        [StringLength(500, ErrorMessage = "El texto no puede exceder de 500 caracteres.")]
        public string Texto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio indicar si la respuesta es correcta o no.")]
        public bool EsCorrecta { get; set; }

        [Required(ErrorMessage = "Es obligatorio indicar a que pregunta pertenece una respuesta.")]
        public int? PreguntaId { get; set; }
    }
}
