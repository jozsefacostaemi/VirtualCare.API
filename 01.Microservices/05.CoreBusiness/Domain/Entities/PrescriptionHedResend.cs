using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class PrescriptionHedResend
{
    public Guid Id { get; set; }

    public Guid PrescriptionHedSendId { get; set; }

    public bool Resend { get; set; }

    public DateTime ResendAt { get; set; }

    public string Response { get; set; } = null!;

    public virtual PrescriptionHedSend PrescriptionHedSend { get; set; } = null!;
}
