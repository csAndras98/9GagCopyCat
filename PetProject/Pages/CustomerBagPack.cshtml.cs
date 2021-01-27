using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetProject.Models;
using PetProject.Services;

namespace PetProject.Pages
{
    public class CustomerBagPackModel : PageModel
    {
        public AppUser AppUser { get; private set; }
        public IEnumerable<Fighter> Fighters { get; set; }
        public ApplicationDbService DbService { get; }

        public CustomerBagPackModel(ApplicationDbService dbService)
        {
            DbService = dbService;
        }
        public void OnGet(string id)
        {
            AppUser = DbService.GetUser(id);
            Fighters = DbService.GetMyFighters(id);
        }
    }
}
