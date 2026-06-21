using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class StudentController : Controller
    {
        private readonly IStudyTypeRepository _studyTypeRepository;
        private readonly ITawjihiCertificateRepository _tawjihiCertificateRepository;
        private readonly IMajorRepository _majorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        public StudentController(IMajorRepository majorRepository, IStudyTypeRepository studyTypeRepository, ITawjihiCertificateRepository tawjihiCertificateRepository, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _majorRepository = majorRepository;
            _studyTypeRepository = studyTypeRepository;
            _tawjihiCertificateRepository = tawjihiCertificateRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }
        public IActionResult GetListsToCreateStudent()
        {
            Infrastructure.DTO.CustomListDTO obj = new Infrastructure.DTO.CustomListDTO();
            //List<Infrastructure.DTO.CustomListDTO> lstMajors = new List<Infrastructure.DTO.CustomListDTO>();
            //List<Infrastructure.DTO.CustomListDTO> lstStudyType = new List<Infrastructure.DTO.CustomListDTO>();
            //List<Infrastructure.DTO.CustomListDTO> lstTawjihiCertificate = new List<Infrastructure.DTO.CustomListDTO>();
            obj.ListDataMajors = (from x in _majorRepository.GetAll()
                       select new Infrastructure.DTO.CustomListDTO
                       {
                           Id = x.Id,
                           Name=x.MajorName,
                       }).ToList();

            obj.ListStudyType = (from x in _studyTypeRepository.GetAll()
                         select new Infrastructure.DTO.CustomListDTO
                         {
                             Id = x.Id,
                             Name = x.StudyTypeName,
                         }).ToList();


            List<int> TempStudent = _studentRepository.GetAll().Select(x=>x.UserId).ToList();

            obj.ListUsers = (from x in _userRepository.Find(x=>!TempStudent.Contains(x.Id))
                          select new Infrastructure.DTO.CustomListDTO
                          {
                             Id = x.Id,
                             Name = x.FirstName+""+x.LastName,
                          }).ToList();
            //obj.ListDataMajor = new List<Infrastructure.DTO.CustomListDTO>();
            //obj.ListData.AddRange(lstMajors);
            //obj.ListData.AddRange(lstStudyType);
            //obj.ListData.AddRange(lstTawjihiCertificate);
            //http://localhost:5121/api/Student/GetListsToCreateStudent

            string jsonString = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddNewStudent(Infrastructure.DTO.StudentDTO studentDTO)
        {
            DomainEntities.DBEntities.Student obj = new DomainEntities.DBEntities.Student();
            obj.TawjihiCertificateld = studentDTO.TawjihiCertificateId;
            obj.UserId = studentDTO.UserId;
            obj.MajorId = studentDTO.MajorId;
            obj.StudyTypeId = studentDTO.StudyTypeId;
            obj.TawjihiAvg = studentDTO.TawjihiAVG;
            obj.GraduationYear = studentDTO.GraduationYear;

            _studentRepository.Add(obj);
            return Ok("Success");
        }
        public IActionResult GetAllStudent()
        {
            List<Infrastructure.DTO.StudentDTO> lst = new List<Infrastructure.DTO.StudentDTO>();
            lst = (from obj in _studentRepository.Find(x => x.Id != 0, x => x.Major, x => x.User, x => x.StudyType ,x=>x.TawjihiCertificate)
                   select new Infrastructure.DTO.StudentDTO
                   {
                       Id = obj.Id,
                       MajorName = obj.Major.MajorName,
                       StudyTypeName = obj.StudyType.StudyTypeName,
                       TawjihiCertificateName =obj.TawjihiCertificate.CertificateName,
                       GraduationYear = obj.GraduationYear,
                       FullName = obj.User.FirstName+""+obj.User.LastName,
                       TawjihiAVG = obj.TawjihiAvg,
                   }).ToList();
                string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                return Ok(jsonString);
        }
    }
}
