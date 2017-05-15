using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBLaba2.Models;
using DBLaba2.StoredProcedures;

namespace DBLaba2.Controllers
{
    public class LocationsController : Controller
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: Locations
        public async Task<ActionResult> Index()
        {
            var products = await StorProcedures.GetProductsAsync();
            var producers = await StorProcedures.GetProducersAsync();
            var shelves = await StorProcedures.GetShelvesAsync();
            var locations = await StorProcedures.GetLocationsAsync();
            foreach (Location l in locations)
            {
                l.Product = products.Where(p => p.Id == l.ProductId).FirstOrDefault();
                l.Producer = producers.Where(p => p.Id == l.ProducerId).FirstOrDefault();
                l.Shelf = shelves.Where(p => p.Id == l.ShelfId).FirstOrDefault();
            }
            return View(locations);
            // var locations = db.Locations.Include(l => l.Producer).Include(l => l.Product).Include(l => l.Shelf);
            //return View(await locations.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var locations = await db.Locations.Include(l => l.Producer).Include(l => l.Product).Include(l => l.Shelf).ToListAsync();
            Location location = locations.FirstOrDefault(l=>l.Id==id);
            
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.ShelfId = new SelectList(db.Shelfs, "Id", "Name");
            return View();
        }

        // POST: Locations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductId,ProducerId,ShelfId,Count")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", location.ProducerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", location.ProductId);
            ViewBag.ShelfId = new SelectList(db.Shelfs, "Id", "Name", location.ShelfId);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", location.ProducerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", location.ProductId);
            ViewBag.ShelfId = new SelectList(db.Shelfs, "Id", "Name", location.ShelfId);
            return View(location);
        }

        // POST: Locations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductId,ProducerId,ShelfId,Count")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerId = new SelectList(db.Producers, "Id", "Name", location.ProducerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", location.ProductId);
            ViewBag.ShelfId = new SelectList(db.Shelfs, "Id", "Name", location.ShelfId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var locations = await db.Locations.Include(l => l.Producer).Include(l => l.Product).Include(l => l.Shelf).ToListAsync();
            Location location = locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Location location = await db.Locations.FindAsync(id);
            db.Locations.Remove(location);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
