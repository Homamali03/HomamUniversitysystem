using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Section
{
    public int Id { get; set; }

    public int? DepartmentId { get; set; }

    public string SectionName { get; set; }

    public virtual Department Department { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
