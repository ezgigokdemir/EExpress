using EExpress.Interface;
using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Repository
{
    public class EFProductRepository : IProduct
    {
        private Context context;

        public EFProductRepository(Context _context)
        {
            context = _context;
        }

        public IQueryable<Product> Products => context.Products;

        public bool AddProduct(Product product)
        {
            context.Products.Add(product);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(int Id)
        {
            var product = GetById(Id);
            context.Products.Remove(product);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Product GetById(int Id)
        {
            return context.Products.Where(x => x.Id == Id).FirstOrDefault();
        }

        public bool UpdateProduct(Product product)
        {
            var productOld = GetById(product.Id);
            productOld.Name = product.Name;
            productOld.Description = product.Description;
            productOld.Price = product.Price;
            productOld.imageUrl = product.imageUrl;
           
            if (context.SaveChanges() > 0)
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
