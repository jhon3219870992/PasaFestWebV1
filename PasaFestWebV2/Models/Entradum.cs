using System;
using System.Collections.Generic;

namespace PasaFestWebV2.Models;

public partial class Entradum
{
    public int IdEntrada { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCompra { get; set; }

    public string? TipoEntrada { get; set; }

    public int? IdCompra { get; set; }

    public int? IdZona { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
