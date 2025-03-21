using System;
using System.Collections.Generic;
using Domain.Helpers;

namespace Domain.Entities;

public partial class Patient
{
    public Guid Id { get; set; }

    public Guid? PatientIdSo { get; set; }

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string LastName { get; set; } = null!;

    public string? SecondLastName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Identification { get; set; }

    public Guid? PlanId { get; set; }

    public Guid PatientStateId { get; set; }

    public Guid? HealthEntityId { get; set; }

    public Guid? CompensationBoxId { get; set; }

    public Guid BusinessLineId { get; set; }

    public int AutoNumber { get; set; }

    public Guid? HealthPolicyId { get; set; }

    public Guid? RegimenId { get; set; }

    public Guid? GenderId { get; set; }

    public string Email { get; set; } = null!;

    public string CellPhone { get; set; } = null!;

    public string? Waphone { get; set; }

    public bool SendDocsToEmail { get; set; }

    public bool SendDocsToWa { get; set; }

    public bool SendDocsToSms { get; set; }

    public string? Phone { get; set; }

    public Guid CityId { get; set; }

    public string? Address { get; set; }

    public DateTime? LastSyncDateSo { get; set; }

    public int? Comorbidities { get; set; }

    public virtual BusinessLine BusinessLine { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual CompensationBox? CompensationBox { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual HealthEntity? HealthEntity { get; set; }

    public virtual HealthPolicy? HealthPolicy { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<PatientHistory> PatientHistories { get; set; } = new List<PatientHistory>();

    public virtual PatientState PatientState { get; set; } = null!;

    public virtual ICollection<PatientStateHistory> PatientStateHistories { get; set; } = new List<PatientStateHistory>();

    public virtual Plan? Plan { get; set; }

    public virtual Regimen? Regimen { get; set; }
}
