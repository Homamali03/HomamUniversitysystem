using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class ModuleRole
{
    public int Id { get; set; }

    public int ModuleId { get; set; }

    public int RoleId { get; set; }

    public virtual Module Module { get; set; }

    public virtual Role Role { get; set; }
}
