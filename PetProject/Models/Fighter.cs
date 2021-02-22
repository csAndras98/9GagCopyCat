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
        public int Morale { get; set; }
        public bool inParty { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
