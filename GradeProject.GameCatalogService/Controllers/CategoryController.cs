using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICatalogService _gameSvc;
        private readonly ICategoryService _categorygSvc;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICatalogService gameSvc,
            ICategoryService ctgRepo,
            ILogger<CategoryController> logger)
        {
            _gameSvc = gameSvc;
            _categorygSvc = ctgRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogWarning("Test Elmahio bug");
            var categories = await _categorygSvc.AllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{categories}", Name = nameof(GetByCategories))]
        public async Task<IActionResult> GetByCategories(string categories, PagingOptions pageOptions)
        {
            var categoriesList = categories.Split(",").Select(x => x.Trim()).ToList();
            var gamesResponse = await _gameSvc.GetByCategoriesAsync(categoriesList, pageOptions);

            return Ok(gamesResponse);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]string name)
        {
            await _categorygSvc.AddAsync(name);
            return CreatedAtRoute(nameof(GetByCategories), new { categories = name }, name);
        }

        //[Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCategory(string name)
        {
            if (await _categorygSvc.DeleteCategory(name)) { return NoContent(); }
            else { return BadRequest("Error. Deletion wasnt completed. Check your data"); }
        }
    }
}