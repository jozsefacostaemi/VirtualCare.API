using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DisabilityResend
{
    public Guid Id { get; set; }

    public Guid DisabilitySendId { get; set; }

    public bool Resend { get; set; }

    public DateTime ResendAt { get; set; }

    public string Response { get; set; } = null!;

    public virtual DisabilitySend DisabilitySend { get; set; } = null!;
}
