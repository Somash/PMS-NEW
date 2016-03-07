using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace PMS.Models.DbModels
{
    public partial class NewProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NewProject()
        {
            AutocadDiagrams = new HashSet<AutocadDiagram>();
            VisitReports = new HashSet<VisitReport>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Street")]

        public string Street { get; set; }

        [Required]
        [Display(Name = "CommencedOn")]
        [DataType(DataType.Date)]
        public DateTime CommencedOn { get; set; }

        [Required]
        [Display(Name = "ConcludedOn")]
        [DataType(DataType.Date)]
        public DateTime ConcludedOn { get; set; }

        [Display(Name = "Architect")]
        public int ArchitectId { get; set; }

        [Display(Name = "FBP")]
        public int BusinessPartnerId { get; set; }

        [Display(Name = "Plan")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string PlanUrl { get; set; }

        [Display(Name = "Sections")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string SectionsUrl { get; set; }

        [Display(Name = "Elevations")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string ElevationsUrl { get; set; }

        [Display(Name = "3DImage")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string TDImageUrl { get; set; }

        [Display(Name = "AreaPanel Calc.")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string AreaPanelCalculationUrl { get; set; }

        [Display(Name = "Concepts Drawings")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string ConceptsDrawingUrl { get; set; }

        [Display(Name = "Optimization")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string OptimizationUrl { get; set; }

        [Display(Name = "ShopDrawing")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string ShopDrawingUrl { get; set; }

        [Display(Name = "Analysis")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string AnalysisUrl { get; set; }

        [Display(Name = "BOQ")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string BOQUrl { get; set; }

        [Display(Name = "Interior")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string InteriorUrl { get; set; }

        [Required]
        [Display(Name = "Owners")]        
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }

        [Required]
        [Display(Name = "Fixing Type")]
        public int FixingTypeId { get; set; }

        [Display(Name = "Application Name")]
        public int ApplicationsId { get; set; }

        [Display(Name = "3DRenderImage")]
        [DisplayFormat(NullDisplayText = "Not Available")]
        public string TDRenderImageUrl { get; set; }

        public virtual Application Application { get; set; }

        public virtual Architect Architect { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AutocadDiagram> AutocadDiagrams { get; set; }

        public virtual BusinessPartner BusinessPartner { get; set; }

        public virtual FixingType FixingType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitReport> VisitReports { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        //public string city { get; set; }
    }

    //public class NewProjectDBContext : DbContext
    //{
    //    public DbSet<NewProject> NewProject { get; set; }

    //}

}
