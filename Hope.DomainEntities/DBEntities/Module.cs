using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Module
{
    public int Id { get; set; }

    public string ModuleName { get; set; }

    public string ModuleUrl { get; set; }

    public virtual ICollection<ModuleRole> ModuleRoles { get; set; } = new List<ModuleRole>();
}
