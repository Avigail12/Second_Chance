using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
   // [Authorize]
    //[Route("api/users/{userid}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository _repo;

        public ProductsController(IMapper mapper, IDataRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {
            var productsFromRepo = await _repo.GetProducts();
            // add map
            var productsToReturn = _mapper.Map<IEnumerable<ProductForListDto>>(productsFromRepo);
            return Ok(productsToReturn);
        }

        [HttpGet("{id}", Name = "GetProduct")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var productFromRepo = await _repo.GetProduct(id);
            // add map
            var productToReturn = _mapper.Map<ProductForDetailedDto>(productFromRepo);
            return Ok(productToReturn);
        }
        

        [HttpPost("{userId}")]
        //[HttpPost]

        public async Task<IActionResult> AddProduct(int userId, Product product)
        {
           
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);
          


            product.UserId = userId;
            //product.User = userFromRepo;

            _repo.Add<Product>(product);
            //await _repo.AddProduct(product);
            userFromRepo.ProductsOfMine.Add(product);
            try
            {

            
            if (await _repo.SaveAll())
            {
                var productToReturn = _mapper.Map<ProductForDetailedDto>(product);
                    // return CreatedAtRoute("GetProduct", new {  id = product.Id }, productToReturn);
                    return Ok(product.Id);
            }
}
            catch(Exception ex)
            {
                int i=9;
            }
            return BadRequest("Could not add the Product");
        }

        [HttpPut("{userId}/{prodId}")]

        public async Task<IActionResult> UpdateProduct(int userId, int prodId, ProductForUpdateDto productForUpdateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var productFromRepo = await _repo.GetProduct(prodId);

            _mapper.Map(productForUpdateDto, productFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating product { prodId } failed on save");
        }

        [HttpPost("{userId}/like/{prodId}")]
        public async Task<IActionResult> LikeProduct(int userId, int prodId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);

            var productFromRepo = await _repo.GetProduct(prodId);

            var like = await _repo.GetLike(userId, prodId);

            if (like != null)
                return BadRequest("המוצר כבר נמצא במוצרים המועדפים");

            if (await _repo.GetProduct(prodId) == null)
                return NotFound();

            like = new Like
            {
                UserId = userId,
                Product = productFromRepo
            };

            _repo.Add<Like>(like);

            userFromRepo.ProductsLike.Add(like);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("הוספת המוצר למוצרים מועדפים נכשלה");
        }

    }
}