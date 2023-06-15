using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TopoLocoApi.Models;

public partial class Sistem21TopolocoContext : DbContext
{
    public Sistem21TopolocoContext()
    {
    }

    public Sistem21TopolocoContext(DbContextOptions<Sistem21TopolocoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.Property(e => e.Id).HasColumnType("int(10)");
            entity.Property(e => e.NombreUsuario).HasMaxLength(20);
            entity.Property(e => e.Puntaje)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
