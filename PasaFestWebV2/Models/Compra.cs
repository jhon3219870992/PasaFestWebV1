using System;
using System.Collections.Generic;

namespace PasaFestWebV2.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoPago { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
