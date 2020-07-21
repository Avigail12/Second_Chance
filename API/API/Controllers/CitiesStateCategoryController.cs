using API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesStateCategoryController: ControllerBase
    {
       
        private readonly IDataRepository _repo;

        public CitiesStateCategoryController(IDataRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _repo.GetCities();

            return Ok(cities);
        }
        
        [HttpGet("productState")]
        
        public async Task<IActionResult> GetproductStates()
        {
            var productStates = await _repo.GetProductStates();

            return Ok(productStates);
        }

        [HttpGet("categories")]

        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.GetCategories();

            return Ok(categories);
        }
    }
}
