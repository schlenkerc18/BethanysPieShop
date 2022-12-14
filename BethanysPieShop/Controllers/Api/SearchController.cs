using BethanysPieShop.Data;
using BethanysPieShop.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase // controller base class is for controllers that don't contain views
    {
        private readonly IPieRepository pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allPies = pieRepository.AllPies;

            return Ok(allPies);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!pieRepository.AllPies.Any(p => p.PieId == id))
                return NotFound();

            return Ok(pieRepository.AllPies.Where(p => p.PieId == id));
        }

        [HttpPost]
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = pieRepository.SearchPies(searchQuery);
            }

            return new JsonResult(pies);
        }
    }
}
