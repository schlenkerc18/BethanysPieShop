using BethanysPieShop.Data;
using BethanysPieShop.Domain.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            //ViewBag.CurrentCategory = "Cheese cakes";
            //return View(pieRepository.AllPies);
            PieListViewModel piesListViewModel = new PieListViewModel(pieRepository.AllPies, "All Pies");
            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = pieRepository.GetPieById(id);

            if (pie == null)
            {
                return NotFound();
            }

            return View(pie);
        }
    }
}
