using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ServiceDesk
{
    public partial class ServiceDeskContext : DbContext
    {
        public ServiceDeskContext()
            : base("name=ServiceDeskContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incident>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Incident>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Version)
                .HasPrecision(18, 1);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Technician>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Technician>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Technician>()
                .Property(e => e.Phone)
                .IsUnicode(false);
        }
    }
}
