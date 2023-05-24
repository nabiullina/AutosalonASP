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

    public virtual DbSet<Manager> Managers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Avtosalon;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .HasColumnName("id_b");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phone_number");
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
            entity.Property(e => e.IdM)
                .HasPrecision(5)
                .HasColumnName("id_m");

            entity.HasOne(d => d.IdBNavigation).WithMany(p => p.Dogovors)
                .HasForeignKey(d => d.IdB)
                .HasConstraintName("fk_b");

            entity.HasOne(d => d.IdMNavigation).WithMany(p => p.Dogovors)
                .HasForeignKey(d => d.IdM)
                .HasConstraintName("fk_m");
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

            entity.HasOne(d => d.IdDNavigation).WithMany(p => p.Ekzemplyars)
                .HasForeignKey(d => d.IdD)
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

            entity.HasMany(d => d.IdAs).WithMany(p => p.IdKs)
                .UsingEntity<Dictionary<string, object>>(
                    "KomplektaciyaAutomobile",
                    r => r.HasOne<Automobile>().WithMany()
                        .HasForeignKey("IdA")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_a"),
                    l => l.HasOne<Komplektaciya>().WithMany()
                        .HasForeignKey("IdK")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_k"),
                    j =>
                    {
                        j.HasKey("IdK", "IdA").HasName("pr_ak");
                        j.ToTable("komplektaciya_automobile");
                        j.IndexerProperty<decimal>("IdK")
                            .HasPrecision(5)
                            .HasColumnName("id_k");
                        j.IndexerProperty<decimal>("IdA")
                            .HasPrecision(5)
                            .HasColumnName("id_a");
                    });
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.IdM).HasName("manager_pkey");

            entity.ToTable("manager");

            entity.Property(e => e.IdM)
                .HasPrecision(5)
                .HasColumnName("id_m");
            entity.Property(e => e.Age)
                .HasPrecision(2)
                .HasColumnName("age");
            entity.Property(e => e.Fio)
                .HasMaxLength(50)
                .HasColumnName("fio");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
