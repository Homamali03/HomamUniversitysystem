using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Department
{
    public int Id { get; set; }

    public string DepartmentName { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
