using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class Fighter
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        [Range(1, 5)]
        public int Level { get; set; }
        [Range(0, 100)]
        public int Morale { get; set; }
        [Range(0, 100)]
        public int Health { get; set; }
        [Range(1, 10)]
        public int Accuracy { get; set; }
        [Range(1, 10)]
        public int Power { get; set; }
        [Range(1, 10)]
        public int Initiative { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
