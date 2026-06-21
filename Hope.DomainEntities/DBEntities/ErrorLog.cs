using System;
using System.Collections.Generic;

namespace Hope.DomainEntities.DBEntities;

public partial class ErrorLog
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorException { get; set; }

    public string ModuleName { get; set; }

    public DateTime Trasnaction { get; set; }

    public string StackTrace { get; set; }

    public virtual User User { get; set; }
}
