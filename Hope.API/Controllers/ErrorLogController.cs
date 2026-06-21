using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    
    public class ErrorLogController : Controller
    {
        private readonly IErrorLogRepository _errorLogRepository;
        public ErrorLogController(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }

        public IActionResult GetAllErrors()
        {
            List<Infrastructure.DTO.ErrorLogDTO> lst = new List<Infrastructure.DTO.ErrorLogDTO>();
            lst = (from obj in _errorLogRepository.Find(x=>x.Id !=0,x=>x.User)
                   select new Infrastructure.DTO.ErrorLogDTO
                   {
                       FullName=obj.User.FirstName+""+obj.User.LastName,
                       ErrorException = obj.ErrorException,
                       ErrorMessage = obj.ErrorMessage,
                       Trasnaction = obj.Trasnaction,
                       StackTrace = obj.StackTrace,
                   }).ToList();
            return Ok(lst);
        }
    }
    
}
