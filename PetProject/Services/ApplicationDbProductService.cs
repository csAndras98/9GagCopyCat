using PetProject.Data;
using PetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Services
{
    public class ApplicationDbProductService
    {
        public IEnumerable<Product> GetAllProducts(ApplicationDbContext context)
        {
            return context.Products
                .Where(p => p.Amount > 0)
                .OrderBy(p => p.Name);
        }

        public Product GetProduct(ApplicationDbContext context, int id)
        {
            return context.Products
                .First(p => p.Id == id);
        }

        public IEnumerable<Review> GetReviews(ApplicationDbContext context, int id)
        {
            return context.Reviews
                .Where(p => p.ProductId == id)
                .OrderBy(p => p.Customer.UserName);
        }
    }
}
