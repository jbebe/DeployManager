using Microsoft.EntityFrameworkCore;

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
        }

        public virtual DbSet<BatchReservation> BatchReservation { get; set; }
        public virtual DbSet<DeployPermission> DeployPermission { get; set; }
        public virtual DbSet<DeployType> DeployType { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<ServerInstance> ServerInstance { get; set; }
        public virtual DbSet<ServerType> ServerType { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchReservation>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.ReservationId });

                entity.Property(e => e.BatchId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ReservationId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.BatchReservation)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchReservation_ReservationId");
            });

            modelBuilder.Entity<DeployPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.DeployType });

                entity.Property(e => e.UserId)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeployTypeNavigation)
                    .WithMany(p => p.DeployPermission)
                    .HasForeignKey(d => d.DeployType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeployPermission_DeployType");
            });

            modelBuilder.Entity<DeployType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.End).HasColumnType("datetime");

                entity.Property(e => e.Start).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeployTypeNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.DeployType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_DeployType");

                entity.HasOne(d => d.ServerTypeNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.ServerType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_ServerType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_UserId");
            });

            modelBuilder.Entity<ServerInstance>(entity =>
            {
                entity.HasKey(e => new { e.DeployType, e.ServerType });

                entity.HasOne(d => d.DeployTypeNavigation)
                    .WithMany(p => p.ServerInstance)
                    .HasForeignKey(d => d.DeployType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServerInstance_DeployType");

                entity.HasOne(d => d.ServerTypeNavigation)
                    .WithMany(p => p.ServerInstance)
                    .HasForeignKey(d => d.ServerType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServerInstance_ServerType");
            });

            modelBuilder.Entity<ServerType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }
    }
}
