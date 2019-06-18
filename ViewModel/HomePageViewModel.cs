using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.ViewModel
{
    public class HomePageViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Basket> Baskets { get; set; }
    }
}
