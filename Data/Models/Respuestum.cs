using System;
using System.Collections.Generic;

namespace ApiExamne.Data.Models;

public partial class Respuestum
{
    public int RespuestaId { get; set; }

    public int? PreguntaId { get; set; }

    public string Texto { get; set; } = null!;

    public bool EsCorrecta { get; set; }

    public virtual Preguntum? Pregunta { get; set; }
}
