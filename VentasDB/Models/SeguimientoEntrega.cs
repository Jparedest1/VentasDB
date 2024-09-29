using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class SeguimientoEntrega
{
    public int IdSeguimiento { get; set; }

    public int? IdEntrega { get; set; }

    public DateTime? FechaHoraSeguimiento { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string? EstadoSeguimiento { get; set; }

    public virtual EntregaPaquete? IdEntregaNavigation { get; set; }
}
