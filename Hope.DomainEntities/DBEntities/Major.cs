using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Major
{
    public int Id { get; set; }

    public string MajorName { get; set; }

    public double MinimumAvg { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
