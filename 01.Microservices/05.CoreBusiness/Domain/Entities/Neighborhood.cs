using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Neighborhood
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CityId { get; set; }

    public virtual ICollection<Amd> Amds { get; set; } = new List<Amd>();

    public virtual City City { get; set; } = null!;
}
