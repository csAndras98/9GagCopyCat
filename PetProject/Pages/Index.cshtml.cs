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
        private readonly ILogger<IndexModel> _logger;
        public ApplicationDbProductService ProductService;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        public void OnGet()
        {
            Products = ProductService.GetAllProducts();
        }

    }
}
