using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Translation
{
    public Guid Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public Guid LanguageId { get; set; }

    public virtual Language Language { get; set; } = null!;
}
