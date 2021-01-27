using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class AppUser : IdentityUser
    {
        public int DungeonRank { get; set; }
        public int Best { get; set; }
        public int Funds { get; set; }
    }
}
