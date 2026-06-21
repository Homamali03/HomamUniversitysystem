using System;
using System.Collections.Generic;
using Hope.DomainEntities.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace Hope.DomainEntities;

public partial class HopeUrhContext : DbContext
{
    public HopeUrhContext()
    {
    }

    public HopeUrhContext(DbContextOptions<HopeUrhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignUsersToRole> AssignUsersToRoles { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleRole> ModuleRoles { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyType> StudyTypes { get; set; }

    public virtual DbSet<TawjihiCertificate> TawjihiCertificates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VJS1SOU;Database=HopeURH;User Id=HopeMay;Password=12345;Trusted_Connection=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignUsersToRole>(entity =>
        {
            entity.ToTable("AssignUsersToRole", "admin");

            entity.HasOne(d => d.Role).WithMany(p => p.AssignUsersToRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignUsersToRole_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.AssignUsersToRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignUsersToRole_Users");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department", "admin");

            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog", "services");

            entity.Property(e => e.ErrorException)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.ErrorMessage)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Trasnaction).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ErrorLog_Users");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Majors", "admin");

            entity.Property(e => e.MajorName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.MinimumAvg).HasColumnName("MinimumAVG");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.ToTable("Module", "admin");

            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModuleUrl)
                .HasMaxLength(50)
                .HasColumnName("ModuleURL");
        });

        modelBuilder.Entity<ModuleRole>(entity =>
        {
            entity.ToTable("ModuleRoles", "admin");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleRoles)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleRoles_Module");

            entity.HasOne(d => d.Role).WithMany(p => p.ModuleRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleRoles_Roles");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.ToTable("Nationality", "admin");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "admin");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Section", "admin");

            entity.Property(e => e.SectionName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Sections)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Section_Department");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Students", "services");

            entity.Property(e => e.GraduationYear)
                .IsRequired()
                .HasMaxLength(4);
            entity.Property(e => e.TawjihiAvg).HasColumnName("TawjihiAVG");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Majors");

            entity.HasOne(d => d.StudyType).WithMany(p => p.Students)
                .HasForeignKey(d => d.StudyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_StudyType");

            entity.HasOne(d => d.TawjihiCertificateldNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.TawjihiCertificateld)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_TawjihiCertificate");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Users");
        });

        modelBuilder.Entity<StudyType>(entity =>
        {
            entity.ToTable("StudyType", "admin");

            entity.Property(e => e.StudyTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<TawjihiCertificate>(entity =>
        {
            entity.ToTable("TawjihiCertificate", "admin");

            entity.Property(e => e.CertificateName)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "admin");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ImageUr)
                .HasMaxLength(125)
                .HasColumnName("ImageUR");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Mobile)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Users_Department");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Users)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Nationality");

            entity.HasOne(d => d.Section).WithMany(p => p.Users)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_Users_Section");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
