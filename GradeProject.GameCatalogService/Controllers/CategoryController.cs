using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Repos;
using GradeProject.GameCatalogService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly GamesService _gameSvc;
        private readonly CategoryService _categorygSvc;

        public CategoryController(GamesService gameSvc, CategoryService ctgRepo)
        {
            _gameSvc = gameSvc;
            _categorygSvc = ctgRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
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