using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyAttemptNum5.Models;

namespace MyAttemptNum5;

public partial class AvtosalonContext : DbContext
{
    public AvtosalonContext()
    {
    }

    public AvtosalonContext(DbContextOptions<AvtosalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Automobile> Automobiles { get; set; }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Dogovor> Dogovors { get; set; }

    public virtual DbSet<Ekzemplyar> Ekzemplyars { get; set; }

    public virtual DbSet<Komplektaciya> Komplektaciyas { get; set; }
    public virtual DbSet<KomplektaciyaEkzemplyar> KomplektaciyaEkzemplyars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Avtosalon;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KomplektaciyaEkzemplyar>(entity =>
        {
            entity.HasKey(e => new { e.IdE, e.IdK }).HasName("pr_ek");

            entity.ToTable("komplektaciya_ekzemplyar");
            entity.Property(e => e.IdE).HasColumnName("id_e");
            entity.Property(e => e.IdK).HasColumnName("id_k");

            entity.HasOne(d => d.Ekzemplyar).WithMany(p => p.KomplektaciyaEkzemplyars)
                .HasForeignKey(d => d.IdE)
                .HasConstraintName("fk_e");

            entity.HasOne(d => d.Komplektaciya).WithMany(p => p.KomplektaciyaEkzemplyars)
                .HasForeignKey(d => d.IdK)
                .HasConstraintName("fk_k");
        });
        
        modelBuilder.Entity<Automobile>(entity =>
        {
            entity.HasKey(e => e.IdA).HasName("automobile_pkey");

            entity.ToTable("automobile");

            entity.Property(e => e.IdA)
                .HasPrecision(5)
                .HasColumnName("id_a");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .HasColumnName("color");
            entity.Property(e => e.KolVo)
                .HasPrecision(2)
                .HasColumnName("kol_vo");
            entity.Property(e => e.Model)
                .HasMaxLength(20)
                .HasColumnName("model");
            entity.Property(e => e.Price)
                .HasPrecision(9)
                .HasColumnName("price");
            entity.Property(e => e.ReleaseDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("release_date");
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.IdB).HasName("buyer_pkey");

            entity.ToTable("buyer");

            entity.Property(e => e.IdB)
                .HasPrecision(5)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_b");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Dogovor>(entity =>
        {
            entity.HasKey(e => e.IdD).HasName("dogovor_pkey");

            entity.ToTable("dogovor");

            entity.Property(e => e.IdD)
                .HasPrecision(5)
                .HasColumnName("id_d");
            entity.Property(e => e.DateOfExecution)
                .HasDefaultValueSql("now()")
                .HasColumnName("date_of_execution");
            entity.Property(e => e.IdB)
                .HasPrecision(5)
                .HasColumnName("id_b");
            entity.Property(e => e.VinKod)
                .HasMaxLength(17)
                .HasColumnName("vin_kod");

            entity.HasOne(d => d.IdBNavigation).WithMany(p => p.Dogovors)
                .HasForeignKey(d => d.IdB)
                .HasConstraintName("fk_b");
            entity.HasOne(d => d.Ekzemplyar).WithOne(e => e.IdDNavigation)
                .HasForeignKey<Ekzemplyar>(d => d.IdD)
                .HasConstraintName("fk_e");
        });

        modelBuilder.Entity<Ekzemplyar>(entity =>
        {
            entity.HasKey(e => e.VinKod).HasName("ekzemplyar_pkey");

            entity.ToTable("ekzemplyar");

            entity.Property(e => e.VinKod)
                .HasMaxLength(17)
                .HasColumnName("vin_kod");
            entity.Property(e => e.IdA)
                .HasPrecision(5)
                .HasColumnName("id_a");
            entity.Property(e => e.IdD)
                .HasPrecision(5)
                .HasColumnName("id_d");

            entity.HasOne(d => d.IdANavigation).WithMany(p => p.Ekzemplyars)
                .HasForeignKey(d => d.IdA)
                .HasConstraintName("fk_avt");

            entity.HasOne(d => d.IdDNavigation).WithOne(p => p.Ekzemplyar)
                .HasForeignKey<Dogovor>(e=>e.VinKod)
                .HasConstraintName("fk_d");
        });

        modelBuilder.Entity<Komplektaciya>(entity =>
        {
            entity.HasKey(e => e.IdK).HasName("komplektaciya_pkey");

            entity.ToTable("komplektaciya");

            entity.Property(e => e.IdK)
                .HasPrecision(5)
                .HasColumnName("id_k");
            entity.Property(e => e.NameK)
                .HasMaxLength(15)
                .HasColumnName("name_k");
            entity.Property(e => e.Opisanie)
                .HasMaxLength(100)
                .HasColumnName("opisanie");
            entity.Property(e => e.Price)
                .HasPrecision(6)
                .HasColumnName("price");

            // entity.HasMany(d => d.IdEs).WithMany(p => p.IdKs)
            //     .UsingEntity<Dictionary<string, object>>(
            //         "KomplektaciyaAutomobile",
            //         r => r.HasOne<Ekzemplyar>().WithMany()
            //             .HasForeignKey("IdE")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("fk_e"),
            //         l => l.HasOne<Komplektaciya>().WithMany()
            //             .HasForeignKey("IdK")
            //             .OnDelete(DeleteBehavior.ClientSetNull)
            //             .HasConstraintName("fk_k"),
            //         j =>
            //         {
            //             j.HasKey("IdK", "IdE").HasName("pr_ek");
            //             j.ToTable("komplektaciya_ekzemplyar");
            //             j.IndexerProperty<long>("IdK")
            //                 .HasPrecision(5)
            //                 .HasColumnName("id_k");
            //             j.IndexerProperty<long>("IdE")
            //                 .HasPrecision(5)
            //                 .HasColumnName("id_e");
            //         });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
