using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMS.Models.DbModels;

namespace PMS.Controllers
{
    public class ArchitectsController : Controller
    {
        private Entities db = new Entities();

        // GET: Architects
        public async Task<ActionResult> Index()
        {
            return View(await db.Architects.ToListAsync());
        }

        // GET: Architects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Architect architect = await db.Architects.FindAsync(id);
            if (architect == null)
            {
                return HttpNotFound();
            }
            return View(architect);
        }

        // GET: Architects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Architects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,City,Address,FirmName,ContactNo,EmailId")] Architect architect)
        {
            if (ModelState.IsValid)
            {
                db.Architects.Add(architect);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(architect);
        }

        // GET: Architects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Architect architect = await db.Architects.FindAsync(id);
            if (architect == null)
            {
                return HttpNotFound();
            }
            return View(architect);
        }

        // POST: Architects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,City,Address,FirmName,ContactNo,EmailId")] Architect architect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(architect).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(architect);
        }

        // GET: Architects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Architect architect = await db.Architects.FindAsync(id);
            if (architect == null)
            {
                return HttpNotFound();
            }
            return View(architect);
        }

        // POST: Architects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Architect architect = await db.Architects.FindAsync(id);
            db.Architects.Remove(architect);
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
