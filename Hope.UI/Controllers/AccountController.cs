using Hope.Infrastructure.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Hope.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginUser(Infrastructure.DTO.LoginDTO loginDTO)
        {
            var Item = CheckLoginUser(loginDTO).Result;
            if (Item == -1)
            {
                ViewBag.ErrorMesssage = "Worng Username or password!";
                return View("Login");
            }
            else
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,loginDTO.UserName),
                    new Claim("UserName","Admin"),
                    new Claim("UserID",Item.ToString())

                };
                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                HttpContext.SignInAsync(userPrincipal);
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<int> CheckLoginUser(Infrastructure.DTO.LoginDTO loginDTO)
        {
            Security obj= new Security();
            loginDTO.Password = obj.EncryptString(loginDTO.Password);
            HttpClient client = new HttpClient();
            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);
            var response = await client.PostAsync("http://localhost:5121/api/user/Login",
                new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));
            var Data = await response.Content.ReadAsStringAsync();
            int id = JsonConvert.DeserializeObject<int>(Data);
            //call API
            return id;
        }
        public async Task<IActionResult> LogOut()
        {
            var _user = HttpContext.User as ClaimsPrincipal;
            var _identity = _user.Identity as ClaimsIdentity;
            foreach (var claim in _user.Claims.ToList())
            {
                _identity.RemoveClaim(claim);
            }
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
