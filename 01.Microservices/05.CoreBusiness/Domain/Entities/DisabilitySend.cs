using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class DisabilitySend
{
    public Guid Id { get; set; }

    public Guid DisabilityId { get; set; }

    public bool Send { get; set; }

    public DateTime SentAt { get; set; }

    public string Response { get; set; } = null!;

    public virtual Disability Disability { get; set; } = null!;

    public virtual ICollection<DisabilityResend> DisabilityResends { get; set; } = new List<DisabilityResend>();
}
