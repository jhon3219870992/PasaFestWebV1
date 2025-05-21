using System;
using System.Collections.Generic;

namespace PasaFestWebV2.Models;

public partial class Concierto
{
    public int IdConcierto { get; set; }

    public string? Nombre { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Lugar { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}
