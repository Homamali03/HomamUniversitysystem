using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MajorId { get; set; }
        public int StudyTypeId { get; set; }
        public int TawjihiCertificateId { get; set; }
        public string GraduationYear { get; set; }
        public double TawjihiAVG {  get; set; }
        public string FullName { get; set; }
        public string MajorName { get; set; }
        public string StudyTypeName { get; set; }
        public string TawjihiCertificateName { get; set; }

    }
}
