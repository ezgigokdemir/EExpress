using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Interface
{
    public interface IBasket
    {
        IQueryable<Basket> Baskets { get; }
        Basket GetById(int Id);
        bool AddBasket(Product product,string userId);
        bool DeleteBasket(int Id);
        bool UpdateBasket(Basket basket);
    }
}
