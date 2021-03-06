﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using PMS.Models.DbModels;
using PMS.Helpers;
using Rotativa;
using Rotativa.Options;
using System.IO;
using PagedList;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace PMS.Controllers
{
    [Authorize]
    public class NewProjectsController : Controller
    {
        private Entities db = new Entities();

        // GET: NewProjects

        public ViewResult Index(string sortOrder, string searchProject, String searchCity, int PageNumber = 1, int PageSize = 15)
        {
            var CityLst = new List<string>();
            var CityQry = from d in db.NewProjects
                          orderby d.City
                          select d.City;

            CityLst.AddRange(CityQry.Distinct());
            ViewBag.searchCity = new SelectList(CityLst);
            var searchString = db.NewProjects.OrderByDescending(r => r.Id).Skip((PageNumber - 1) * PageSize).Take(PageSize).AsQueryable();
            if (!String.IsNullOrEmpty(searchProject))
            {
                searchString = searchString.Where(s => s.ProjectName.Contains(searchProject));
            }

            if (!string.IsNullOrEmpty(searchCity))
            {
                searchString = searchString.Where(x => x.City == searchCity);
            }     

            var ListProjects = from p in db.NewProjects
                               select p;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.OwnerSortParm = String.IsNullOrEmpty(sortOrder) ? "owner_desc" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewBag.EnquiryOnSortParm = String.IsNullOrEmpty(sortOrder) ? "EnquiryOn_desc" : "";
            ViewBag.BusinessPartnerParm = String.IsNullOrEmpty(sortOrder) ? "BusinessPartner_desc" : "";

            switch (sortOrder)
            { 
                case "name_desc":
                    searchString = searchString.OrderByDescending(s => s.ProjectName);
                    break;

                case "owner_desc":
                    searchString = searchString.OrderByDescending(s => s.Owner);
                    break;

                case "city_desc":
                    searchString = searchString.OrderByDescending(s => s.City);
                    break;

                case "EnquiryOn_desc":
                    searchString = searchString.OrderByDescending(s => s.CommencedOn);
                    break;

                case "BusinessPartner_desc":
                    searchString = searchString.OrderByDescending(s => s.BusinessPartner);
                    break;          
  
                 default:
                    searchString = searchString.OrderBy(s => s.ProjectName);
                    break;
            }           
            return View(searchString.ToPagedList(PageNumber, PageSize));
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
                newProject.AnalysisUrl = Utilities.isFile(Request.Files["AnalysisUrl"]) ? Utilities.saveFile(Request.Files["AnalysisUrl"], newProject.ProjectName + "\\Analysis") : newProject.AnalysisUrl;
                newProject.AreaPanelCalculationUrl = Utilities.isFile(Request.Files["AreaPanelCalculationUrl"]) ? Utilities.saveFile(Request.Files["AreaPanelCalculationUrl"], newProject.ProjectName + "\\AreaPanelCalculation") : newProject.AreaPanelCalculationUrl;
                newProject.BOQUrl = Utilities.isFile(Request.Files["BOQUrl"]) ? Utilities.saveFile(Request.Files["BOQUrl"], newProject.ProjectName + "\\BOQ") : newProject.BOQUrl;
                newProject.ConceptsDrawingUrl = Utilities.isFile(Request.Files["ConceptsDrawingUrl"]) ? Utilities.saveFile(Request.Files["ConceptsDrawingUrl"], newProject.ProjectName + "\\ConceptsDrawing") : newProject.ConceptsDrawingUrl;
                newProject.ElevationsUrl = Utilities.isFile(Request.Files["ElevationsUrl"]) ? Utilities.saveFile(Request.Files["ElevationsUrl"], newProject.ProjectName + "\\Elevations") : newProject.ElevationsUrl;
                newProject.InteriorUrl = Utilities.isFile(Request.Files["InteriorUrl"]) ? Utilities.saveFile(Request.Files["InteriorUrl"], newProject.ProjectName + "\\Interior") : newProject.InteriorUrl;
                newProject.OptimizationUrl = Utilities.isFile(Request.Files["OptimizationUrl"]) ? Utilities.saveFile(Request.Files["OptimizationUrl"], newProject.ProjectName + "\\Optimization") : newProject.OptimizationUrl;
                newProject.PlanUrl = Utilities.isFile(Request.Files["PlanUrl"]) ? Utilities.saveFile(Request.Files["PlanUrl"], newProject.ProjectName + "\\Plan") : newProject.PlanUrl;
                newProject.SectionsUrl = Utilities.isFile(Request.Files["SectionsUrl"]) ? Utilities.saveFile(Request.Files["SectionsUrl"], newProject.ProjectName + "\\Sections") : newProject.SectionsUrl;
                newProject.ShopDrawingUrl = Utilities.isFile(Request.Files["ShopDrawingUrl"]) ? Utilities.saveFile(Request.Files["ShopDrawingUrl"], newProject.ProjectName + "\\ShopDrawing") : newProject.ShopDrawingUrl;
                newProject.TDImageUrl = Utilities.isFile(Request.Files["TDImageUrl"]) ? Utilities.saveFile(Request.Files["TDImageUrl"], newProject.ProjectName + "\\TDImage") : newProject.TDImageUrl;
                newProject.TDRenderImageUrl = Utilities.isFile(Request.Files["TDRenderImageUrl"]) ? Utilities.saveFile(Request.Files["TDRenderImageUrl"], newProject.ProjectName + "\\TDRenderImage") : newProject.TDRenderImageUrl;
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

        public ActionResult Report()
        {
            Entities db = new Entities();
            var Model = db.NewProjects;
            ViewBag.logoPath = "~/Content/logo.png";
            return new ViewAsPdf(Model)
            {
                FileName = "AllProject.pdf",
                CustomSwitches = "--print-media-type --header-center \"Project Details\"",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageMargins = { Left = 10, Right = 10 }

            };
        }

        public async Task<ActionResult> ReportDetails(int? id)
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
            ViewBag.logoPath = "~/Content/logo.png";         
            return new ViewAsPdf(newProject)
            {
                FileName = "ProjectInfo.pdf",
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                //CustomSwitches = "--print-media-type --header-center \"Project Information\"",
                CustomSwitches = "--footer-center \"Name: " + "Project Information of: " + newProject.ProjectName + "  DOS: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" + " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };
        }

        public ActionResult ExporttoExcel()
        {
            GridView gv = new GridView();            
            //gv.DataSource = db.NewProjects.ToList();
            gv.DataSource = db.NewProjects.Select(p => new
            {
                Name = p.ProjectName,
                City = p.City,
                Area = p.Street,
                DateofEnquiry = p.CommencedOn,
                ProjectStatus = p.ProjectType.Name,
                Architect = p.Architect.FullName,
                FBP = p.BusinessPartner.FullName,
                Owner = p.Owner.FullName,
                FixingType = p.FixingType.Name,
                Application = p.Application.Name
            }).ToList();
            
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename= ProjectList.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");        
        }

               
        public ActionResult Download(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                string filename = Path.GetFileName(filepath);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            return null;
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
