using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TemplateType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public virtual ICollection<TemplateConfig> TemplateConfigs { get; set; } = new List<TemplateConfig>();
}
