using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class EntregaPaquete
{
    public int IdEntrega { get; set; }

    public int? IdVenta { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? EstadoEntrega { get; set; }

    public virtual ICollection<BitacoraEntrega> BitacoraEntregas { get; set; } = new List<BitacoraEntrega>();

    public virtual Venta? IdVentaNavigation { get; set; }

    public virtual ICollection<SeguimientoEntrega> SeguimientoEntregas { get; set; } = new List<SeguimientoEntrega>();
}
