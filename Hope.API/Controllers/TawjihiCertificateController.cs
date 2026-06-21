using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class TawjihiCertificateController : Controller
    {
        private readonly ITawjihiCertificateRepository _tawjihiCertificateRepository;
        public TawjihiCertificateController(ITawjihiCertificateRepository tawjihiCertificateRepository)
        {
            _tawjihiCertificateRepository = tawjihiCertificateRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllTawjihiCertificate()
        {
            var list = _tawjihiCertificateRepository.GetAll();
            string jsonString = JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return Ok(jsonString);
        }
    }
}
