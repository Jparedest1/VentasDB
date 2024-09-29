using System;
using System.Collections.Generic;

namespace VentasDB.Models;

public partial class BitacoraEntrega
{
    public int IdBitacora { get; set; }

    public int? IdEntrega { get; set; }

    public DateTime? FechaHoraRegistro { get; set; }

    public string? Descripcion { get; set; }

    public string? Usuario { get; set; }

    public virtual EntregaPaquete? IdEntregaNavigation { get; set; }
}
