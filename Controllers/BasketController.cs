using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EExpress.Interface;
using EExpress.Models;
using EExpress.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EExpress.Controllers
{
    public class BasketController : Controller
    {
        IBasket repo;
        IProduct repoProduct;
        static double payment = 0;
        static int count = 0;

        public BasketController(IBasket _repo, IProduct _repoProduct)
        {
            repo = _repo;
            repoProduct = _repoProduct;
        }

        public IActionResult Index(int Id)
        {
            HomePageViewModel model = new HomePageViewModel();
            payment = 0;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var product = repoProduct.GetById(Id);
            repo.AddBasket(product, userId);

            var baskets = repo.Baskets.Where(x=>x.UserId==userId && x.ProductState==false);
            var products = repoProduct.Products;

            var productBasket =
                        from x in products
                        join y in baskets on x.Id equals y.ProductId
                        select x;


            foreach (var item in productBasket)
            {
                payment += item.Price;
            }

            ViewData["payment"] = Math.Round(payment, 2).ToString();
            ViewData["countTotal"] = productBasket.Count();
            count = productBasket.Count();

            model.Products = productBasket;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index()
        {
            ViewData["payment"] = payment.ToString();
            return View(ViewData["payment"]);
        }
        
        public IActionResult Payment()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            var basketToSell = repo.Baskets.Where(x => x.UserId == userId && x.ProductState == false).ToList();
            foreach (var item in basketToSell)
            {
                item.ProductState = true;
                repo.UpdateBasket(item);
            }
            return View();
        }
    }
}