using BethanysPieShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories => bethanysPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
