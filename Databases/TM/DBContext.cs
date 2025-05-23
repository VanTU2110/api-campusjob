using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apicampusjob.Databases.TM;

public partial class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Applications> Applications { get; set; }

    public virtual DbSet<Companies> Companies { get; set; }

    public virtual DbSet<Conversations> Conversations { get; set; }

    public virtual DbSet<DevvnQuanhuyen> DevvnQuanhuyen { get; set; }

    public virtual DbSet<DevvnTinhthanhpho> DevvnTinhthanhpho { get; set; }

    public virtual DbSet<DevvnXaphuongthitran> DevvnXaphuongthitran { get; set; }

    public virtual DbSet<Job> Job { get; set; }

    public virtual DbSet<JobSchedule> JobSchedule { get; set; }

    public virtual DbSet<JobSkill> JobSkill { get; set; }

    public virtual DbSet<Messages> Messages { get; set; }

    public virtual DbSet<Report> Report { get; set; }

    public virtual DbSet<Sessions> Sessions { get; set; }

    public virtual DbSet<Skills> Skills { get; set; }

    public virtual DbSet<Student> Student { get; set; }

    public virtual DbSet<StudentAvailability> StudentAvailability { get; set; }

    public virtual DbSet<StudentCv> StudentCv { get; set; }

    public virtual DbSet<StudentSkill> StudentSkill { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserWarning> UserWarning { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_520_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Applications>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("applications");

            entity.HasIndex(e => e.JobUuid, "FK_applications_ref_job");

            entity.HasIndex(e => new { e.StudentUuid, e.JobUuid }, "unq_student_job").IsUnique();

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AppliedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("applied_at");
            entity.Property(e => e.CoverLeter).HasColumnName("cover_leter");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasComment("1: active 0: lock")
                .HasColumnName("is_active");
            entity.Property(e => e.JobUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("job_uuid");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','interviewing','accepted','rejected','cancelled','hired')")
                .HasColumnName("status");
            entity.Property(e => e.StudentUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("student_uuid");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.JobUu).WithMany(p => p.Applications)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.JobUuid)
                .HasConstraintName("FK_applications_ref_job");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.Applications)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.StudentUuid)
                .HasConstraintName("FK_appications_ref_student");
        });

        modelBuilder.Entity<Companies>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Maqh, "FK_company_ref_quanhuyen");

            entity.HasIndex(e => e.Matp, "FK_company_ref_tinhtp");

            entity.HasIndex(e => e.UserUuid, "FK_company_ref_user").IsUnique();

            entity.HasIndex(e => e.Xaid, "FK_company_ref_xa");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Maqh)
                .HasMaxLength(20)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(20)
                .HasColumnName("xaid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("FK_company_ref_quanhuyen");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.Companies)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("FK_company_ref_tinhtp");

            entity.HasOne(d => d.UserUu).WithOne(p => p.Companies)
                .HasPrincipalKey<User>(p => p.Uuid)
                .HasForeignKey<Companies>(d => d.UserUuid)
                .HasConstraintName("FK_company_ref_user");

            entity.HasOne(d => d.Xa).WithMany(p => p.Companies)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("FK_company_ref_xa");
        });

        modelBuilder.Entity<Conversations>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("conversations");

            entity.HasIndex(e => e.CompanyUuid, "FK_converstation_ref_company");

            entity.HasIndex(e => new { e.StudentUuid, e.CompanyUuid }, "unq_student_company").IsUnique();

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("company_uuid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.StudentUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("student_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.CompanyUu).WithMany(p => p.Conversations)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.CompanyUuid)
                .HasConstraintName("FK_converstation_ref_company");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.Conversations)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.StudentUuid)
                .HasConstraintName("FK_converstation_ref_");
        });

        modelBuilder.Entity<DevvnQuanhuyen>(entity =>
        {
            entity.HasKey(e => e.Maqh).HasName("PRIMARY");

            entity
                .ToTable("devvn_quanhuyen")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Maqh)
                .HasMaxLength(20)
                .HasColumnName("maqh")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<DevvnTinhthanhpho>(entity =>
        {
            entity.HasKey(e => e.Matp).HasName("PRIMARY");

            entity
                .ToTable("devvn_tinhthanhpho")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Slug)
                .HasMaxLength(70)
                .HasColumnName("slug");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<DevvnXaphuongthitran>(entity =>
        {
            entity.HasKey(e => e.Xaid).HasName("PRIMARY");

            entity
                .ToTable("devvn_xaphuongthitran")
                .HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            entity.Property(e => e.Xaid)
                .HasMaxLength(20)
                .HasColumnName("xaid")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Maqh)
                .HasMaxLength(5)
                .HasColumnName("maqh")
                .UseCollation("utf8mb4_unicode_520_ci")
                .HasCharSet("utf8mb4");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Type)
                .HasMaxLength(70)
                .HasColumnName("type")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job");

            entity.HasIndex(e => e.CompanyUuid, "FK_job_ref_company");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CompanyUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("company_uuid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.JobType)
                .HasColumnType("enum('parttime','remote','freelance')")
                .HasColumnName("job_type");
            entity.Property(e => e.Requirements).HasColumnName("requirements");
            entity.Property(e => e.SalaryFixed)
                .HasPrecision(10, 2)
                .HasColumnName("salary_fixed");
            entity.Property(e => e.SalaryMax)
                .HasPrecision(10, 2)
                .HasColumnName("salary_max");
            entity.Property(e => e.SalaryMin)
                .HasPrecision(10, 2)
                .HasColumnName("salary_min");
            entity.Property(e => e.SalaryType)
                .HasColumnType("enum('hourly','monthly','daily','fixed')")
                .HasColumnName("salary_type");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.CompanyUu).WithMany(p => p.Job)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.CompanyUuid)
                .HasConstraintName("FK_job_ref_company");
        });

        modelBuilder.Entity<JobSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_schedule");

            entity.HasIndex(e => e.JobUuid, "FK_jobschedule_ref_job");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DayOfWeek)
                .HasColumnType("enum('monday','tuesday','wednesday','thursday','friday','saturday','sunday')")
                .HasColumnName("day_of_week");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("end_time");
            entity.Property(e => e.JobUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("job_uuid");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("start_time");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.JobUu).WithMany(p => p.JobSchedule)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.JobUuid)
                .HasConstraintName("FK_jobschedule_ref_job");
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("job_skill");

            entity.HasIndex(e => e.JobUuid, "FK_jobskill_ref_job");

            entity.HasIndex(e => e.SkillUuid, "FK_jobskill_ref_skill");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.JobUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("job_uuid");
            entity.Property(e => e.SkillUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("skill_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.JobUu).WithMany(p => p.JobSkill)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.JobUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_jobskill_ref_job");

            entity.HasOne(d => d.SkillUu).WithMany(p => p.JobSkill)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.SkillUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_jobskill_ref_skill");
        });

        modelBuilder.Entity<Messages>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.HasIndex(e => e.ConversationUuid, "FK_message_ref_convrsations");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.ConversationUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("conversation_uuid");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.SendAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("send_at");
            entity.Property(e => e.SenderUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("sender_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.ConversationUu).WithMany(p => p.Messages)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.ConversationUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_ref_convrsations");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("report");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Reason)
                .HasDefaultValueSql("'other'")
                .HasColumnType("enum('fake_information','scam_fraud','inappropriate','spam','duplicate','wrong_category','offensive','other')")
                .HasColumnName("reason");
            entity.Property(e => e.ReporterUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("reporter_uuid");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','resolved','rejected')")
                .HasColumnName("status");
            entity.Property(e => e.TargetType)
                .HasColumnType("enum('student','company','job')")
                .HasColumnName("target_type");
            entity.Property(e => e.TargetUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("target_uuid");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Sessions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sessions");

            entity.HasIndex(e => e.UserUuid, "FK_sessions_ref_user");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(12)")
                .HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");
            entity.Property(e => e.Status)
                .HasComment("0: Login ,1:Logout")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.TimeLogin)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("time_login");
            entity.Property(e => e.TimeLogout)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("time_logout");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .IsFixedLength()
                .HasColumnName("uuid");

            entity.HasOne(d => d.UserUu).WithMany(p => p.Sessions)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.UserUuid)
                .HasConstraintName("sessions_ibfk_1");
        });

        modelBuilder.Entity<Skills>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("skills");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student");

            entity.HasIndex(e => e.Maqh, "FK_student_ref_quanhuye");

            entity.HasIndex(e => e.Matp, "FK_student_ref_tinhtp");

            entity.HasIndex(e => e.UserUuid, "FK_student_ref_user").IsUnique();

            entity.HasIndex(e => e.Xaid, "FK_student_ref_xa");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasComment("0-Nam , 1-Nữ , 2 - khác")
                .HasColumnType("tinyint(2)")
                .HasColumnName("gender");
            entity.Property(e => e.Major)
                .HasMaxLength(255)
                .HasColumnName("major");
            entity.Property(e => e.Maqh)
                .HasMaxLength(20)
                .HasColumnName("maqh");
            entity.Property(e => e.Matp)
                .HasMaxLength(5)
                .HasColumnName("matp");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
            entity.Property(e => e.University)
                .HasMaxLength(255)
                .HasColumnName("university");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("user_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
            entity.Property(e => e.Xaid)
                .HasMaxLength(20)
                .HasColumnName("xaid");

            entity.HasOne(d => d.MaqhNavigation).WithMany(p => p.Student)
                .HasForeignKey(d => d.Maqh)
                .HasConstraintName("FK_student_ref_quanhuye");

            entity.HasOne(d => d.MatpNavigation).WithMany(p => p.Student)
                .HasForeignKey(d => d.Matp)
                .HasConstraintName("FK_student_ref_tinhtp");

            entity.HasOne(d => d.UserUu).WithOne(p => p.Student)
                .HasPrincipalKey<User>(p => p.Uuid)
                .HasForeignKey<Student>(d => d.UserUuid)
                .HasConstraintName("FK_student_ref_user");

            entity.HasOne(d => d.Xa).WithMany(p => p.Student)
                .HasForeignKey(d => d.Xaid)
                .HasConstraintName("FK_student_ref_xa");
        });

        modelBuilder.Entity<StudentAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student_availability");

            entity.HasIndex(e => e.StudentUuid, "FK_studentavailability_ref_student");

            entity.HasIndex(e => e.Uuid, "unq_uuid");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DayOfWeek)
                .HasColumnType("enum('monday','tuesday','wednesday','thursday','friday','saturday','sunday')")
                .HasColumnName("day_of_week");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("end_time");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("start_time");
            entity.Property(e => e.StudentUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("student_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.StudentAvailability)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.StudentUuid)
                .HasConstraintName("FK_studentavailability_ref_student");
        });

        modelBuilder.Entity<StudentCv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student_cv");

            entity.HasIndex(e => e.StudentUuid, "FK_cv_ref_student");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CloudinaryPublicId)
                .HasMaxLength(255)
                .HasColumnName("cloudinary_public_id");
            entity.Property(e => e.StudentUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("student_uuid");
            entity.Property(e => e.UploadAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("'0000-00-00 00:00:00'")
                .HasColumnType("datetime")
                .HasColumnName("upload_at");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.StudentCv)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.StudentUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cv_ref_student");
        });

        modelBuilder.Entity<StudentSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student_skill");

            entity.HasIndex(e => e.SkillUuid, "FK_studentskill_ref_skill");

            entity.HasIndex(e => e.StudentUuid, "FK_studentskill_ref_student");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Proficiency)
                .HasDefaultValueSql("'beginner'")
                .HasColumnType("enum('beginner','intermediate','advanced','expert')")
                .HasColumnName("proficiency");
            entity.Property(e => e.SkillUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("skill_uuid");
            entity.Property(e => e.Status)
                .HasComment("0: hoạt động, 1 khóa")
                .HasColumnType("tinyint(2)")
                .HasColumnName("status");
            entity.Property(e => e.StudentUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("student_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");

            entity.HasOne(d => d.SkillUu).WithMany(p => p.StudentSkill)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.SkillUuid)
                .HasConstraintName("FK_studentskill_ref_skill");

            entity.HasOne(d => d.StudentUu).WithMany(p => p.StudentSkill)
                .HasPrincipalKey(p => p.Uuid)
                .HasForeignKey(d => d.StudentUuid)
                .HasConstraintName("FK_studentskill_ref_student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Uuid, "unq_uuid").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsVerify)
                .HasDefaultValueSql("'0'")
                .HasColumnName("isVerify");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasComment("0-sinh vien 1-nha tuyen dung(cty)-3 admin")
                .HasColumnType("tinyint(2)")
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("0-Khóa, 1-Đang hoạt động")
                .HasColumnType("tinyint(4)")
                .HasColumnName("status");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<UserWarning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_warning");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Messages)
                .HasColumnType("text")
                .HasColumnName("messages");
            entity.Property(e => e.TargetType)
                .HasColumnType("enum('student','company')")
                .HasColumnName("target_type");
            entity.Property(e => e.TargetUuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("target_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("uuid()")
                .HasColumnName("uuid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
