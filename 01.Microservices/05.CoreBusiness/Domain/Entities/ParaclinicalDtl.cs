using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ParaclinicalDtl
{
    public Guid Id { get; set; }

    public Guid ParaclinicalHedId { get; set; }

    public Guid ExamId { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual ParaclinicalHed ParaclinicalHed { get; set; } = null!;
}
