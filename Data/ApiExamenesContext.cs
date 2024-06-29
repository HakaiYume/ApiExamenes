using System;
using System.Collections.Generic;
using ApiExamne.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamne.Data;

public partial class ApiExamenesContext : DbContext
{
    public ApiExamenesContext()
    {
    }

    public ApiExamenesContext(DbContextOptions<ApiExamenesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Examan> Examen { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Examan>(entity =>
        {
            entity.HasKey(e => e.ExamenId).HasName("PK__Examen__AC23A2F1F393046E");

            entity.Property(e => e.ExamenId).HasColumnName("ExamenID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.PreguntaId).HasName("PK__Pregunta__EBB2A3591C5B4344");

            entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");
            entity.Property(e => e.ExamenId).HasColumnName("ExamenID");
            entity.Property(e => e.Texto).HasMaxLength(255);

            entity.HasOne(d => d.Examen).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.ExamenId)
                .HasConstraintName("FK__Pregunta__Examen__398D8EEE");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.RespuestaId).HasName("PK__Respuest__31F7FC313EE0B33D");

            entity.Property(e => e.RespuestaId).HasColumnName("RespuestaID");
            entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");
            entity.Property(e => e.Texto).HasMaxLength(255);

            entity.HasOne(d => d.Pregunta).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.PreguntaId)
                .HasConstraintName("FK__Respuesta__Pregu__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
