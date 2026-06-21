using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class CustomListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CustomListDTO> ListDataMajors { get; set; }
        public List<CustomListDTO> ListStudyType { get; set; }
        public List<CustomListDTO> ListTawjihiCertificate { get; set; }
        public List<CustomListDTO> ListUsers { get; set; }
    }
    //public class List<CustomListDTO>
    //{
    //    public CustomListDTO sssss { get; set; }
    //}
}
