using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Interface
{
    public interface IProduct
    {
        IQueryable<Product> Products { get; }
        Product GetById(int Id);
        bool AddProduct(Product product);
        bool DeleteProduct(int Id);
        bool UpdateProduct(Product product);
    }
}
