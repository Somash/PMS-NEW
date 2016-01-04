namespace PMS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VisitReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitReport()
        {
            Checklists = new HashSet<Checklist>();
            ProjectPictures1 = new HashSet<ProjectPicture>();
        }

        public int Id { get; set; }

        [Required]
        public string VisitedBy { get; set; }

        [Required]
        public string VisitedOn { get; set; }

        [Required]
        public string ActionPlanReportUrl { get; set; }

        public int NewProjectId { get; set; }

        public int ProjectStatusId { get; set; }

        public int ProjectHealthId { get; set; }

        public string comments { get; set; }

        public string notes { get; set; }

        public string ProjectPictures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Checklist> Checklists { get; set; }

        public virtual NewProject NewProject { get; set; }

        public virtual ProjectHealth ProjectHealth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectPicture> ProjectPictures1 { get; set; }

        public virtual ProjectStatu ProjectStatu { get; set; }
    }
}
