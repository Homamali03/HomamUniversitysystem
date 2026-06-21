using Hope.Infrastructure.Base;
using Hope.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Hope.UI.Controllers
{

    public class UserController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/Nationality/GetAllNationslity");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;


                if (apiResponse == "Error")
                {
                    return RedirectToAction("ErrorPage", "Home");
                }
                List<Hope.Infrastructure.DTO.NationalityDTO> lst = new List<Infrastructure.DTO.NationalityDTO>();
                lst = JsonConvert.DeserializeObject<List<Infrastructure.DTO.NationalityDTO>>(apiResponse);
                ViewBag.Nationality = lst;
            }

            var responseDep = await client.GetAsync("http://localhost:5121/api/Department/GetAllDepartment");

            if (responseDep.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string apiResponse = responseDep.Content.ReadAsStringAsync().Result;


                if (apiResponse == "Error")
                {
                    return RedirectToAction("ErrorPage", "Home");
                }
                List<Hope.Infrastructure.DTO.DepartmentDTO> lst = new List<Infrastructure.DTO.DepartmentDTO>();
                lst = JsonConvert.DeserializeObject<List<Infrastructure.DTO.DepartmentDTO>>(apiResponse);
                ViewBag.Department = lst;
            }

            return View();
        }
        public async Task<IActionResult> Update(string Id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/GetUserById?UserId=" + Id);
            var apiResponse = await response.Content.ReadAsStringAsync();
            if (apiResponse == "Error")
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            Infrastructure.DTO.UserDTO userDTO = JsonConvert.DeserializeObject<Infrastructure.DTO.UserDTO>(apiResponse);
            var responseNat = await client.GetAsync("http://localhost:5121/api/Nationality/GetAllNationslity");
            string apiResponseNat = response.Content.ReadAsStringAsync().Result;
            List<Hope.Infrastructure.DTO.NationalityDTO> lst = new List<Infrastructure.DTO.NationalityDTO>();
            lst = JsonConvert.DeserializeObject<List<Infrastructure.DTO.NationalityDTO>>(apiResponseNat);
            ViewBag.Nationality = lst;
            return View(userDTO);
        }
        public async Task<IActionResult> UpdateUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            HttpClient client = new HttpClient();
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);
            var response = await client.PostAsync("http://localhost:5121/api/User/UpdateUser",
                new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));
            return RedirectToAction("GetAllUsers");
        }
        public async Task<IActionResult> AddNewUser(Hope.Infrastructure.DTO.UserDTO userDTO,IFormFile files)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = files.FileName;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                files.CopyTo(stream);
            }
            var host= HttpContext.Request.Host.Value;
            userDTO.ImageFullPath = "http://"+host+"/Files/" + fileName;
            //userDTO.Image = null;
            HttpClient client = new HttpClient();
            Hope.Infrastructure.Base.Security obj = new Security();
            userDTO.Password = obj.EncryptString(userDTO.Password);
            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);
            var response = await client.PostAsync("http://localhost:5121/api/User/AddNewUser",
                new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));
            
            return RedirectToAction("GetAllUsers");
        }
        public async Task<IActionResult> GetAllUsers()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/GetAllUsers");
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (apiResponse == "Error")
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            List<Infrastructure.DTO.UserDTO> lst = new List<Infrastructure.DTO.UserDTO>();
            lst = JsonConvert.DeserializeObject<List<Infrastructure.DTO.UserDTO>>(apiResponse);
            return View(lst);
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/DeleteUser?UserId=" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            if (apiResponse == "Error")
            {
                return RedirectToAction("ErrorPage", "Home");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return View("Ok");
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> GetAllSectionByDepId(string DepId)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/GetAllSectionsByDepartmentId?DepartmentId=" + DepId);
            var apiResponse = await response.Content.ReadAsStringAsync();
            List<Infrastructure.DTO.SectionDTO> lst = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.SectionDTO>>(apiResponse);
            return Json(lst);
        }
        public async Task<IActionResult> Profile()
        {
            int userId=Convert.ToInt32(ViewBag.UserId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/GetProfile?UserId=" + userId);
            var apiResponse = await response.Content.ReadAsStringAsync();
            
            Infrastructure.DTO.UserDTO userDTO = JsonConvert.DeserializeObject<Infrastructure.DTO.UserDTO>(apiResponse);
            
            return View(userDTO);
        }
    }
}
