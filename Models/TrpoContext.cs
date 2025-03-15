using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DartSklad.Models;

public partial class TrpoContext : DbContext
{
    public TrpoContext()
    {
    }

    public TrpoContext(DbContextOptions<TrpoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectType> SubjectTypes { get; set; }

    public virtual DbSet<SubjectsStorage> SubjectsStorages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=trpo;Username=postgres;Password=mysecretpassword");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BeginDate).HasColumnName("begin_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ServiceTitle).HasColumnName("service_title");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Project).WithMany(p => p.Events)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("events_project_id_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BeginDate).HasColumnName("begin_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("storages_pkey");

            entity.ToTable("storages");

            entity.HasIndex(e => e.Title, "storages_title_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .HasColumnName("title");

            entity.HasOne(d => d.Event).WithMany(p => p.Storages)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("storages_event_id_fkey");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Price)
                .HasDefaultValue(0)
                .HasColumnName("price");
            entity.Property(e => e.SerialNumber).HasColumnName("serial_number");
            entity.Property(e => e.SubjectTypeId).HasColumnName("subject_type_id");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.SubjectType).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.SubjectTypeId)
                .HasConstraintName("subjects_subject_type_id_fkey");
        });

        modelBuilder.Entity<SubjectType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subject_types_pkey");

            entity.ToTable("subject_types");

            entity.HasIndex(e => e.Title, "subject_types_title_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SubjectsStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subjects_storage_pkey");

            entity.ToTable("subjects_storage");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.StorageId).HasColumnName("storage_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Storage).WithMany(p => p.SubjectsStorages)
                .HasForeignKey(d => d.StorageId)
                .HasConstraintName("subjects_storage_storage_id_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectsStorages)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("subjects_storage_subject_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
