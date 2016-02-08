namespace PMS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Authorize]
    public partial class ProjectPicture
    {
        public int Id { get; set; }

        [Required]
        public string Picture { get; set; }

        public int VisitReportId { get; set; }

        public virtual VisitReport VisitReport { get; set; }
    }
}
