using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? TipoVenta { get; set; }

    public decimal? TotalVenta { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<EntregaPaquete> EntregaPaquetes { get; set; } = new List<EntregaPaquete>();

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual ICollection<NotasCredito> NotasCreditos { get; set; } = new List<NotasCredito>();
}
