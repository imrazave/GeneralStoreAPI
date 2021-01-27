using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly GeneralStoreDBContext _context = new GeneralStoreDBContext();
        // POST
        // api/Product
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody]Product model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty");
            }
            // If the model is valid
            if (ModelState.IsValid)
            {
                // Store the model in the database
                _context.Products.Add(model);
                int changeCount = await _context.SaveChangesAsync();

                return Ok("Your customer was created!");
            }

            // The model is not valid, go ahead and reject it
            return BadRequest(ModelState);
        } 

        // Get All Products(GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // Get a Product by its ID(GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Product product = await _context.Products.FindAsync(id);

            if(product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        // Update an existing Product by its ID(PUT)
        [HttpPut]

        public async Task<IHttpActionResult> UpdateProduct([FromUri] string sku, [FromBody] Product updatedProduct)
        {
            // Check the SKUs if they match
            if (sku != updatedProduct?.SKU)
            {
                return BadRequest("SKUs do not match.");
            }

            // Check the ModelState
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the the product in the database
            Product product = await _context.Products.FindAsync(sku);

            // If the product does not exist then do something
            if (product is null)
                return NotFound();

            // Update the properties of the product
            product.SKU = updatedProduct.SKU;
            product.Name = updatedProduct.Name;
            product.NumberInInventory = updatedProduct.NumberInInventory;
            product.Cost = updatedProduct.Cost;

            // Save the changes
            await _context.SaveChangesAsync();

            return Ok("The restaurant was updated!");
        }

        // Delete an existing Product by its ID(DELETE)
        
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] string sku)
        {
            Product product = await _context.Products.FindAsync(sku);

            if (product is null)
                return NotFound();

            _context.Products.Remove(product);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The customer was deleted.");
            }

            return InternalServerError();
        }
    }
}
