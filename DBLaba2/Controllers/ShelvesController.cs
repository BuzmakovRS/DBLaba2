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

namespace DBLaba2.Controllers
{
    public class ShelvesController : Controller
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: Shelves
        public async Task<ActionResult> Index()
        {
            return View(await StoredProcedures.StorProcedures.GetShelvesAsync());
            //return View(await db.Shelfs.ToListAsync());
        }

        // GET: Shelves/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf shelf = await db.Shelfs.FindAsync(id);
            if (shelf == null)
            {
                return HttpNotFound();
            }
            return View(shelf);
        }

        // GET: Shelves/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shelves/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Position")] Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                db.Shelfs.Add(shelf);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shelf);
        }

        // GET: Shelves/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf shelf = await db.Shelfs.FindAsync(id);
            if (shelf == null)
            {
                return HttpNotFound();
            }
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Position")] Shelf shelf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shelf).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shelf shelf = await db.Shelfs.FindAsync(id);
            if (shelf == null)
            {
                return HttpNotFound();
            }
            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Shelf shelf = await db.Shelfs.FindAsync(id);
            db.Shelfs.Remove(shelf);
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
