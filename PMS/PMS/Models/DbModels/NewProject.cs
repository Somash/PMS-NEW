namespace PMS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        public string ProjectName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CommencedOn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ConcludedOn { get; set; }

        public int ArchitectId { get; set; }

        public int BusinessPartnerId { get; set; }

 
        public string PlanUrl { get; set; }

    
        public string SectionsUrl { get; set; }

 
        public string ElevationsUrl { get; set; }

 
        public string TDImageUrl { get; set; }

 
        public string AreaPanelCalculationUrl { get; set; }


        public string ConceptsDrawingUrl { get; set; }

        public string OptimizationUrl { get; set; }

        public string ShopDrawingUrl { get; set; }

 
        public string AnalysisUrl { get; set; }

    
        public string BOQUrl { get; set; }

    
        public string InteriorUrl { get; set; }

        public int OwnerId { get; set; }

        public int ProjectTypeId { get; set; }

        public int FixingTypeId { get; set; }

        public int ApplicationsId { get; set; }

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
    }
}
