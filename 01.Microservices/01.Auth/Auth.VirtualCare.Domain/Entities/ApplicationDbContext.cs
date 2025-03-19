using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auth.VirtualCare.API;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserState> UserStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=VirtualCare;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LogUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AutoNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.AvailableAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(300);
            entity.Property(e => e.TokenExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.UserIdSo).HasColumnName("UserIdSO");
            entity.Property(e => e.UserName).HasMaxLength(200);

            entity.HasOne(d => d.UserState).WithMany(p => p.Users)
              .HasForeignKey(d => d.UserStateId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Users_UserStates");
        });

        modelBuilder.Entity<UserState>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
