using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class MajorController : Controller
    {
        private readonly IMajorRepository _majorRepository;
        public MajorController(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }
        public IActionResult GetAllMajors()
        {
            var list = _majorRepository.GetAll();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
