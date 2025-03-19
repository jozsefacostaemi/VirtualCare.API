using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Exam
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<ParaclinicalDtl> ParaclinicalDtls { get; set; } = new List<ParaclinicalDtl>();
}
