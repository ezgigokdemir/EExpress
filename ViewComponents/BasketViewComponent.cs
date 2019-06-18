using EExpress.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.ViewComponents
{
    public class BasketViewComponent:ViewComponent
    {
        IBasket repo;
        IProduct repoProduct;
        static double payment = 0;
        static int count = 0;

        public BasketViewComponent(IBasket _repo, IProduct _repoProduct)
        {
            repo = _repo;
            repoProduct = _repoProduct;
        }

        public IViewComponentResult Invoke(int Id,string userId)
        {
            payment = 0;

            var product = repoProduct.GetById(Id);
            repo.AddBasket(product, userId);

            var baskets = repo.Baskets;
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
            return View(productBasket);
        }
    }
}
