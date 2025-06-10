using System;
using System.Collections.Generic;

namespace WebReposteria.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nit { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
