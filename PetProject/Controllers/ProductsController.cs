using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProject.Data;
using PetProject.Models;

namespace PetProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("owned/{id}")]
        public ActionResult<IEnumerable<Product>> GetMyProducts(string id)
        {
            Customer customer = _context.Users.Find(id);
            List<Product> result = new List<Product>();
            foreach (Order order in _context.Orders)
            {
                foreach (ProductOrder productOrder in _context.ProductOrders)
                {
                    if (order.Customer.Equals(customer) && productOrder.OrderId.Equals(order.Id))
                    {
                        result.Add(_context.Products.FirstOrDefault(p => p.Id == productOrder.ProductId));
                    }
                }
            }

            return result;
        }

        [HttpPost("{userId}/{id}")]
        public ActionResult Buy(string userId, int id)
        {
            Customer customer = _context.Users.Find(userId);
            Product product = _context.Products.Find(id);

            if (customer.Funds >= product.Amount)
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

            return Ok();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
