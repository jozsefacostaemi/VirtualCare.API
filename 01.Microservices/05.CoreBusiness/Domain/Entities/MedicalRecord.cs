using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalRecord
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ServiceId { get; set; }

    public Guid MedicalRecordStateId { get; set; }

    public Guid? ReferredServiceId { get; set; }

    public int AutoNumber { get; set; }

    public Guid? TriageId { get; set; }

    public string? ParamedicComments { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public bool? Open { get; set; }

    public bool? Active { get; set; }

    public int? Priority { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Comments { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Amd> Amds { get; set; } = new List<Amd>();

    public virtual ICollection<ClinicalRecordGynObstHistory> ClinicalRecordGynObstHistories { get; set; } = new List<ClinicalRecordGynObstHistory>();

    public virtual ICollection<DiagnosticImpression> DiagnosticImpressions { get; set; } = new List<DiagnosticImpression>();

    public virtual ICollection<Disability> Disabilities { get; set; } = new List<Disability>();

    public virtual ICollection<MedicalRecordCall> MedicalRecordCalls { get; set; } = new List<MedicalRecordCall>();

    public virtual ICollection<MedicalRecordHistory> MedicalRecordHistories { get; set; } = new List<MedicalRecordHistory>();

    public virtual ICollection<MedicalRecordLinkSend> MedicalRecordLinkSends { get; set; } = new List<MedicalRecordLinkSend>();

    public virtual MedicalRecordState MedicalRecordState { get; set; } = null!;

    public virtual ICollection<ParaclinicalHed> ParaclinicalHeds { get; set; } = new List<ParaclinicalHed>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientAttachment> PatientAttachments { get; set; } = new List<PatientAttachment>();

    public virtual ICollection<PatientInformation> PatientInformations { get; set; } = new List<PatientInformation>();

    public virtual ICollection<PrescriptionHed> PrescriptionHeds { get; set; } = new List<PrescriptionHed>();

    public virtual ICollection<ProcessMessage> ProcessMessages { get; set; } = new List<ProcessMessage>();

    public virtual ReferredService? ReferredService { get; set; }

    public virtual Service? Service { get; set; }

    public virtual ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

    public virtual Triage? Triage { get; set; }

    public virtual User? User { get; set; }
}
