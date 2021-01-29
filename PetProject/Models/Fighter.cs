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
        public int Level { get; set; }
        public int Morale { get; set; }
        public int Health { get; set; }
        public int Accuracy { get; set; }
        public int Power { get; set; }
        public int Initiative { get; set; }
        public bool inParty { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
