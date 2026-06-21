using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class ErrorLogDTO
    {
        public string FullName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorException { get; set; }
        public string ModuleName { get; set; }
        public DateTime Trasnaction { get; set; }
        public string StackTrace { get; set; }

    }
}
