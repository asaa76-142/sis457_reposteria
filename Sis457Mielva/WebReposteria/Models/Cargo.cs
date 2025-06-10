using System;
using System.Collections.Generic;

namespace WebReposteria.Models;

public partial class Cargo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
