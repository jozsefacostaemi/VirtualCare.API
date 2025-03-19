using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParaclinicalSend
{
    public Guid Id { get; set; }

    public Guid ParaclinicalHedId { get; set; }

    public bool Send { get; set; }

    public DateTime SendAt { get; set; }

    public string Response { get; set; } = null!;

    public virtual ParaclinicalHed ParaclinicalHed { get; set; } = null!;
}
