using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public int Amount { get; set; }
        public int? Rating { get; set; }
    }
}
