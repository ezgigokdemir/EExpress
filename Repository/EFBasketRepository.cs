using EExpress.Interface;
using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Repository
{
    public class EFBasketRepository : IBasket
    {
        private Context context;
        private ApplicationIdentityContext appContext;

        public EFBasketRepository(Context _context, ApplicationIdentityContext _appContext)
        {
            context = _context;
            appContext = _appContext;
        }

        public IQueryable<Basket> Baskets => context.Basket;

        public bool AddBasket(Product product,string userId)
        {
            Basket basket = new Basket();
            basket.ProductId = product.Id;
            basket.ProductState = false;
            basket.UserId = userId;
            context.Basket.Add(basket);

            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBasket(int Id)
        {
            var basket = GetById(Id);
            context.Basket.Remove(basket);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Basket GetById(int Id)
        {
            return context.Basket.Where(x => x.Id == Id).FirstOrDefault();
        }

        public bool UpdateBasket(Basket basket)
        {
            var basketOld = GetById(basket.Id);
            basketOld.ProductId = basket.ProductId;
            basketOld.ProductState = basket.ProductState;
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
