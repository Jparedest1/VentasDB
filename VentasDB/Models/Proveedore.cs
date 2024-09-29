using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string? NombreProveedor { get; set; }

    public string? DireccionProveedor { get; set; }

    public string? ContactoProveedor { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
