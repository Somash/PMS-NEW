namespace PMS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Checklist
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int VisitReportId { get; set; }

        public virtual VisitReport VisitReport { get; set; }
    }
}
