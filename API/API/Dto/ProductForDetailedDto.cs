using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class ProductForDetailedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        //?
        public string ProductState { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
