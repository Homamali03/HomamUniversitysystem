using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class TawjihiCertificate
{
    public int Id { get; set; }

    public string CertificateName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
