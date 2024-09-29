using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? NombresCliente { get; set; }

    public string? ApellidosCliente { get; set; }

    public string? Nit { get; set; }

    public string? DireccionCliente { get; set; }

    public string? CategoriaCliente { get; set; }

    public string? EstadoCliente { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
