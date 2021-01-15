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
        public Customer Customer { get; private set; }
        public IEnumerable<Product> Products { get; set; }
        public ApplicationDbProductService ProductService { get; }

        public CustomerBagPackModel(ApplicationDbProductService productService)
        {
            ProductService = productService;
        }
        public void OnGet(string id)
        {
            Customer = ProductService.GetCustomer(id);
            Products = ProductService.GetMyProducts(Customer);
        }
    }
}
