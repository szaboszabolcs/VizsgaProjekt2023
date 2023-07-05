using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ZaroFeladat.Models
{
    public partial class zaroprojektContext : DbContext
    {
        public zaroprojektContext()
        {
        }

        public zaroprojektContext(DbContextOptions<zaroprojektContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Felhasznalok> Felhasznaloks { get; set; }
        public virtual DbSet<Filmek> Filmeks { get; set; }
        public virtual DbSet<Registry> Registries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=zaroprojekt;user=root;password=;ssl mode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Felhasznalok>(entity =>
            {
                entity.ToTable("felhasznalok");

                entity.HasIndex(e => new { e.FelhasznaloNev, e.Email }, "FelhasznaloNev");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Aktiv).HasColumnType("int(1)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Jogosultsag).HasColumnType("int(1)");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Filmek>(entity =>
            {
                entity.ToTable("filmek");

                entity.Property(e => e.Id).HasColumnType("int(100)");

                entity.Property(e => e.Cim)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ertekeles).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Filmlogo)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("filmlogo");

                entity.Property(e => e.Kategoria)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Leiras)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Registry>(entity =>
            {
                entity.ToTable("registry");

                entity.HasIndex(e => e.Key, "Key");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
