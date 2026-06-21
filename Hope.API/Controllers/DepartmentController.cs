using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISectionRepository _sectionRepository;
        public DepartmentController(IDepartmentRepository departmentRepository, ISectionRepository sectionRepository)
        {
            _departmentRepository = departmentRepository;
            _sectionRepository = sectionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllDepartment()
        {
            var list = _departmentRepository.GetAll();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Ok(jsonString);
        }
        public IActionResult GetAllSectionsByDepartmentId(int DepartmentId)
        {
            List<Infrastructure.DTO.SectionDTO> list = new List<Infrastructure.DTO.SectionDTO>();
            list = (from obj in _sectionRepository.Find(obj=>obj.DepartmentId == DepartmentId)
             select new Infrastructure.DTO.SectionDTO
             {
                 Id = obj.Id,
                 Name = obj.SectionName,
             }).ToList();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(jsonString);
        }
    }
}
