using Microsoft.AspNetCore.Identity;
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
        private UserManager<Customer> _userManager;
        public ApplicationDbProductService(ApplicationDbContext context, UserManager<Customer> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public IEnumerable<Product> GetMyProducts(Customer customer)
        {
            List<Product> result = new List<Product>();
            foreach(Order order in _context.Orders)
            {
                foreach(ProductOrder productOrder in _context.ProductOrders)
                {
                    if (order.Customer.Equals(customer) && productOrder.OrderId.Equals(order.Id))
                    {
                        result.Add(productOrder.Product);
                    }
                }
            }
            return result;
        }

        public IEnumerable<Review> GetReviews(int id)
        {
            return _context.Reviews
                .Where(p => p.ProductId == id)
                .OrderBy(p => p.Customer.UserName);
        }

        public Customer GetCustomer(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }
    }
}
