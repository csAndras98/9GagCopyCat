using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int Price { get; set; }
        [Range(1, 5)]
        public int Level { get; set; }
        [Range(0, 100)]
        public int Morale { get; set; }
        [Range(1, 10)]
        public int Stealth { get; set; }
        [Range(1, 10)]
        public int Power { get; set; }
        public int? Rating { get; set; }
    }
}
