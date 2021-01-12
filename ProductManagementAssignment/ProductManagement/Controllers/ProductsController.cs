using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductManagement.Context;
using ProductManagement.Models;

/*--------------------------------   WebAPI Controller for Querying Product Information   ---------------------------------*/

namespace ProductManagement.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ProductDbContext db = new ProductDbContext();
        private readonly static Logger logger = LogManager.GetLogger("APILogger");

        // GET: api/Products
        [HttpGet]
        public IQueryable<Products> GetProducts()
        { 
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpGet]
        public IHttpActionResult GetProducts(int id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                logger.Error("Trying to access non existing product.");
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutProducts(int id, Products products)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid request for updating a product");
                return BadRequest(ModelState);
            }

            if (id != products.Id)
            {
                logger.Error("Invalid requesy for updating a product");
                return BadRequest();
            }

            db.Entry(products).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error("Error while updating product in database");
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Products))]
        [HttpPost]
        public IHttpActionResult PostProducts(Products products)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid request to add a product");
                return BadRequest(ModelState);
            }

            db.Products.Add(products);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = products.Id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete]
        [ResponseType(typeof(Products))]
        public IHttpActionResult DeleteProducts(int id)
        {
            Products products = db.Products.Find(id);
            if (products == null)
            {
                logger.Error("Invalid request to delete a product");
                return NotFound();
            }

            db.Products.Remove(products);
            db.SaveChanges();

            return Ok(products);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}