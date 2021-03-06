﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductState ProductState { get; set; }
        public int ProductStateId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

       // public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
