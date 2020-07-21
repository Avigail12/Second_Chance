using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IDataRepository
    {
        void Add<T>(T entity) where T : class;

       // Task<Product> AddProduct(Product product);
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);

        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<ProductState>> GetProductStates();

        Task<IEnumerable<Category>> GetCategories();

        Task<Like> GetLike(int userId, int prodId);
    }
}
