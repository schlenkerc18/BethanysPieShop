using BethanysPieShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Data
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext bethanysPieShopDbContext;

        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            this.bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return bethanysPieShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return bethanysPieShopDbContext.Pies
                        .Include(c => c.Category)
                        .Where(p => p.IsPieOfTheWeek);
            }
        }


        public Pie? GetPieById(int pieId)
        {
            return bethanysPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return bethanysPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
