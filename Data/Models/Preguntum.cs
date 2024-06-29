using System;
using System.Collections.Generic;

namespace ApiExamne.Data.Models;

public partial class Preguntum
{
    public int PreguntaId { get; set; }

    public int ExamenId { get; set; }

    public string Texto { get; set; } = null!;

    public virtual Examan Examen { get; set; } = null!;

    public virtual ICollection<Respuestum> Respuesta { get; set; } = new List<Respuestum>();
}
