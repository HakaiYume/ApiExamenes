namespace ApiExamne.Dto
{
    public class RespuestaResponseDto
    {
        public int RespuestaId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public bool EsCorrecta { get; set; }
    }
}
