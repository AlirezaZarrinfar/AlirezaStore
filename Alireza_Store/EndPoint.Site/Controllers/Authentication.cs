using Alireza_Store.Application.Interfaces.Contexts;
using Alireza_Store.Application.Interfaces.FacadPattern;
using Alireza_Store.Application.Services.Users.Commands.AddUsers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class Authentication : Controller
    {
        private readonly IDatabaseContext _context;
        private readonly IUsersFacad _usersFacad;
        public Authentication(IDatabaseContext context, IUsersFacad usersFacad)
        {
            _usersFacad = usersFacad;
            _context = context;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string name,string lastname , string email,string password , string repassword)
        {
            var result = _usersFacad.addUsersService.Execute(new AddUsersRequestDto
            {
                Name = name,
                LastName = lastname,
                Email = email,
                Password = password,
                RePassword = repassword,
                RoleId = 3
            });
            if (result.IsSuccess)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , result.UserId.ToString()),
                    new Claim(ClaimTypes.Email , email),
                    new Claim(ClaimTypes.Name , name),
                    new Claim(ClaimTypes.Surname , lastname),
                    new Claim(ClaimTypes.Role, "Customer"),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(result);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult SignIn(string Email, string Pass)
        {
            var result = _usersFacad.userLoginService;
            var exresult = result.Execute(Email, Pass);
            if (exresult.IsSuccess)
            {
                HttpContext.SignInAsync(result.principal(), result.properties());
            }
            return Json(exresult);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

    }
}
