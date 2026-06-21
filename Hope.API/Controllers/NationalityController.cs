using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class NationalityController : Controller
    {
        private readonly INationalityRepository _nationalityRepository;
        public NationalityController(INationalityRepository nationalityRepository)
        {
            _nationalityRepository = nationalityRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllNationslity()
        {
            var list = _nationalityRepository.GetAll();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
