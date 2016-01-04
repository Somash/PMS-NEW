namespace PMS.Models.DbModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Architect> Architects { get; set; }
        public virtual DbSet<AutocadDiagram> AutocadDiagrams { get; set; }
        public virtual DbSet<BusinessPartner> BusinessPartners { get; set; }
        public virtual DbSet<Checklist> Checklists { get; set; }
        public virtual DbSet<FixingType> FixingTypes { get; set; }
        public virtual DbSet<NewProject> NewProjects { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<ProjectHealth> ProjectHealths { get; set; }
        public virtual DbSet<ProjectPicture> ProjectPictures { get; set; }
        public virtual DbSet<ProjectStatu> ProjectStatus { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<VisitReport> VisitReports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.Application)
                .HasForeignKey(e => e.ApplicationsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Architect>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.Architect)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BusinessPartner>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.BusinessPartner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FixingType>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.FixingType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NewProject>()
                .HasMany(e => e.AutocadDiagrams)
                .WithRequired(e => e.NewProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NewProject>()
                .HasMany(e => e.VisitReports)
                .WithRequired(e => e.NewProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Owner>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.Owner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectHealth>()
                .HasMany(e => e.VisitReports)
                .WithRequired(e => e.ProjectHealth)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectStatu>()
                .HasMany(e => e.VisitReports)
                .WithRequired(e => e.ProjectStatu)
                .HasForeignKey(e => e.ProjectStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectType>()
                .HasMany(e => e.NewProjects)
                .WithRequired(e => e.ProjectType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisitReport>()
                .HasMany(e => e.Checklists)
                .WithRequired(e => e.VisitReport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisitReport>()
                .HasMany(e => e.ProjectPictures1)
                .WithRequired(e => e.VisitReport)
                .WillCascadeOnDelete(false);
        }
    }
}
