using System.Collections.Generic;

namespace ApiExamne.Dto
{
    public class PreguntaResponseDto
    {
        public int PreguntaId { get; set; }
        public string Texto { get; set; } = string.Empty;

        public object Respuestas { get; set; } = new object();
    }
}
