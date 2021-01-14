using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Models
{
    public class Customer : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Funds { get; set; }
    }
}
