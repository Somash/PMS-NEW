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

        [Required]
        public string PlanUrl { get; set; }

        [Required]
        public string SectionsUrl { get; set; }

        [Required]
        public string ElevationsUrl { get; set; }

        [Required]
        public string TDImageUrl { get; set; }

        [Required]
        public string AreaPanelCalculationUrl { get; set; }

        [Required]
        public string ConceptsDrawingUrl { get; set; }

        [Required]
        public string OptimizationUrl { get; set; }

        [Required]
        public string ShopDrawingUrl { get; set; }

        [Required]
        public string AnalysisUrl { get; set; }

        [Required]
        public string BOQUrl { get; set; }

        [Required]
        public string InteriorUrl { get; set; }

        public int OwnerId { get; set; }

        public int ProjectTypeId { get; set; }

        public int FixingTypeId { get; set; }

        public int ApplicationsId { get; set; }

        [Required]
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
