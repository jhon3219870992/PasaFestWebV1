using System;
using System.Collections.Generic;

namespace PasaFestWebV2.Models;

public partial class Zona
{
    public int IdZona { get; set; }

    public string? Nombre { get; set; }

    public int? Capacidad { get; set; }

    public int? EspaciosOcupados { get; set; }

    public decimal? Precio { get; set; }

    public int? IdConcierto { get; set; }

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Concierto? IdConciertoNavigation { get; set; }
}
