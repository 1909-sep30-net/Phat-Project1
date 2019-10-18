using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreApp.Data.Entities
{
    public partial class StoreAppContext : DbContext
    {
        public StoreAppContext()
        {
        }

        public StoreAppContext(DbContextOptions<StoreAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Store> Store { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Store");

                entity.Property(e => e.City).HasMaxLength(25);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(25);

                entity.Property(e => e.Street).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Zip).HasMaxLength(5);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager", "Store");

                entity.HasIndex(e => e.StoreId)
                    .HasName("UQ__Manager__3B82F100E1FC486F")
                    .IsUnique();

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.HasOne(d => d.Store)
                    .WithOne(p => p.Manager)
                    .HasForeignKey<Manager>(d => d.StoreId)
                    .HasConstraintName("FK__Manager__StoreId__52593CB8");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BAF4C859660");

                entity.ToTable("Orders", "Store");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__Customer__4D94879B");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Orders__StoreId__4E88ABD4");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store", "Store");

                entity.Property(e => e.City).HasMaxLength(25);

                entity.Property(e => e.State).HasMaxLength(25);

                entity.Property(e => e.Street).HasMaxLength(50);

                entity.Property(e => e.Zip).HasMaxLength(5);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
