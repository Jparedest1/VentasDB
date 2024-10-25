using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public int? IdProveedor { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public string? UbicacionFisica { get; set; }

    public int? ExistenciaMinima { get; set; }

    public virtual ICollection<DetalleNotasCredito> DetalleNotasCreditos { get; set; } = new List<DetalleNotasCredito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Proveedore? IdProveedorNavigation { get; set; }

    public ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
