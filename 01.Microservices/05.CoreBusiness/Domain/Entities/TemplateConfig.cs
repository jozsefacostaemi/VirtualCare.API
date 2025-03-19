using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TemplateConfig
{
    public Guid Id { get; set; }

    public Guid TemplateTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Value { get; set; } = null!;

    public int Order { get; set; }

    public bool Pin { get; set; }

    public virtual TemplateType TemplateType { get; set; } = null!;
}
