using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalRecordCall
{
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public Guid MedicalRecordId { get; set; }

    public long CallTimeSeconds { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
