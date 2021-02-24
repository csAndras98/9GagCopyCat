using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class Fighter : Character
    {
        public int Price { get; set; }
        public int MaxHealth { get; set; }
        public bool InParty { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
