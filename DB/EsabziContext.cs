using System;
using System.Collections.Generic;
using esabzi.Models;
using Microsoft.EntityFrameworkCore;

namespace esabzi.DB;

public partial class EsabziContext : DbContext
{
    public EsabziContext()
    {
    }

    public EsabziContext(DbContextOptions<EsabziContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ESABZI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83F2C06BCC8");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E616492C34C61").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__user__F3DBC57248CC440D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(255)
                .HasColumnName("contact_no");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Picture)
                .HasMaxLength(255)
                .HasDefaultValueSql("('https://i.ibb.co/cT5mM2Z/profile-img.png')")
                .HasColumnName("picture");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasDefaultValueSql("('CUSTOMER')")
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
