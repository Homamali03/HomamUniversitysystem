using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class StudyType
{
    public int Id { get; set; }

    public string StudyTypeName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
