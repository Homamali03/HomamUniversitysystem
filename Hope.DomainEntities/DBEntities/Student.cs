using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class Student
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string GraduationYear { get; set; }

    public int TawjihiCertificateld { get; set; }

    public double TawjihiAvg { get; set; }

    public int MajorId { get; set; }

    public int StudyTypeId { get; set; }

    public virtual Major Major { get; set; }

    public virtual StudyType StudyType { get; set; }

    public virtual TawjihiCertificate TawjihiCertificateldNavigation { get; set; }

    public virtual User User { get; set; }
}
