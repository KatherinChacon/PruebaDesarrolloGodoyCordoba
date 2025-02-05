using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Models;

public partial class BdGodoyCordobaContext : DbContext
{
    public BdGodoyCordobaContext()
    {
    }

    public BdGodoyCordobaContext(DbContextOptions<BdGodoyCordobaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.IdSesion).HasName("PK__Sesion__22EC535B89F96565");

            entity.ToTable("Sesion");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97CB2EAC32");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaAcceso)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Acceso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
