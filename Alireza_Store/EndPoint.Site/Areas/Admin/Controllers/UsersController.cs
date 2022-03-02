using Alireza_Store.Application.Interfaces.FacadPattern;
using Alireza_Store.Application.Services.Users.Commands.AddUsers;
using Alireza_Store.Application.Services.Users.Commands.EditUsers;
using Alireza_Store.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersFacad _usersFacad;

        public UsersController(IUsersFacad usersFacad)
        {
            _usersFacad = usersFacad;
        }
        [HttpGet]
        public IActionResult Show(string searchKey)
        {
            ViewBag.Roles = new SelectList(_usersFacad.getRolesService.Execute(), "Id", "Name");
            var result = _usersFacad.getUsersService.Execute(new GetUsersRequestDto { SearchKey = searchKey});
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_usersFacad.getRolesService.Execute(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name, string lastname, string email,long RoleId ,string Password, string RePassword)
        {
            var result = _usersFacad.addUsersService.Execute(new AddUsersRequestDto
            {
                Email = email,
                Name = name,
                LastName = lastname,
                Password = Password,
                RePassword = RePassword,
                RoleId = RoleId
            });
            return Json(result);
        }
        [HttpPost]
        public IActionResult Edit(long userId, string name, string lastname, string email, long roleId, string newpass, string oldpass , bool checkbox)
        {
            var result = _usersFacad.editUsersService.Execute(userId, new EditUsersRequestDto { Name = name, LastName = lastname, Email = email, RoleId = roleId , NewPassword = newpass , OldPassword = oldpass ,IsCheckedPass = checkbox});
            return Json(result);
        }
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var result = _usersFacad.deleteUserSerivce.Execute(id);
            return Json(result);
        }
        [HttpPost]
        public IActionResult IsActive(long id)
        {
            var result = _usersFacad.isActiveService.Execute(id);
            return Json(result);
        }
    }
}
