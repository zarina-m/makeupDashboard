using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace makeupDashboard.Models
{
    public partial class makeupDBContext : DbContext
    {
        static string connectionString {get;set;} = Environment.GetEnvironmentVariable("MAKEUP", EnvironmentVariableTarget.Process);

        static makeupDBContext() 
        {
            connectionString = connectionString ?? Environment.GetEnvironmentVariable("MAKEUP", EnvironmentVariableTarget.Machine);
        }

        public makeupDBContext()
        {
        }

        public makeupDBContext(DbContextOptions<makeupDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.Retailer, e.ProductId, e.SkuId })
                    .HasName("PK__Products__8EE018CD5507931F");

                entity.Property(e => e.Retailer)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SkuId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(2083)
                    .IsUnicode(false);

                entity.Property(e => e.ListPrice).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.SaleDate).HasColumnType("datetime");

                entity.Property(e => e.SalePrice).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Size)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2083)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
