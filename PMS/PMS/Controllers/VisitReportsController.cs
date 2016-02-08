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
    [Authorize]
    public class VisitReportsController : Controller
    {
        private Entities db = new Entities();

        // GET: VisitReports
        public async Task<ActionResult> Index()
        {
            var visitReports = db.VisitReports.Include(v => v.NewProject).Include(v => v.ProjectHealth).Include(v => v.ProjectStatu);
            return View(await visitReports.ToListAsync());
        }

        // GET: VisitReports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitReport visitReport = await db.VisitReports.FindAsync(id);
            if (visitReport == null)
            {
                return HttpNotFound();
            }
            return View(visitReport);
        }

        // GET: VisitReports/Create
        public ActionResult Create()
        {
            ViewBag.NewProjectId = new SelectList(db.NewProjects, "Id", "ProjectName");
            ViewBag.ProjectHealthId = new SelectList(db.ProjectHealths, "Id", "Name");
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Name");
            return View();
        }

        // POST: VisitReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VisitedBy,VisitedOn,ActionPlanReportUrl,NewProjectId,ProjectStatusId,ProjectHealthId,comments,notes,ProjectPictures")] VisitReport visitReport)
        {
            if (ModelState.IsValid)
            {
                db.VisitReports.Add(visitReport);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NewProjectId = new SelectList(db.NewProjects, "Id", "ProjectName", visitReport.NewProjectId);
            ViewBag.ProjectHealthId = new SelectList(db.ProjectHealths, "Id", "Name", visitReport.ProjectHealthId);
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Name", visitReport.ProjectStatusId);
            return View(visitReport);
        }

        // GET: VisitReports/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitReport visitReport = await db.VisitReports.FindAsync(id);
            if (visitReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewProjectId = new SelectList(db.NewProjects, "Id", "ProjectName", visitReport.NewProjectId);
            ViewBag.ProjectHealthId = new SelectList(db.ProjectHealths, "Id", "Name", visitReport.ProjectHealthId);
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Name", visitReport.ProjectStatusId);
            return View(visitReport);
        }

        // POST: VisitReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VisitedBy,VisitedOn,ActionPlanReportUrl,NewProjectId,ProjectStatusId,ProjectHealthId,comments,notes,ProjectPictures")] VisitReport visitReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitReport).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NewProjectId = new SelectList(db.NewProjects, "Id", "ProjectName", visitReport.NewProjectId);
            ViewBag.ProjectHealthId = new SelectList(db.ProjectHealths, "Id", "Name", visitReport.ProjectHealthId);
            ViewBag.ProjectStatusId = new SelectList(db.ProjectStatus, "Id", "Name", visitReport.ProjectStatusId);
            return View(visitReport);
        }

        // GET: VisitReports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitReport visitReport = await db.VisitReports.FindAsync(id);
            if (visitReport == null)
            {
                return HttpNotFound();
            }
            return View(visitReport);
        }

        // POST: VisitReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VisitReport visitReport = await db.VisitReports.FindAsync(id);
            db.VisitReports.Remove(visitReport);
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
