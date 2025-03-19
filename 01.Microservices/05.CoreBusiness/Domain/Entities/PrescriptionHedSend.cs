using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PrescriptionHedSend
{
    public Guid Id { get; set; }

    public Guid PrescriptionHedId { get; set; }

    public bool Send { get; set; }

    public DateTime SendAt { get; set; }

    public string Response { get; set; } = null!;

    public virtual PrescriptionHed PrescriptionHed { get; set; } = null!;

    public virtual ICollection<PrescriptionHedResend> PrescriptionHedResends { get; set; } = new List<PrescriptionHedResend>();
}
