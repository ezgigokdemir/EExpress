using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EExpress.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EExpress.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel r)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = r.Username;
                user.Email = r.Email;

                var result = await userManager.CreateAsync(user, r.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(r);
        }
    }
}