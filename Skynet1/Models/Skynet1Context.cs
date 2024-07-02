using System;
using Microsoft.EntityFrameworkCore;
using Skynet1.Models;

namespace Skynet1.Models
{
    public partial class Skynet1Context : DbContext
    {
        public Skynet1Context()
        {
        }

        public Skynet1Context(DbContextOptions<Skynet1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-28KMSTV\\SQLEXPRESS;Database=Skynet1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.NameEmployee)
                    .IsRequired()
                    .HasColumnName("name_employee");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.RolId).HasColumnName("rol_id"); // Заменено на "rol_id"

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Employee_Roles"); // Изменено на "FK_Employee_Roles"
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Services");

                entity.HasKey(e => e.ServiceId);

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.NameService)
                    .HasMaxLength(50)
                    .HasColumnName("name_service");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets");

                entity.HasKey(e => e.TicketId);

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.NameTicket).HasColumnName("name_ticket");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
