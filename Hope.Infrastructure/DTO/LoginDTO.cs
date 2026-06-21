using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class LoginDTO
    {
        [Required(AllowEmptyStrings =false , ErrorMessage ="This Field is Required")]
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string Password { get; set; }
    }
}
