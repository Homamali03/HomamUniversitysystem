using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Nationality
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
