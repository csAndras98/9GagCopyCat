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
        private readonly ApplicationDbProductService ProductService;
        public Product Product { get; private set; }
        public SignInManager<Customer> SignInManager;
        public IEnumerable<Review> Reviews { get; private set; }
        public ProductModel(ApplicationDbProductService productService, SignInManager<Customer> signInManager)
        {
            ProductService = productService;
            SignInManager = signInManager;
        }

        public void OnGet(int id)
        {
            Product = ProductService.GetProduct(id);
            Reviews = ProductService.GetReviews(id);
        }
        public RedirectToPageResult OnPostBuyMe(int id)
        {
            ProductService.Buy(ProductService.GetCustomer(User.FindFirst(ClaimTypes.NameIdentifier).Value), ProductService.GetProduct(id));
            return RedirectToPage("./Index");
        }
    }
}
