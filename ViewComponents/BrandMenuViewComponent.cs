using EExpress.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.ViewComponents
{
    public class BrandMenuViewComponent : ViewComponent
    {
        private IBrand repo;

        public BrandMenuViewComponent(IBrand _repo)
        {
            repo = _repo;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString() == "Index")
            {
                ViewBag.SelectedCategory = RouteData?.Values["data"];
            }

            return View(repo.Brands);
        }
    }
}
