using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class NotasCredito
{
    public int IdNotaCredito { get; set; }

    public int? IdVenta { get; set; }

    public DateTime? FechaNotaCredito { get; set; }

    public string? TipoNotaCredito { get; set; }

    public decimal? TotalNotaCredito { get; set; }

    public virtual ICollection<DetalleNotasCredito> DetalleNotasCreditos { get; set; } = new List<DetalleNotasCredito>();

    public virtual Venta? IdVentaNavigation { get; set; }
}
