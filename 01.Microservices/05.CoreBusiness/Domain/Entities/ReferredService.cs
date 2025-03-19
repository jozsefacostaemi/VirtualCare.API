using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ReferredService
{
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
