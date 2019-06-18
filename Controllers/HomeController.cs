using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EExpress.Interface;
using EExpress.Models;
using EExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EExpress.Controllers
{
    public class HomeController : Controller
    {
        private IProduct repo;

        public HomeController(IProduct _repo)
        {
            repo = _repo;
        }

        [Authorize]
        public IActionResult Index(int? Id, int? data)
        {
            ViewBag.UserName = this.User.Identity.Name;
            var products = repo.Products;
            if (Id != null)
            {
                products = products.Where(x => x.CategoryId == Id || x.BrandId == data);
            }
            return View(products);
        }
        
        public IActionResult About()
        {
            return View();
        }
    }
}