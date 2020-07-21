using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        //public async Task<Product> AddProduct(Product product)
        //{
        //     await _context.Products.AddAsync(product);
        //    return product;
        //}

        public void Delete<T>(T entity) where T : class
        {
            
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            var cities =await _context.Cities.ToListAsync();
            return cities;
        }

        public async Task<Like> GetLike(int userId, int prodId)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.Product.Id == prodId);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Photos).FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Photos).ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductState>> GetProductStates()
        {
            return await _context.ProductsState.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        
    }
}
