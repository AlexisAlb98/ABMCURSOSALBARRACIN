using System;
using System.Collections.Generic;
using ABMCURSOSALBARRACIN.Models;
using Microsoft.EntityFrameworkCore;

namespace ABMCURSOSALBARRACIN.Data;

public partial class AbmcursosContext : DbContext
{
    public AbmcursosContext()
    {
    }

    public AbmcursosContext(DbContextOptions<AbmcursosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__085F27D6E1281D06");

            entity.ToTable("Curso");

            entity.Property(e => e.Dia).HasMaxLength(10);
            entity.Property(e => e.NombreCurso).HasMaxLength(100);
            entity.Property(e => e.Profesor).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
