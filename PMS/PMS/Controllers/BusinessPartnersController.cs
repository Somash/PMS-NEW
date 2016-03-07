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
    public class BusinessPartnersController : Controller
    {
        private Entities db = new Entities();

        // GET: BusinessPartners
        public async Task<ActionResult> Index()
        {
            return View(await db.BusinessPartners.ToListAsync());
        }

        // GET: BusinessPartners/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = await db.BusinessPartners.FindAsync(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // GET: BusinessPartners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessPartners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FullName,City,Address,OwnerName,ContactNo,Emailid")] BusinessPartner businessPartner)
        {
            if (ModelState.IsValid)
            {
                db.BusinessPartners.Add(businessPartner);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(businessPartner);
        }

        // GET: BusinessPartners/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = await db.BusinessPartners.FindAsync(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // POST: BusinessPartners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FullName,City,Address,OwnerName,ContactNo,Emailid")] BusinessPartner businessPartner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessPartner).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(businessPartner);
        }

        // GET: BusinessPartners/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = await db.BusinessPartners.FindAsync(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // POST: BusinessPartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BusinessPartner businessPartner = await db.BusinessPartners.FindAsync(id);
            db.BusinessPartners.Remove(businessPartner);
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
