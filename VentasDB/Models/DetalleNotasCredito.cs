using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class DetalleNotasCredito
{
    public int IdDetalleNotaCredito { get; set; }

    public int? IdNotaCredito { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual NotasCredito? IdNotaCreditoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
