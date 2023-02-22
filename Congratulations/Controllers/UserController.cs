using Congratulations.Domain.Entity;
using Congratulations.Domain.ViewModels;
using Congratulations.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Linq;

namespace Congratulations.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return View("Error", $"{response.Description}");
        }


        [HttpPut]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
                if (model.Id == 0)
                {
                    await _userService.CreateUser(model, imageData);
                }
                else
                {
                    await _userService.EditUser(model.Id, model);
                }
            }

            return RedirectToAction("GetUsers");
        }

    }
}
