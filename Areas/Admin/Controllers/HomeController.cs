using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EExpress.Interface;
using EExpress.Models;
using EExpress.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EExpress.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private IProduct repo;
        private IBrand repoBrand;
        private ICategory repoCategory;
        private readonly UserManager<ApplicationUser> userManager;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public HomeController(IProduct _repo, IBrand _repoBrand, ICategory _repoCategory, UserManager<ApplicationUser> _userManager, IPasswordValidator<ApplicationUser> _passwordValidator,
                    IPasswordHasher<ApplicationUser> _passwordHasher)
        {
            repo = _repo;
            repoBrand = _repoBrand;
            repoCategory = _repoCategory;
            userManager = _userManager;
            passwordValidator = _passwordValidator;
            passwordHasher = _passwordHasher;
        }


        public IActionResult Index(string SearchString)
        {
            HomePageViewModel model = new HomePageViewModel();
            IEnumerable<Product> products = repo.Products.ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString)).ToList();
            }
            model.Products = products;
            model.Brands = repoBrand.Brands.ToList();
            model.Categories = repoCategory.Categories.ToList();
            return View(model);
        }

        public IActionResult Users()
        {
            return View(userManager.Users);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterModel r)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = r.Username;
                user.Email = r.Email;

                var result = await userManager.CreateAsync(user, r.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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

       
        public async Task<IActionResult> DeleteUser(string Id)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(Id);
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View("Users");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string Id, string Password, string Email)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.Email = Email;
                IdentityResult validPassword = null;
                if (!string.IsNullOrEmpty(Password))
                {
                    validPassword = await passwordValidator.ValidateAsync(userManager, user, Password);
                    if (validPassword.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, Password);
                    }
                }
                else
                {
                    foreach (var item in validPassword.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                if (validPassword.Succeeded)
                {
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "user Not Fount");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.AddProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            var product = repo.GetById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = repo.GetById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int Id)
        {
            repo.DeleteProduct(Id);
            return RedirectToAction("Index");
        }

        public IActionResult List(string SearchString)
        {
            HomePageViewModel model = new HomePageViewModel();
            IEnumerable<Product> products = repo.Products.ToList();
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString)).ToList();
            }
            model.Products = products;
            return View(model);
        }

        public IActionResult Statistic()
        {
            HomePageViewModel model = new HomePageViewModel();
            model.Products = repo.Products;
            model.Brands = repoBrand.Brands.ToList();
            model.Categories = repoCategory.Categories.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        {
            if (LoginUser(loginModel.Username, loginModel.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        private bool LoginUser(string username, string password)
        {
            if (username == "ezgi" && password == "123")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}