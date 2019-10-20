using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebScraper_WebApp.Models
{
    public partial class WebScraperContext : DbContext
    {
        public WebScraperContext()
        {
        }

        public WebScraperContext(DbContextOptions<WebScraperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CleanedData> CleanedData { get; set; }
        public virtual DbSet<RawData> RawData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Warning from Microsoft removed
            }
        }
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CleanedData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CombinedValue).HasColumnType("decimal(8, 3)");

                entity.Property(e => e.NetMarginValue).HasColumnType("decimal(8, 3)");

                entity.Property(e => e.PercentageValue).HasColumnType("decimal(8, 3)");

                entity.Property(e => e.RawValue).HasMaxLength(50);

                entity.Property(e => e.SpacePlusRanking).HasMaxLength(50);

                entity.Property(e => e.ValueOnly).HasMaxLength(4000);

                entity.Property(e => e.ValueType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValueValue).HasColumnType("decimal(8, 3)");
            });

            modelBuilder.Entity<RawData>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.RawValue).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
