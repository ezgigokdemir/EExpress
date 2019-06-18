using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Interface
{
    public interface IBrand
    {
        IQueryable<Brand> Brands { get; }
        Brand GetById(int Id);
        bool AddBrand(Brand brand);
        bool DeleteBrand(int Id);
        bool UpdateBrand(Brand brand);
    }
}
