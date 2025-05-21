using System;
using System.Collections.Generic;

namespace PasaFestWebV2.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPat { get; set; }

    public string? ApellidoMat { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
