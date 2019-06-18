using EExpress.Interface;
using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Repository
{
    public class EFBrandRepository : IBrand
    {
        private Context context;

        public EFBrandRepository(Context _context)
        {
            context = _context;
        }

        public IQueryable<Brand> Brands => context.Brands;

        public bool AddBrand(Brand brand)
        {
            context.Brands.Add(brand);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBrand(int Id)
        {
            var brand = GetById(Id);
            context.Brands.Remove(brand);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Brand GetById(int Id)
        {
            return context.Brands.Where(x => x.BrandId == Id).FirstOrDefault();
        }

        public bool UpdateBrand(Brand brand)
        {
            var brandOld = GetById(brand.BrandId);
            brandOld.Name = brand.Name;
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
