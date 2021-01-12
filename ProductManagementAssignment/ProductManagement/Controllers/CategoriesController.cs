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

/*--------------------------------   WebAPI Controller for Querying Categories ------------------------------------*/

namespace ProductManagement.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ProductDbContext db = new ProductDbContext();
        private static readonly Logger logger = LogManager.GetLogger("APILogger");

        // GET: api/Categories
        public List<Categories> GetCategories()
        {
            return db.Categories.ToList<Categories>();
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Categories))]
        public IHttpActionResult GetCategories(int id)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                logger.Error("Trying to access non existing category.");
                return NotFound();
            }

            return Ok(categories);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategories(int id, Categories categories)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid request to upadate a category.");
                return BadRequest(ModelState);
            }

            if (id != categories.Id)
            {
                logger.Error("Invalid request to update a category.");
                return BadRequest();
            }

            db.Entry(categories).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                logger.Error("Error while updating category in the database.");
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Categories))]
        public IHttpActionResult PostCategories(Categories categories)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid request to add a new category");
                return BadRequest(ModelState);
            }

            db.Categories.Add(categories);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categories.Id }, categories);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Categories))]
        public IHttpActionResult DeleteCategories(int id)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                logger.Error("Invalid request to delete a category.");
                return NotFound();
            }

            db.Categories.Remove(categories);
            db.SaveChanges();

            return Ok(categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriesExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}