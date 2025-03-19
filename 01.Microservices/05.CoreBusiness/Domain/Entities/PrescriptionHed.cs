using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PrescriptionHed
{
    public Guid Id { get; set; }

    public Guid MedicalRecordId { get; set; }

    public bool IsPos { get; set; }

    public bool IsCommercial { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<PrescriptionHedSend> PrescriptionHedSends { get; set; } = new List<PrescriptionHedSend>();

    public virtual ICollection<PrescriptionsDtl> PrescriptionsDtls { get; set; } = new List<PrescriptionsDtl>();
}
