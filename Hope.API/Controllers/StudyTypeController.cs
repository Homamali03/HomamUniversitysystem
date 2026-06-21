using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class StudyTypeController : Controller
    {
        private readonly IStudyTypeRepository _studyTypeRepository;
        public StudyTypeController(IStudyTypeRepository studyTypeRepository)
        {
            _studyTypeRepository = studyTypeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllStudyType()
        {
            var list = _studyTypeRepository.GetAll();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
