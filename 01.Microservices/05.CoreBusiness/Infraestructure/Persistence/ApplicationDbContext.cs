using Application.Data;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher _publisher;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<BusinessLine> BusinessLines { get; set; }
    public DbSet<MedicalRecordHistory> MedicalRecordHistories { get; set; }
    public DbSet<PatientState> PatientStates { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<BusinessLineLevelValueQueueConfig> BusinessLineLevelValueQueueConfigs { get; set; }
    public DbSet<QueueConf> QueueConfs { get; set; }
    public DbSet<GeneratedQueue> GeneratedQueues { get; set; }
    public DbSet<ProcessMessage> ProcessMessages { get; set; }
    public DbSet<ProcessMessageErrorLog> ProcessMessageErrorLogs { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<MedicalRecordState> MedicalRecordStates { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<LevelQueue> LevelQueues { get; set; }
    public DbSet<UserService> UserServices { get; set; }
    public DbSet<UserExpire> UserExpires { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdministrationRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AdministrationRoutess");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Amd>(entity =>
        {
            entity.ToTable("AMD");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.AdrressReference).HasMaxLength(100);
            entity.Property(e => e.AmdclasificationId).HasColumnName("AMDClasificationId");
            entity.Property(e => e.CellPhone).HasMaxLength(15);
            entity.Property(e => e.ConfirmInfoAmd).HasColumnName("ConfirmInfoAMD");
            entity.Property(e => e.Copay).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.Phone).HasMaxLength(15);

            entity.HasOne(d => d.Amdclasification).WithMany(p => p.Amds)
                .HasForeignKey(d => d.AmdclasificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AMD_AMDClasifications");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Amds)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AMD_MedicalRecords");

            entity.HasOne(d => d.Neighborhood).WithMany(p => p.Amds)
                .HasForeignKey(d => d.NeighborhoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AMD_Neighborhoods");
        });

        modelBuilder.Entity<Amdclasification>(entity =>
        {
            entity.ToTable("AMDClasifications");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<BusinessLine>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.ExperienceCenterLeader).WithMany(p => p.BusinessLines)
                .HasForeignKey(d => d.ExperienceCenterLeaderId)
                .HasConstraintName("FK_BusinessLines_Users");

            entity.HasOne(d => d.Language).WithMany(p => p.BusinessLines)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessLines_Languages");

            entity.HasOne(d => d.LevelQueue).WithMany(p => p.BusinessLines)
                .HasForeignKey(d => d.LevelQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessLines_LevelQueues");
        });

        modelBuilder.Entity<BusinessLineLevelValueQueueConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LevelValue");

            entity.ToTable("BusinessLineLevelValueQueueConfig");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BusinessLine).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.BusinessLineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessLineLevelValueQueueConfig_BusinessLines");

            entity.HasOne(d => d.City).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_LevelValueQueueConfig_Cities");

            entity.HasOne(d => d.Country).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_LevelValueQueueConfig_Countries");

            entity.HasOne(d => d.Department).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_LevelValueQueueConfig_Departments");

            entity.HasOne(d => d.LevelQueue).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.LevelQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LevelValueQueueConfig_LevelQueues");

            entity.HasOne(d => d.Service).WithMany(p => p.BusinessLineLevelValueQueueConfigs)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LevelValueQueueConfig_Services");
        });

        modelBuilder.Entity<BusinessLineSubmenu>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.BusinessLine).WithMany(p => p.BusinessLineSubmenus)
                .HasForeignKey(d => d.BusinessLineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessLineSubmenus_BusinessLines");

            entity.HasOne(d => d.Submenu).WithMany(p => p.BusinessLineSubmenus)
                .HasForeignKey(d => d.SubmenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessLineSubmenus_Submenus");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Cities)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Departments");
        });

        modelBuilder.Entity<ClinicalRecordGynObstHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GynecologicalObstetricHistories");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fpp)
                .HasColumnType("datetime")
                .HasColumnName("FPP");
            entity.Property(e => e.FromSo).HasColumnName("FromSO");
            entity.Property(e => e.Fum)
                .HasColumnType("datetime")
                .HasColumnName("FUM");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ClinicalRecordGynObstHistories)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GynecologicalObstetricHistories_Appointments");
        });

        modelBuilder.Entity<ClinicalRecordHistoriesType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MedicalHistoryTypes");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ClinicalRecordHistory>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FromSo).HasColumnName("FromSO");
            entity.Property(e => e.Value).HasMaxLength(50);

            entity.HasOne(d => d.ClinicalRecordType).WithMany()
                .HasForeignKey(d => d.ClinicalRecordTypeId)
                .HasConstraintName("FK_ClinicalRecordHistories_ClinicalRecordHistoriesTypes");

            entity.HasOne(d => d.MedicalRecord).WithMany()
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientHistories_Appointments");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.ColorHex)
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CompensationBox>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Country");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_States");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasOne(d => d.Country).WithMany(p => p.Departments)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Departments_Countries");
        });

        modelBuilder.Entity<Diagnostic>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CodeCie10).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DiagnosticImpression>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Diagnostic).WithMany(p => p.DiagnosticImpressionDiagnostics)
                .HasForeignKey(d => d.DiagnosticId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiagnosticImpressions_Diagnostics");

            entity.HasOne(d => d.DiagnosticRelated).WithMany(p => p.DiagnosticImpressionDiagnosticRelateds)
                .HasForeignKey(d => d.DiagnosticRelatedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiagnosticImpressions_Diagnostics1");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.DiagnosticImpressions)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiagnosticImpressions_Appointments");
        });

        modelBuilder.Entity<Disability>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDisability).HasColumnType("datetime");
            entity.Property(e => e.ReasonMedicalAttention).HasMaxLength(50);
            entity.Property(e => e.StartDisability).HasColumnType("datetime");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Disabilities)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disabilities_MedicalRecords");
        });

        modelBuilder.Entity<DisabilityResend>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ResendAt).HasColumnType("datetime");

            entity.HasOne(d => d.DisabilitySend).WithMany(p => p.DisabilityResends)
                .HasForeignKey(d => d.DisabilitySendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DisabilityResends_DisabilitySends");
        });

        modelBuilder.Entity<DisabilitySend>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SentAt).HasColumnType("datetime");

            entity.HasOne(d => d.Disability).WithMany(p => p.DisabilitySends)
                .HasForeignKey(d => d.DisabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DisabilitySends_Disabilities");
        });

        modelBuilder.Entity<Dosage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<GeneratedQueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GeneratedQueue");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.QueueConf).WithMany(p => p.GeneratedQueues)
                .HasForeignKey(d => d.QueueConfId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GeneratedQueues_QueueConfs");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<HealthEntity>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<HealthPolicy>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.HealthEntity).WithMany(p => p.HealthPolicies)
                .HasForeignKey(d => d.HealthEntityId)
                .HasConstraintName("FK_HealthPolicies_HealthEntities");
        });

        modelBuilder.Entity<Help>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Icon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Logos");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LevelQueue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Levels");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Appointments");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AutoNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FinishedAt).HasColumnType("datetime");
            entity.Property(e => e.StartedAt).HasColumnType("datetime");

            entity.HasOne(d => d.MedicalRecordState).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.MedicalRecordStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_AppointmentStates");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Appointments_Patients");

            entity.HasOne(d => d.ReferredService).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.ReferredServiceId)
                .HasConstraintName("FK_Appointments_ReferredServices");

            entity.HasOne(d => d.Service).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Appointments_Services");

            entity.HasOne(d => d.Triage).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.TriageId)
                .HasConstraintName("FK_MedicalCares_Triages");

            entity.HasOne(d => d.User).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Appointments_LogUser");
        });

        modelBuilder.Entity<MedicalRecordCall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MedicalCareCalls");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.MedicalRecordCalls)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalCareCalls_MedicalCares");
        });

        modelBuilder.Entity<MedicalRecordHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MedicalCaresHistories");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.MedicalRecordHistories)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalCaresHistories_MedicalCares");

            entity.HasOne(d => d.MedicalRecordState).WithMany(p => p.MedicalRecordHistories)
                .HasForeignKey(d => d.MedicalRecordStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalCaresHistories_MedicalCareStates");
        });

        modelBuilder.Entity<MedicalRecordLinkSend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MedicalCareLinkSends");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Links).HasMaxLength(500);

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.MedicalRecordLinkSends)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalCareLinkSends_MedicalCares");
        });

        modelBuilder.Entity<MedicalRecordState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AttentionStates");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MedicalShift>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Modules");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Modality>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Neighborhood>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.City).WithMany(p => p.Neighborhoods)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Neighborhoods_Cities");
        });

        modelBuilder.Entity<NotificationsDtl>(entity =>
        {
            entity.ToTable("NotificationsDtl");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.HexColor)
                .HasMaxLength(7)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Color).WithMany(p => p.NotificationsDtls)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK_NotificationsDtl_Colors");

            entity.HasOne(d => d.NotificationHed).WithMany(p => p.NotificationsDtls)
                .HasForeignKey(d => d.NotificationHedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationsDtl_NotificationsHead");

            entity.HasOne(d => d.SoundNavigation).WithMany(p => p.NotificationsDtls)
                .HasForeignKey(d => d.SoundId)
                .HasConstraintName("FK_NotificationsDtl_Sounds");
        });

        modelBuilder.Entity<NotificationsHead>(entity =>
        {
            entity.ToTable("NotificationsHead");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(7);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ParaclinicalDtl>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Exam).WithMany(p => p.ParaclinicalDtls)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParaclinicalDtls_Exams");

            entity.HasOne(d => d.ParaclinicalHed).WithMany(p => p.ParaclinicalDtls)
                .HasForeignKey(d => d.ParaclinicalHedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParaclinicalDtls_ParaclinicalHeds");
        });

        modelBuilder.Entity<ParaclinicalHed>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ParaclinicalHeds)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParaclinicalHeds_MedicalRecords");
        });

        modelBuilder.Entity<ParaclinicalSend>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SendAt).HasColumnType("datetime");

            entity.HasOne(d => d.ParaclinicalHed).WithMany(p => p.ParaclinicalSends)
                .HasForeignKey(d => d.ParaclinicalHedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParaclinicalSends_ParaclinicalHeds");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.AutoNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.CellPhone).HasMaxLength(15);
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LastSyncDateSo)
                .HasColumnType("datetime")
                .HasColumnName("LastSyncDateSO");
            entity.Property(e => e.PatientIdSo).HasColumnName("PatientIdSO");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.SecondLastName).HasMaxLength(50);
            entity.Property(e => e.SecondName).HasMaxLength(50);
            entity.Property(e => e.SendDocsToSms).HasColumnName("SendDocsToSMS");
            entity.Property(e => e.SendDocsToWa).HasColumnName("SendDocsToWA");
            entity.Property(e => e.Waphone)
                .HasMaxLength(15)
                .HasColumnName("WAPhone");

            entity.HasOne(d => d.BusinessLine).WithMany(p => p.Patients)
                .HasForeignKey(d => d.BusinessLineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_BusinessLines");

            entity.HasOne(d => d.City).WithMany(p => p.Patients)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Cities");

            entity.HasOne(d => d.CompensationBox).WithMany(p => p.Patients)
                .HasForeignKey(d => d.CompensationBoxId)
                .HasConstraintName("FK_Patients_CompensationBoxes");

            entity.HasOne(d => d.Gender).WithMany(p => p.Patients)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_Patients_Genders");

            entity.HasOne(d => d.HealthEntity).WithMany(p => p.Patients)
                .HasForeignKey(d => d.HealthEntityId)
                .HasConstraintName("FK_Patients_HealthEntities");

            entity.HasOne(d => d.HealthPolicy).WithMany(p => p.Patients)
                .HasForeignKey(d => d.HealthPolicyId)
                .HasConstraintName("FK_Patients_HealthPolicies");

            entity.HasOne(d => d.PatientState).WithMany(p => p.Patients)
                .HasForeignKey(d => d.PatientStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_PatientStates");

            entity.HasOne(d => d.Plan).WithMany(p => p.Patients)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK_Patients_Plans");

            entity.HasOne(d => d.Regimen).WithMany(p => p.Patients)
                .HasForeignKey(d => d.RegimenId)
                .HasConstraintName("FK_Patients_Regimens");
        });

        modelBuilder.Entity<PatientAttachment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FileType).HasMaxLength(100);
            entity.Property(e => e.OriginCode).HasMaxLength(10);

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.PatientAttachments)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientAttachments_Appointments");
        });

        modelBuilder.Entity<PatientHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Cellphone).HasMaxLength(15);
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FromSo).HasColumnName("FromSO");
            entity.Property(e => e.WhatsAppPhone).HasMaxLength(15);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientHistories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientHistories_Patients");
        });

        modelBuilder.Entity<PatientInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InfoPatients");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.PatientInformations)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientInformations_Appointments");
        });

        modelBuilder.Entity<PatientState>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PatientStateHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientStateHistories)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientStateHistories_Patients");

            entity.HasOne(d => d.PatientState).WithMany(p => p.PatientStateHistories)
                .HasForeignKey(d => d.PatientStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientStateHistories_PatientStates");
        });

        modelBuilder.Entity<PatientType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Plan");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PrescriptionHed>(entity =>
        {
            entity.ToTable("PrescriptionHed");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.PrescriptionHeds)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionHed_MedicalRecords");
        });

        modelBuilder.Entity<PrescriptionHedResend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PrescriptionHedResendHistories");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ResendAt).HasColumnType("datetime");

            entity.HasOne(d => d.PrescriptionHedSend).WithMany(p => p.PrescriptionHedResends)
                .HasForeignKey(d => d.PrescriptionHedSendId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionHedResendHistories_PrescriptionHedSendHistories");
        });

        modelBuilder.Entity<PrescriptionHedSend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PresctiptionHedSyncHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SendAt).HasColumnType("datetime");

            entity.HasOne(d => d.PrescriptionHed).WithMany(p => p.PrescriptionHedSends)
                .HasForeignKey(d => d.PrescriptionHedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PresctiptionHedSyncHistory_PrescriptionHed");
        });

        modelBuilder.Entity<PrescriptionsDtl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Prescriptions");

            entity.ToTable("PrescriptionsDtl");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.AdministrationRoute).WithMany(p => p.PrescriptionsDtls)
                .HasForeignKey(d => d.AdministrationRouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionsDtl_AdministrationRoutes");

            entity.HasOne(d => d.Dosage).WithMany(p => p.PrescriptionsDtls)
                .HasForeignKey(d => d.DosageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionsDtl_Dosages");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PrescriptionsDtls)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionsDtl_Medicines");

            entity.HasOne(d => d.PrescriptionHedNavigation).WithMany(p => p.PrescriptionsDtls)
                .HasForeignKey(d => d.PrescriptionHed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionsDtl_PrescriptionHed");

            entity.HasOne(d => d.Presentation).WithMany(p => p.PrescriptionsDtls)
                .HasForeignKey(d => d.PresentationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PrescriptionsDtl_Presentations");
        });

        modelBuilder.Entity<Presentation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProcessMessage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConsumedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.PublishedAt).HasColumnType("datetime");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.ProcessMessages)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProcessMessages_Appoinments");

            entity.HasOne(d => d.QueueConf).WithMany(p => p.ProcessMessages)
                .HasForeignKey(d => d.QueueConfId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProcessMessages_QueueConfs");
        });

        modelBuilder.Entity<ProcessMessageErrorLog>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(20);

            entity.HasOne(d => d.ProcessMessage).WithMany(p => p.ProcessMessageErrorLogs)
                .HasForeignKey(d => d.ProcessMessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProcessMessageErrorLogs_ProcessMessages");
        });

        modelBuilder.Entity<QueueConf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ConfQueues");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.NOrder).HasColumnName("nOrder");
            entity.Property(e => e.Nprocessor).HasColumnName("NProcessor");
            entity.Property(e => e.QueueDeadLetterExchange).HasMaxLength(50);
            entity.Property(e => e.QueueDeadLetterExchangeRoutingKey).HasMaxLength(50);
            entity.Property(e => e.QueueMode).HasMaxLength(10);

            entity.HasOne(d => d.BusinessLineLevelValueQueueConf).WithMany(p => p.QueueConfs)
                .HasForeignKey(d => d.BusinessLineLevelValueQueueConfId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QueueConfs_LevelValueQueueConfig");
        });

        modelBuilder.Entity<ReferredService>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Regimen>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AutoNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Color).WithMany(p => p.Services)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK_Services_Colors");

            entity.HasOne(d => d.Group).WithMany(p => p.Services)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Services_Groups");

            entity.HasOne(d => d.Icon).WithMany(p => p.Services)
                .HasForeignKey(d => d.IconId)
                .HasConstraintName("FK_Services_Logos");

            entity.HasOne(d => d.Modality).WithMany(p => p.Services)
                .HasForeignKey(d => d.ModalityId)
                .HasConstraintName("FK_Services_Modalities");

            entity.HasOne(d => d.NotificationHed).WithMany(p => p.Services)
                .HasForeignKey(d => d.NotificationHedId)
                .HasConstraintName("FK_Services_NotificationsHead");
        });

        modelBuilder.Entity<ServicePriority>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Sound>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Sound1).HasColumnName("Sound");
        });

        modelBuilder.Entity<Submenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Submodules");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Menu).WithMany(p => p.Submenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submenus_Menus");
        });

        modelBuilder.Entity<TemplateConfig>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.TemplateType).WithMany(p => p.TemplateConfigs)
                .HasForeignKey(d => d.TemplateTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TemplateConfigs_TemplateTypes");
        });

        modelBuilder.Entity<TemplateType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Translation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Key).HasMaxLength(20);
            entity.Property(e => e.Value).HasMaxLength(200);

            entity.HasOne(d => d.Language).WithMany(p => p.Translations)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Translations_Languages");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MedicationName).HasMaxLength(100);

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Treatments_Appointments");
        });

        modelBuilder.Entity<Triage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Icon).WithMany(p => p.Triages)
                .HasForeignKey(d => d.IconId)
                .HasConstraintName("FK_Triages_Icons");
        });

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

            entity.HasOne(d => d.BusinessLine).WithMany(p => p.Users)
                .HasForeignKey(d => d.BusinessLineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_BusinessLines");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Cities");

            entity.HasOne(d => d.UserExpire).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserExpireId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserExpires");

            entity.HasOne(d => d.UserState).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserStates");
        });

        modelBuilder.Entity<UserExpire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LogUserExpires");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserHelp>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comments).HasMaxLength(100);
            entity.Property(e => e.ResolveAt).HasColumnType("datetime");

            entity.HasOne(d => d.Help).WithMany(p => p.UserHelps)
                .HasForeignKey(d => d.HelpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserHelps_Helps");

            entity.HasOne(d => d.User).WithMany(p => p.UserHelps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserHelps_Users");
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LogUserHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.UserHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUserHistory_LogUser");
        });

        modelBuilder.Entity<UserMedicalShift>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MedicalShift).WithMany(p => p.UserMedicalShifts)
                .HasForeignKey(d => d.MedicalShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMedicalShifts_MedicalShifts");

            entity.HasOne(d => d.User).WithMany(p => p.UserMedicalShifts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMedicalShifts_Users");
        });

        modelBuilder.Entity<UserService>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Service).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserServices_Services");

            entity.HasOne(d => d.User).WithMany(p => p.UserServices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserServices_Users");
        });

        modelBuilder.Entity<UserState>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserStateHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.UserStateHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserStateHistories_Users");

            entity.HasOne(d => d.UserState).WithMany(p => p.UserStateHistories)
                .HasForeignKey(d => d.UserStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserStateHistories_UserStates");
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(x => x.GetDomainEvents().Any())
            .SelectMany(e => e.GetDomainEvents());
        var result = await base.SaveChangesAsync(cancellationToken);
        foreach (var publish in domainEvents)
            await _publisher.Publish(publish, cancellationToken);
        return result;
    }
}