using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Interface
{
    public interface ICategory
    {
        IQueryable<Category> Categories { get; }
        Category GetById(int Id);
        bool AddCategory(Category category);
        bool DeleteCategory(int Id);
        bool UpdateCategory(Category category);
    }
}
