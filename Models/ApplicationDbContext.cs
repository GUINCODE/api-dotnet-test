using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace testApi.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cour> Cours { get; set; }
        public virtual DbSet<Etduiant> Etduiants { get; set; }
        public virtual DbSet<EtudiantsCour> EtudiantsCours { get; set; }
        public virtual DbSet<ReleveDeNote> ReleveDeNotes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:ChaineDeConnexion");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cour>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Etduiant>(entity =>
            {
                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EtudiantsCour>(entity =>
            {
                entity.HasOne(d => d.IdCourNavigation)
                    .WithMany(p => p.EtudiantsCours)
                    .HasForeignKey(d => d.IdCour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EtudiantsCours_Cours");

                entity.HasOne(d => d.IdEtudiantNavigation)
                    .WithMany(p => p.EtudiantsCours)
                    .HasForeignKey(d => d.IdEtudiant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EtudiantsCours_Etudiants");
            });

            modelBuilder.Entity<ReleveDeNote>(entity =>
            {
                entity.HasOne(d => d.IdCourNavigation)
                    .WithMany(p => p.ReleveDeNotes)
                    .HasForeignKey(d => d.IdCour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleveDeNotes_Cours");

                entity.HasOne(d => d.IdEtudiantNavigation)
                    .WithMany(p => p.ReleveDeNotes)
                    .HasForeignKey(d => d.IdEtudiant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReleveDeNotes_Etduiants");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
