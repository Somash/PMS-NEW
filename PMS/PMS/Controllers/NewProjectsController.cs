﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PMS.Models.DbModels;
using PMS.Helpers;


namespace PMS.Controllers
{
    public class NewProjectsController : Controller
    {
        private Entities db = new Entities();

        // GET: NewProjects
        public ActionResult Index(string searchProject, String searchCity)        
        {

            var CityLst = new List<string>();

            var CityQry = from d in db.NewProjects
                           orderby d.City
                           select d.City;

            CityLst.AddRange(CityQry.Distinct());           
            
            ViewBag.searchCity = new SelectList(CityLst);            

            //var newProjects = db.NewProjects.Include(n => n.Application).Include(n => n.Architect).Include(n => n.BusinessPartner).Include(n => n.FixingType).Include(n => n.Owner).Include(n => n.ProjectType);

            var searchString = from m in db.NewProjects
                                      select m;

            if (!String.IsNullOrEmpty(searchProject))
            {
                searchString = searchString.Where(s => s.ProjectName.Contains(searchProject));                
            }

            if (!string.IsNullOrEmpty(searchCity))
            {
                searchString = searchString.Where(x => x.city == searchCity);
            }

            return View(searchString);
        }

        // GET: NewProjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = await db.NewProjects.FindAsync(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            return View(newProject);
        }

        // GET: NewProjects/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationsId = new SelectList(db.Applications, "Id", "Name");
            ViewBag.ArchitectId = new SelectList(db.Architects, "Id", "FullName");
            ViewBag.BusinessPartnerId = new SelectList(db.BusinessPartners, "Id", "FullName");
            ViewBag.FixingTypeId = new SelectList(db.FixingTypes, "Id", "Name");
            ViewBag.OwnerId = new SelectList(db.Owners, "Id", "FullName");
            ViewBag.ProjectTypeId = new SelectList(db.ProjectTypes, "Id", "Name");
            return View();
        }

        // POST: NewProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProjectName,City,Street,CommencedOn,ConcludedOn,ArchitectId,BusinessPartnerId,PlanUrl,SectionsUrl,ElevationsUrl,TDImageUrl,AreaPanelCalculationUrl,ConceptsDrawingUrl,OptimizationUrl,ShopDrawingUrl,AnalysisUrl,BOQUrl,InteriorUrl,OwnerId,ProjectTypeId,FixingTypeId,ApplicationsId,TDRenderImageUrl")] NewProject newProject)
        {
            if (ModelState.IsValid)
            {
                newProject.AnalysisUrl = Utilities.saveFile(Request.Files["AnalysisUrl"], newProject.ProjectName + "\\Analysis");
                newProject.AreaPanelCalculationUrl = Utilities.saveFile(Request.Files["AreaPanelCalculationUrl"], newProject.ProjectName + "\\AreaPanelCalculation");
                newProject.BOQUrl = Utilities.saveFile(Request.Files["BOQUrl"], newProject.ProjectName + "\\BOQ");
                newProject.ConceptsDrawingUrl = Utilities.saveFile(Request.Files["ConceptsDrawingUrl"], newProject.ProjectName + "\\ConceptsDrawing");
                newProject.ElevationsUrl = Utilities.saveFile(Request.Files["ElevationsUrl"], newProject.ProjectName + "\\Elevations");
                newProject.InteriorUrl = Utilities.saveFile(Request.Files["InteriorUrl"], newProject.ProjectName + "\\Interior");
                newProject.OptimizationUrl = Utilities.saveFile(Request.Files["OptimizationUrl"], newProject.ProjectName + "\\Optimization");
                newProject.PlanUrl = Utilities.saveFile(Request.Files["PlanUrl"], newProject.ProjectName + "\\Plan");
                newProject.SectionsUrl = Utilities.saveFile(Request.Files["SectionsUrl"], newProject.ProjectName + "\\Sections");
                newProject.ShopDrawingUrl = Utilities.saveFile(Request.Files["ShopDrawingUrl"], newProject.ProjectName + "\\ShopDrawing");
                newProject.TDImageUrl = Utilities.saveFile(Request.Files["TDImageUrl"], newProject.ProjectName + "\\TDImage");
                newProject.TDRenderImageUrl = Utilities.saveFile(Request.Files["TDRenderImageUrl"], newProject.ProjectName + "\\TDRenderImage");
                db.NewProjects.Add(newProject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationsId = new SelectList(db.Applications, "Id", "Name", newProject.ApplicationsId);
            ViewBag.ArchitectId = new SelectList(db.Architects, "Id", "FullName", newProject.ArchitectId);
            ViewBag.BusinessPartnerId = new SelectList(db.BusinessPartners, "Id", "FullName", newProject.BusinessPartnerId);
            ViewBag.FixingTypeId = new SelectList(db.FixingTypes, "Id", "Name", newProject.FixingTypeId);
            ViewBag.OwnerId = new SelectList(db.Owners, "Id", "FullName", newProject.OwnerId);
            ViewBag.ProjectTypeId = new SelectList(db.ProjectTypes, "Id", "Name", newProject.ProjectTypeId);
            return View(newProject);
        }

        // GET: NewProjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = await db.NewProjects.FindAsync(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationsId = new SelectList(db.Applications, "Id", "Name", newProject.ApplicationsId);
            ViewBag.ArchitectId = new SelectList(db.Architects, "Id", "FullName", newProject.ArchitectId);
            ViewBag.BusinessPartnerId = new SelectList(db.BusinessPartners, "Id", "FullName", newProject.BusinessPartnerId);
            ViewBag.FixingTypeId = new SelectList(db.FixingTypes, "Id", "Name", newProject.FixingTypeId);
            ViewBag.OwnerId = new SelectList(db.Owners, "Id", "FullName", newProject.OwnerId);
            ViewBag.ProjectTypeId = new SelectList(db.ProjectTypes, "Id", "Name", newProject.ProjectTypeId);
            return View(newProject);
        }

        // POST: NewProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProjectName,City,Street,CommencedOn,ConcludedOn,ArchitectId,BusinessPartnerId,PlanUrl,SectionsUrl,ElevationsUrl,TDImageUrl,AreaPanelCalculationUrl,ConceptsDrawingUrl,OptimizationUrl,ShopDrawingUrl,AnalysisUrl,BOQUrl,InteriorUrl,OwnerId,ProjectTypeId,FixingTypeId,ApplicationsId,TDRenderImageUrl")] NewProject newProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newProject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationsId = new SelectList(db.Applications, "Id", "Name", newProject.ApplicationsId);
            ViewBag.ArchitectId = new SelectList(db.Architects, "Id", "FullName", newProject.ArchitectId);
            ViewBag.BusinessPartnerId = new SelectList(db.BusinessPartners, "Id", "FullName", newProject.BusinessPartnerId);
            ViewBag.FixingTypeId = new SelectList(db.FixingTypes, "Id", "Name", newProject.FixingTypeId);
            ViewBag.OwnerId = new SelectList(db.Owners, "Id", "FullName", newProject.OwnerId);
            ViewBag.ProjectTypeId = new SelectList(db.ProjectTypes, "Id", "Name", newProject.ProjectTypeId);
            return View(newProject);
        }

        // GET: NewProjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = await db.NewProjects.FindAsync(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            return View(newProject);
        }

        // POST: NewProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NewProject newProject = await db.NewProjects.FindAsync(id);
            db.NewProjects.Remove(newProject);
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
