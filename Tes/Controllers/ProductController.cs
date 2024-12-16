using Microsoft.AspNetCore.Mvc;
using Tes.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tes.Controllers
{
    public class ProductController : Controller
    {
        // Simulated in-memory product list
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500.00m },
            new Product { Id = 2, Name = "Phone", Price = 800.00m },
            new Product { Id = 3, Name = "Tablet", Price = 500.00m }
        };

        // List products
        public IActionResult Index()
        {
            return View(products);
        }

        // View a single product
        public IActionResult View(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // Add a new product (GET)
        public IActionResult Add()
        {
            // Pass a new instance of Product to the view
            return View(new Product());
        }


        // Add a new product (POST)
        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.Id = products.Max(p => p.Id) + 1; // Auto-increment ID
                products.Add(newProduct);
                return RedirectToAction("Index");
            }
            return View(newProduct);
        }

        // Edit a product (GET)
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // Edit a product (POST)
        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        // Delete a product (GET confirmation)
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // Delete a product (POST)
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
