namespace PMS.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AutocadDiagram
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public int NewProjectId { get; set; }

        public virtual NewProject NewProject { get; set; }
    }
}
