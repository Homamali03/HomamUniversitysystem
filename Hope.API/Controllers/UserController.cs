using Hope.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly INationalityRepository _nationalityRepository;
        private readonly IAssignUserToRoleRepository _assignUserToRoleRepository;
        private readonly IModulesRepository _modulesRepository;
        private readonly IModuleRoleRepository _moduleRoleRepository;
        Infrastructure.Helper.HopeErrorLog obj = new Infrastructure.Helper.HopeErrorLog();
        public UserController(IUserRepository userRepository, INationalityRepository nationalityRepository 
            ,IModuleRoleRepository moduleRoleRepository,IModulesRepository modulesRepository,IAssignUserToRoleRepository assignUserToRoleRepository)
        {
            _userRepository = userRepository;
            _nationalityRepository = nationalityRepository;
            _moduleRoleRepository = moduleRoleRepository;
            _modulesRepository = modulesRepository;
            _assignUserToRoleRepository = assignUserToRoleRepository;
        }
        
        public IActionResult AddNewUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            try
            {
                DomainEntities.DBEntities.User obj = new DomainEntities.DBEntities.User();
                obj.FirstName = userDTO.FirstName;
                obj.LastName = userDTO.LastName;
                obj.Email = userDTO.Email;
                obj.DateOfBirth = userDTO.DateOfBirth;
                obj.Mobile = userDTO.Mobile;
                obj.Gender = userDTO.Gender;
                obj.Address = userDTO.Address;
                obj.CreatedDate = DateTime.Now;
                obj.NationalityId = userDTO.NationalityId;
                obj.Username = userDTO.Username;
                obj.Password = userDTO.Password;
                obj.DepartmentId = userDTO.DepartmentId;
                obj.SectionId = userDTO.SectionId;
                obj.ImageUr = userDTO.ImageFullPath;
                _userRepository.Add(obj);

                return Ok("Success");
            }
            catch (Exception ex)
            {


                obj.AddErrorLog(ex, "2", "User Module");
                return BadRequest("Error");
            }
        }

        public IActionResult GetAllUsers()
        {
            Infrastructure.Base.Security security = new Infrastructure.Base.Security();
            try
            {
                List<Infrastructure.DTO.UserDTO> lst = new List<Infrastructure.DTO.UserDTO>();
                lst = (from obj in _userRepository.Find(x => x.Id != 0, x => x.Nationality)
                           //join _na in _nationalityRepository.GetAll() on obj.NationalityId equals _na.Id
                       select new Infrastructure.DTO.UserDTO
                       {
                           UserId = obj.Id,
                           EncryptUserId = security.EncryptString(obj.Id.ToString()),
                           FirstName = obj.FirstName,
                           LastName = obj.LastName,
                           Email = obj.Email,
                           DateOfBirth = obj.DateOfBirth,
                           Mobile = obj.Mobile,
                           Address = obj.Address,
                           NationalityName = obj.Nationality.Name,
                           //_na.Name,
                           GenderDisplayName = obj.Gender == true ? "Male" : "Female",
                       }).ToList();
                string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                return Ok(jsonString);

            }
            catch (Exception ex)
            {

                obj.AddErrorLog(ex, "2", "User Module");
                return BadRequest("Error");
            }
        }

        public IActionResult DeleteUser(int UserId)
        {
            try
            {


                DomainEntities.DBEntities.User obj = new DomainEntities.DBEntities.User();
                obj = _userRepository.Find(x => x.Id == UserId).FirstOrDefault();
                _userRepository.Delete(obj);
                return Ok("Success");
            }
            catch (Exception ex)
            {

                obj.AddErrorLog(ex, "2", "User Module");
                return BadRequest("Error");
            }

        }

        public IActionResult GetUserById(string UserId)
        {
            try
            {
                Infrastructure.Base.Security security = new Infrastructure.Base.Security();
                int _UserId = Convert.ToInt32(security.DecryptString(UserId));

                Infrastructure.DTO.UserDTO userDTO = new Infrastructure.DTO.UserDTO();
                userDTO = (from obj in _userRepository.Find(x => x.Id == _UserId)
                           select new Infrastructure.DTO.UserDTO
                           {
                               UserId = obj.Id,
                               FirstName = obj.FirstName,
                               LastName = obj.LastName,
                               Email = obj.Email,
                               DateOfBirth = obj.DateOfBirth,
                               Mobile = obj.Mobile,
                               Address = obj.Address,
                               NationalityId = obj.NationalityId,
                               Gender = obj.Gender,
                           }).FirstOrDefault();
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                obj.AddErrorLog(ex, "2", "User Module");
                return BadRequest("Error");
            }
        }
        public IActionResult GetProfile(int userId)
        {
            Infrastructure.DTO.UserDTO userDTO = new Infrastructure.DTO.UserDTO();
            userDTO = (from obj in _userRepository.Find(x => x.Id == userId)
                       select new Infrastructure.DTO.UserDTO
                       {
                           UserId = obj.Id,
                           FirstName = obj.FirstName,
                           LastName = obj.LastName,
                           Email = obj.Email,
                           DateOfBirth = obj.DateOfBirth,
                           Mobile = obj.Mobile,
                           Address = obj.Address,
                           NationalityId = obj.NationalityId,
                           Gender = obj.Gender,
                           ImageFullPath = obj.ImageUr,
                       }).FirstOrDefault();
            return Ok(userDTO);
        }
        public IActionResult GetFullNameById(int UserId)
        {
            var result = _userRepository.Find(x => x.Id == UserId).FirstOrDefault();
            return Ok(result.FirstName + " " + result.LastName);
        }
        public IActionResult UpdateUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            try
            {
                DomainEntities.DBEntities.User obj = new DomainEntities.DBEntities.User();
                obj = _userRepository.Find(x => x.Id == userDTO.UserId).FirstOrDefault();
                obj.FirstName = userDTO.FirstName;
                obj.LastName = userDTO.LastName;
                obj.Email = userDTO.Email;
                obj.DateOfBirth = userDTO.DateOfBirth;
                obj.Mobile = userDTO.Mobile;
                obj.Address = userDTO.Address;
                obj.Gender = userDTO.Gender;
                obj.NationalityId = userDTO.NationalityId;
                _userRepository.Update(obj);
                return Ok("OK");
            }

            catch (Exception ex)
            {
                obj.AddErrorLog(ex, "2", "User Module");
                return BadRequest("Error");
            }
        }
        public IActionResult Login(Infrastructure.DTO.LoginDTO loginDTO)
        {
            var result = _userRepository.Find(x => x.Username == loginDTO.UserName &&
            x.Password == loginDTO.Password).FirstOrDefault();
            if (result != null)
            {
                return Ok(result.Id);
            }
            else
            {
                return BadRequest(-1);
            }
        }
        public IActionResult GetAllPermissionByUserId(int UserId)
        {
            List<int> lstRole = new List<int>();
            lstRole = _assignUserToRoleRepository.Find(x => x.UserId == UserId).Select(x => x.RoleId).ToList();
            List<int> lstModules = new List<int>();
            lstModules = _moduleRoleRepository.Find(x => lstRole.Contains(x.RoleId)).Select(x => x.ModuleId).Distinct().ToList();
            Infrastructure.DTO.MenuPermissionDTO menuPermissionDTO = new Infrastructure.DTO.MenuPermissionDTO();
            if(lstModules.Contains(1))            
                menuPermissionDTO.User = "True";
            if(lstRole.Contains(2))
                menuPermissionDTO.Student = "True";

            return Ok(menuPermissionDTO);

        }
        
    }
}
