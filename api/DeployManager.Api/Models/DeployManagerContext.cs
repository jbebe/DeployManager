using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace DeployManager.Api.Models
{
    public partial class DeployManagerContext : DbContext
    {
        private string ConnectionString { get; set; }

        public DeployManagerContext()
        {
        }

        public DeployManagerContext(DbContextOptions<DeployManagerContext> options)
            : base(options)
        {
            ConnectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;
        }

        public virtual DbSet<Reservation> Reservation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Previous)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Start).HasColumnType("datetime");
            });
        }
    }
}
