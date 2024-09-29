using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace VentasDB.Models;

public partial class VentasDBContext : DbContext
{
    public VentasDBContext()
    {
    }

    public VentasDBContext(DbContextOptions<VentasDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BitacoraEntrega> BitacoraEntregas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleNotasCredito> DetalleNotasCreditos { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<EntregaPaquete> EntregaPaquetes { get; set; }

    public virtual DbSet<NotasCredito> NotasCreditos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<SeguimientoEntrega> SeguimientoEntregas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseMySql("server=bkaph1apuexxuaolomim-mysql.services.clever-cloud.com;database=bkaph1apuexxuaolomim;user=urvwp4rdsr1w1rli;password=Ys9ax0kJjWtsaaSeEM7Y", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<BitacoraEntrega>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("PRIMARY");

            entity.HasIndex(e => e.IdEntrega, "IdEntrega");

            entity.Property(e => e.IdBitacora).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaHoraRegistro).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.BitacoraEntregas)
                .HasForeignKey(d => d.IdEntrega)
                .HasConstraintName("BitacoraEntregas_ibfk_1");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();
            entity.Property(e => e.ApellidosCliente).HasMaxLength(100);
            entity.Property(e => e.CategoriaCliente).HasMaxLength(50);
            entity.Property(e => e.DireccionCliente).HasMaxLength(200);
            entity.Property(e => e.EstadoCliente).HasMaxLength(20);
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .HasColumnName("NIT");
            entity.Property(e => e.NombresCliente).HasMaxLength(100);
        });

        modelBuilder.Entity<DetalleNotasCredito>(entity =>
        {
            entity.HasKey(e => e.IdDetalleNotaCredito).HasName("PRIMARY");

            entity.ToTable("DetalleNotasCredito");

            entity.HasIndex(e => e.IdNotaCredito, "IdNotaCredito");

            entity.HasIndex(e => e.IdProducto, "IdProducto");

            entity.Property(e => e.IdDetalleNotaCredito).ValueGeneratedNever();
            entity.Property(e => e.PrecioUnitario).HasPrecision(10, 2);
            entity.Property(e => e.Subtotal).HasPrecision(10, 2);

            entity.HasOne(d => d.IdNotaCreditoNavigation).WithMany(p => p.DetalleNotasCreditos)
                .HasForeignKey(d => d.IdNotaCredito)
                .HasConstraintName("DetalleNotasCredito_ibfk_1");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleNotasCreditos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("DetalleNotasCredito_ibfk_2");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProducto, "IdProducto");

            entity.HasIndex(e => e.IdVenta, "IdVenta");

            entity.Property(e => e.IdDetalleVenta).ValueGeneratedNever();
            entity.Property(e => e.PrecioUnitario).HasPrecision(10, 2);
            entity.Property(e => e.Subtotal).HasPrecision(10, 2);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("DetalleVentas_ibfk_2");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("DetalleVentas_ibfk_1");
        });

        modelBuilder.Entity<EntregaPaquete>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PRIMARY");

            entity.HasIndex(e => e.IdVenta, "IdVenta");

            entity.Property(e => e.IdEntrega).ValueGeneratedNever();
            entity.Property(e => e.EstadoEntrega).HasMaxLength(50);

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.EntregaPaquetes)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("EntregaPaquetes_ibfk_1");
        });

        modelBuilder.Entity<NotasCredito>(entity =>
        {
            entity.HasKey(e => e.IdNotaCredito).HasName("PRIMARY");

            entity.ToTable("NotasCredito");

            entity.HasIndex(e => e.IdVenta, "IdVenta");

            entity.Property(e => e.IdNotaCredito).ValueGeneratedNever();
            entity.Property(e => e.TipoNotaCredito).HasMaxLength(20);
            entity.Property(e => e.TotalNotaCredito).HasPrecision(10, 2);

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.NotasCreditos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("NotasCredito_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProveedor, "IdProveedor");

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.UbicacionFisica).HasMaxLength(100);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("Productos_ibfk_1");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PRIMARY");

            entity.Property(e => e.IdProveedor).ValueGeneratedNever();
            entity.Property(e => e.ContactoProveedor).HasMaxLength(100);
            entity.Property(e => e.DireccionProveedor).HasMaxLength(200);
            entity.Property(e => e.NombreProveedor).HasMaxLength(100);
        });

        modelBuilder.Entity<SeguimientoEntrega>(entity =>
        {
            entity.HasKey(e => e.IdSeguimiento).HasName("PRIMARY");

            entity.HasIndex(e => e.IdEntrega, "IdEntrega");

            entity.Property(e => e.IdSeguimiento).ValueGeneratedNever();
            entity.Property(e => e.EstadoSeguimiento).HasMaxLength(50);
            entity.Property(e => e.FechaHoraSeguimiento).HasColumnType("datetime");
            entity.Property(e => e.Latitud).HasPrecision(10, 8);
            entity.Property(e => e.Longitud).HasPrecision(11, 8);

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.SeguimientoEntregas)
                .HasForeignKey(d => d.IdEntrega)
                .HasConstraintName("SeguimientoEntregas_ibfk_1");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PRIMARY");

            entity.HasIndex(e => e.IdCliente, "IdCliente");

            entity.Property(e => e.IdVenta).ValueGeneratedNever();
            entity.Property(e => e.TipoVenta).HasMaxLength(10);
            entity.Property(e => e.TotalVenta).HasPrecision(10, 2);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("Ventas_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
