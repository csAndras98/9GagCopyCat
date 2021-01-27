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
    public class ProductModel : PageModel
    {
        private readonly ApplicationDbService DbService;
        public Fighter Fighter { get; private set; }
        public SignInManager<AppUser> SignInManager;
        public ProductModel(ApplicationDbService dbService, SignInManager<AppUser> signInManager)
        {
            DbService = dbService;
            SignInManager = signInManager;
        }

        public void OnGet(int id)
        {
            Fighter = DbService.GetFighter(id);
        }

        public RedirectToPageResult OnPostBuyMe(int id)
        {
            AppUser user = DbService.GetUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DbService.BuyFighter(new Fighter(), user);
            return RedirectToPage("./Index");
        }
    }
}
