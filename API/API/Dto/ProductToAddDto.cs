using API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto
{
    public class ProductToAddDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
      //  [Required]
        public ProductState ProductState { get; set; }
       // [Required]
        public Category Category { get; set; }
      //  public ICollection<PhotoForCreationDto> PhotosForCreationDto { get; set; }
        public DateTime DateAdded { get; set; }

        public ProductToAddDto()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}
