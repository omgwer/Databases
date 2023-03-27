using Entity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data;

public partial class CourseDbContext : DbContext
{
    public CourseDbContext()
    {
    }
    
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
    {
    }

  //  protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
        // modelBuilder.ApplyConfiguration(new CourseConfiguration());
        // modelBuilder.ApplyConfiguration(new CourseModuleConfiguration());
        // modelBuilder.ApplyConfiguration(new CourseModuleStatusConfiguration());
        // modelBuilder.ApplyConfiguration(new CourseStatusConfiguration());
        // для каждой таблицы добавлять в билдер конфиг.
  //  }
    
      public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<CourseModule> CourseModules { get; set; }

    public virtual DbSet<CourseModuleStatus> CourseModuleStatuses { get; set; }

    public virtual DbSet<CourseStatus> CourseStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Database=ips_labs_4; Username=postgres; Password=12345678; Port= 5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.CourseId)
                .HasMaxLength(36)
                .HasColumnName("course_id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Version)
                .ValueGeneratedOnAdd()
                .HasColumnName("version");
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("course_enrollment_pkey");

            entity.ToTable("course_enrollment");

            entity.Property(e => e.EnrollmentId)
                .HasMaxLength(36)
                .HasColumnName("enrollment_id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(36)
                .HasColumnName("course_id");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("course_enrollment_course_id_fkey");

            entity.HasOne(d => d.Enrollment).WithOne(p => p.CourseEnrollment)
                .HasForeignKey<CourseEnrollment>(d => d.EnrollmentId)
                .HasConstraintName("course_enrollment_enrollment_id_fkey");
        });

        modelBuilder.Entity<CourseModule>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("course_module_pkey");

            entity.ToTable("course_module");

            entity.Property(e => e.ModuleId)
                .HasMaxLength(36)
                .HasColumnName("module_id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(36)
                .HasColumnName("course_id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsRequired)
                .HasMaxLength(5)
                .HasColumnName("is_required");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseModules)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("course_module_course_id_fkey");
        });

        modelBuilder.Entity<CourseModuleStatus>(entity =>
        {
            entity.HasKey(e => new { e.EnrollmentId, e.ModuleId }).HasName("course_module_status_pkey");

            entity.ToTable("course_module_status");

            entity.Property(e => e.EnrollmentId)
                .HasMaxLength(36)
                .HasColumnName("enrollment_id");
            entity.Property(e => e.ModuleId)
                .HasMaxLength(36)
                .HasColumnName("module_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Progress)
                .HasPrecision(3)
                .HasColumnName("progress");

            entity.HasOne(d => d.Enrollment).WithMany(p => p.CourseModuleStatuses)
                .HasForeignKey(d => d.EnrollmentId)
                .HasConstraintName("course_module_status_enrollment_id_fkey");

            entity.HasOne(d => d.Module).WithMany(p => p.CourseModuleStatuses)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("course_module_status_module_id_fkey");
        });

        modelBuilder.Entity<CourseStatus>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("course_status_pkey");

            entity.ToTable("course_status");

            entity.Property(e => e.EnrollmentId)
                .HasMaxLength(36)
                .HasColumnName("enrollment_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Progress)
                .HasPrecision(3)
                .HasColumnName("progress");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}