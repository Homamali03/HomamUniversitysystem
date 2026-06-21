using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.Base
{
    public class BaseController : Controller
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsUserLoggedIn())
            {
                string Id = HttpContext.User.Claims.Where(obj => obj.Type == "UserID").FirstOrDefault().Value;
                ViewBag.UserId = Convert.ToInt32(Id);
                ViewBag.FullName = "Test";// GetFullName(Convert.ToInt32(Id)).Result;
                
            }
            else
            {
                context.Result = RedirectToAction("Login", "Account");
            }

        }
        public bool IsUserLoggedIn()
        {
            var result = HttpContext.User.Claims.Where(obj => obj.Type == "UserID").FirstOrDefault();
            if (result == null)
                return false;
            else
                return true;
        }
        public async Task<string> GetFullName(int UserId)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/User/GetFullNameById?UserId=" + UserId);
            var apiResponse = await response.Content.ReadAsStringAsync();
            string name = JsonConvert.DeserializeObject<string>(apiResponse);
            return name;
        }
        public async Task<IActionResult> GetAllPermissionByUserId()
        {
            int id = 0;
            if (ViewBag.UserId != null)
                id =Convert.ToInt32(ViewBag.UserId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5121/api/user/GetAllPermissionByUserId?UserId=" + id);
            Infrastructure.DTO.MenuPermissionDTO menuPermissionDTO = new Infrastructure.DTO.MenuPermissionDTO();
            var Data = await response.Content.ReadAsStringAsync();
            menuPermissionDTO = JsonConvert.DeserializeObject<Infrastructure.DTO.MenuPermissionDTO>(Data);
            return PartialView("_ManageMenu", menuPermissionDTO);
        }
    }
}
