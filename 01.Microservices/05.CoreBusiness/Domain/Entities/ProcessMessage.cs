using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ProcessMessage
{
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;

    public bool? Published { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool? Consumed { get; set; }

    public DateTime? ConsumedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid MedicalRecordId { get; set; }

    public Guid QueueConfId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual ICollection<ProcessMessageErrorLog> ProcessMessageErrorLogs { get; set; } = new List<ProcessMessageErrorLog>();

    public virtual QueueConf QueueConf { get; set; } = null!;
}
