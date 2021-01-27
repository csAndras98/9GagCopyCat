using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PetProject.Models;
using PetProject.Services;

namespace PetProject.Pages
{
    public class IndexModel : PageModel
    {
        public SignInManager<AppUser> SignInManager;
        public ApplicationDbService DbService;
        public IEnumerable<Fighter> Fighters { get; private set; }

        public IndexModel(SignInManager<AppUser> signInManager, ApplicationDbService dbService)
        {
            SignInManager = signInManager;
            DbService = dbService;
        }

        public void OnGet()
        {
            Fighters = DbService.RandomFighters(); 
        }

        public void OnPostRoll()
        {
            Fighters = DbService.RandomFighters();
        }

        public void OnPostBuy(Fighter fighter)
        {
            DbService.BuyFighter(fighter, DbService.GetUser(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}