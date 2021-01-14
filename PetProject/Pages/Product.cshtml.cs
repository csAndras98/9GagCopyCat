using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<Review> Reviews { get; private set; }
        public ProductModel(ApplicationDbProductService productService)
        {
            ProductService = productService;
        }

        public void OnGet(int id)
        {
            Product = ProductService.GetProduct(id);
            Reviews = ProductService.GetReviews(id);
        }
    }
}
