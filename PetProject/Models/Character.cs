using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Initiative { get; set; }
    }
}
