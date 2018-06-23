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
        private readonly CategoryRepository _ctgRepo;

        public CategoryController(GamesService gameSvc, CategoryRepository ctgRepo)
        {
            _gameSvc = gameSvc;
            _ctgRepo = ctgRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _ctgRepo.AllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{categories}", Name = nameof(GetByCategories))]
        public async Task<IActionResult> GetByCategories(string categories, PagingOptions pageOptions)
        {
            var categoriesList = categories.Split(",").Select(x => x.Trim()).ToList();
            var games = await _gameSvc.GetByCategoriesAsync(categoriesList, pageOptions);
            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody]string name)
        {
            var newCategory = new Category(name);
            await _ctgRepo.Add(newCategory);
            return CreatedAtRoute("GetByCategories", name);
        }
    }
}