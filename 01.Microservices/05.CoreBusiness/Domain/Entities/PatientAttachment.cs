using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PatientAttachment
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string FileType { get; set; } = null!;

    public byte[] FileData { get; set; } = null!;

    public long FileSize { get; set; }

    public Guid MedicalRecordId { get; set; }

    public string? OriginCode { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;
}
