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
        private readonly ApplicationDbContext _context;
        public ApplicationDbProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                .Where(p => p.Amount > 0)
                .OrderBy(p => p.Name);
        }

        public Product GetProduct(int id)
        {
            return _context.Products
                .First(p => p.Id == id);
        }

        public IEnumerable<Review> GetReviews(int id)
        {
            return _context.Reviews
                .Where(p => p.ProductId == id)
                .OrderBy(p => p.Customer.UserName);
        }
    }
}
