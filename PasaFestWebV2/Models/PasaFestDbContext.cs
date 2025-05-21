using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PasaFestWebV2.Models;

public partial class PasaFestDbContext : DbContext
{
    public PasaFestDbContext()
    {
    }

    public PasaFestDbContext(DbContextOptions<PasaFestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Concierto> Conciertos { get; set; }

    public virtual DbSet<Entradum> Entrada { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql1003.site4now.net;Database=db_ab9511_pasafestdb;User Id=db_ab9511_pasafestdb_admin;Password=Davito3210.;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__C4BAA6048E281A83");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_pago");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Compra__id_usuar__4222D4EF");
        });

        modelBuilder.Entity<Concierto>(entity =>
        {
            entity.HasKey(e => e.IdConcierto).HasName("PK__Conciert__90E441A26BE1513D");

            entity.ToTable("Concierto");

            entity.Property(e => e.IdConcierto).HasColumnName("id_concierto");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Lugar)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("lugar");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Entradum>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("PK__Entrada__167CD61BE04EF7C1");

            entity.Property(e => e.IdEntrada).HasColumnName("id_entrada");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.TipoEntrada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_entrada");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__Entrada__id_comp__45F365D3");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("FK__Entrada__id_zona__46E78A0C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD06353434");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__2A586E0B428D42A5").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ApellidoMat)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido_mat");
            entity.Property(e => e.ApellidoPat)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido_pat");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PK__Zona__67C936117211920A");

            entity.ToTable("Zona");

            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.Capacidad).HasColumnName("capacidad");
            entity.Property(e => e.EspaciosOcupados)
                .HasDefaultValue(0)
                .HasColumnName("espacios_ocupados");
            entity.Property(e => e.IdConcierto).HasColumnName("id_concierto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdConciertoNavigation).WithMany(p => p.Zonas)
                .HasForeignKey(d => d.IdConcierto)
                .HasConstraintName("FK__Zona__id_concier__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
