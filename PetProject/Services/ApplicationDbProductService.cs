﻿using Microsoft.AspNetCore.Identity;
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
                        result.Add(_context.Products.FirstOrDefault(p => p.Id == productOrder.ProductId));
                    }
                }
            }
            return result;
        }

        internal void Buy(Customer customer, Product product)
        {
            if(customer.Funds >= product.Amount)
            {
                customer.Funds -= product.Amount;

                Order order = new Order()
                {
                    Customer = customer,
                    Date = DateTime.Now,
                    ProductOrders = new List<ProductOrder>()
                };
                ProductOrder productOrder = new ProductOrder()
                {
                    Order = order,
                    Product = product
                };
                order.ProductOrders.Add(productOrder);

                _context.Add(order);
                _context.Add(productOrder);
            }
            _context.SaveChanges();
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
