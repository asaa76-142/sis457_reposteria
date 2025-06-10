using System;
using System.Collections.Generic;

namespace WebReposteria.Models;

public partial class Producto
{
    public int Id { get; set; }

    public int IdUnidadMedida { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Saldo { get; set; }

    public decimal PrecioVenta { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
