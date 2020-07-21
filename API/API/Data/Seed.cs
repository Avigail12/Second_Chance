using API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static void SeedCategory(DataContext context)
        {
            if(!context.Categories.Any())
            {
                var catgoryData = System.IO.File.ReadAllText("Data/SeedData/category.json");

                var Categories = JsonConvert.DeserializeObject<List<Category>>(catgoryData);

                foreach (var Category in Categories)
                {
                    context.Categories.Add(Category);
                }
                context.SaveChanges();
            }
        }
    }
}
