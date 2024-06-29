using System;
using System.Collections.Generic;

namespace ApiExamne.Data.Models;

public partial class Examan
{
    public int ExamenId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
