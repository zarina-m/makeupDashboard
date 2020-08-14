using System;
using System.Collections;
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
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Colour)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasColumnName("ImageURL")
                    .HasMaxLength(2083)
                    .IsUnicode(false);

                entity.Property(e => e.NewPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Retailer)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SaleDate).HasColumnType("datetime");

                entity.Property(e => e.Size)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
