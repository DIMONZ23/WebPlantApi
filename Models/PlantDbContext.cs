using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebPlantApi.Models;

public partial class PlantDbContext : DbContext
{
    public PlantDbContext()
    {
    }

    public PlantDbContext(DbContextOptions<PlantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Plantitem> Plantitems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usercontact> Usercontacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PlantDB;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plants_pkey");

            entity.ToTable("plants");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Detaileddescription)
                .HasMaxLength(10000)
                .HasColumnName("detaileddescription");
            entity.Property(e => e.Imageurl).HasColumnName("imageurl");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(12, 2)
                .HasColumnName("price");
            entity.Property(e => e.Shortdescription)
                .HasMaxLength(100)
                .HasColumnName("shortdescription");
        });

        modelBuilder.Entity<Plantitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plantitems_pkey");

            entity.ToTable("plantitems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descriptionplant).HasColumnName("descriptionplant");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(255)
                .HasColumnName("imageurl");
            entity.Property(e => e.Istrendy).HasColumnName("istrendy");
            entity.Property(e => e.Nameplant)
                .HasMaxLength(100)
                .HasColumnName("nameplant");
            entity.Property(e => e.Priceplant).HasColumnName("priceplant");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(255)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Usercontact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usercontacts_pkey");

            entity.ToTable("usercontacts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contacttype)
                .HasMaxLength(10)
                .HasColumnName("contacttype");
            entity.Property(e => e.Contactvalue)
                .HasMaxLength(255)
                .HasColumnName("contactvalue");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Isprimary)
                .HasDefaultValueSql("false")
                .HasColumnName("isprimary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
