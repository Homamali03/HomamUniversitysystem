using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool Gender { get; set; }

    public string Email { get; set; }

    public string Mobile { get; set; }

    public string Address { get; set; }

    public int NationalityId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public string ImageUr { get; set; }

    public virtual ICollection<AssignUsersToRole> AssignUsersToRoles { get; set; } = new List<AssignUsersToRole>();

    public virtual Department Department { get; set; }

    public virtual ICollection<ErrorLog> ErrorLogs { get; set; } = new List<ErrorLog>();

    public virtual Nationality Nationality { get; set; }

    public virtual Section Section { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
