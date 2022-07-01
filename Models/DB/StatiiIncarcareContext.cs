using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StatiiIncarcare.Models.DB
{
    public partial class StatiiIncarcareContext : DbContext
    {
        public StatiiIncarcareContext()
        {
        }

        public StatiiIncarcareContext(DbContextOptions<StatiiIncarcareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Priza> Prizas { get; set; } = null!;
        public virtual DbSet<Rezervare> Rezervares { get; set; } = null!;
        public virtual DbSet<Statie> Staties { get; set; } = null!;
        public virtual DbSet<TipPriza> TipPrizas { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2VOADKB\\SQLEXPRESS;Initial Catalog=StatiiIncarcare;Integrated Security=True;Connect Timeout=30;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Priza>(entity =>
            {
                entity.HasKey(e => e.IdPriza);

                entity.ToTable("Priza");

                entity.HasOne(d => d.IdStatieNavigation)
                    .WithMany(p => p.Prizas)
                    .HasForeignKey(d => d.IdStatie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Priza_Statie");

                entity.HasOne(d => d.IdTipNavigation)
                    .WithMany(p => p.Prizas)
                    .HasForeignKey(d => d.IdTip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Priza_TipPriza");
            });

            modelBuilder.Entity<Rezervare>(entity =>
            {
                entity.HasKey(e => e.IdRezervare);

                entity.ToTable("Rezervare");

                entity.Property(e => e.IdRezervare).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NrMasina).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdPrizaNavigation)
                    .WithMany(p => p.Rezervares)
                    .HasForeignKey(d => d.IdPriza)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rezervare_Priza");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Rezervares)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Rezervare_User");
            });

            modelBuilder.Entity<Statie>(entity =>
            {
                entity.HasKey(e => e.IdStatie);

                entity.ToTable("Statie");

                entity.Property(e => e.Adresa).HasMaxLength(50);

                entity.Property(e => e.Nume).HasMaxLength(50);

                entity.Property(e => e.Oras).HasMaxLength(50);
            });

            modelBuilder.Entity<TipPriza>(entity =>
            {
                entity.HasKey(e => e.IdTip);

                entity.ToTable("TipPriza");

                entity.Property(e => e.NumePriza).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
