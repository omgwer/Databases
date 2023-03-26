﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Data;

#nullable disable

namespace Repository.Data.Migrations
{
    [DbContext(typeof(CourseDbContext))]
    partial class CourseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entity.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("text");

                    b.Property<string>("CourseModuleModuleId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseModuleModuleId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Entity.CourseModule", b =>
                {
                    b.Property<string>("ModuleId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ModuleId");

                    b.ToTable("CourseModule");
                });

            modelBuilder.Entity("Entity.CourseModuleStatus", b =>
                {
                    b.Property<string>("CourseModuleId")
                        .HasColumnType("text");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("EnrollmentId")
                        .HasColumnType("text");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.HasKey("CourseModuleId");

                    b.ToTable("CourseModuleStatus");
                });

            modelBuilder.Entity("Entity.CourseStatus", b =>
                {
                    b.Property<string>("EnrollmentId")
                        .HasColumnType("text");

                    b.Property<string>("CourseId")
                        .HasColumnType("text");

                    b.Property<string>("DeletedAt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseStatus");
                });

            modelBuilder.Entity("Entity.Course", b =>
                {
                    b.HasOne("Entity.CourseModule", null)
                        .WithMany("Courses")
                        .HasForeignKey("CourseModuleModuleId");
                });

            modelBuilder.Entity("Entity.CourseStatus", b =>
                {
                    b.HasOne("Entity.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Entity.CourseModule", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
