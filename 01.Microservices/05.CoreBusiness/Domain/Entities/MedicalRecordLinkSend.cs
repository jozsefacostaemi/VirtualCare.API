using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicalRecordLinkSend
{
    public Guid Id { get; set; }

    public string Links { get; set; } = null!;

    public Guid MedicalRecordId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
