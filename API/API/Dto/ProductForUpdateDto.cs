using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class ProductForUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
