using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Triage
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public Guid? IconId { get; set; }

    public int? Score { get; set; }

    public virtual Icon? Icon { get; set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
}
