using EExpress.Interface;
using EExpress.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Repository
{
    public class EFCategoryRepository : ICategory
    {
        private Context context;

        public EFCategoryRepository(Context _context)
        {
            context = _context;
        }

        public IQueryable<Category> Categories => context.Categories;

        public bool AddCategory(Category category)
        {
            context.Categories.Add(category);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteCategory(int Id)
        {
            var category = GetById(Id);
            context.Categories.Remove(category);
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Category GetById(int Id)
        {
            return context.Categories.Where(x => x.CategoryId == Id).FirstOrDefault();
        }

        public bool UpdateCategory(Category category)
        {
            var categoryOld = GetById(category.CategoryId);
            categoryOld.Name = category.Name;
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
