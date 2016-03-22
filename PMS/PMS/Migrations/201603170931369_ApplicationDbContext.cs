namespace PMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        CommencedOn = c.DateTime(nullable: false),
                        ConcludedOn = c.DateTime(nullable: false),
                        ArchitectId = c.Int(nullable: false),
                        BusinessPartnerId = c.Int(nullable: false),
                        PlanUrl = c.String(),
                        SectionsUrl = c.String(),
                        ElevationsUrl = c.String(),
                        TDImageUrl = c.String(),
                        AreaPanelCalculationUrl = c.String(),
                        ConceptsDrawingUrl = c.String(),
                        OptimizationUrl = c.String(),
                        ShopDrawingUrl = c.String(),
                        AnalysisUrl = c.String(),
                        BOQUrl = c.String(),
                        InteriorUrl = c.String(),
                        OwnerId = c.Int(nullable: false),
                        ProjectTypeId = c.Int(nullable: false),
                        FixingTypeId = c.Int(nullable: false),
                        ApplicationsId = c.Int(nullable: false),
                        TDRenderImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Architects", t => t.ArchitectId)
                .ForeignKey("dbo.BusinessPartners", t => t.BusinessPartnerId)
                .ForeignKey("dbo.FixingTypes", t => t.FixingTypeId)
                .ForeignKey("dbo.Owners", t => t.OwnerId)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeId)
                .ForeignKey("dbo.Applications", t => t.ApplicationsId)
                .Index(t => t.ArchitectId)
                .Index(t => t.BusinessPartnerId)
                .Index(t => t.OwnerId)
                .Index(t => t.ProjectTypeId)
                .Index(t => t.FixingTypeId)
                .Index(t => t.ApplicationsId);
            
            CreateTable(
                "dbo.Architects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        City = c.String(),
                        Address = c.String(),
                        FirmName = c.String(),
                        ContactNo = c.String(),
                        EmailId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AutocadDiagrams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        NewProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewProjects", t => t.NewProjectId)
                .Index(t => t.NewProjectId);
            
            CreateTable(
                "dbo.BusinessPartners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address = c.String(),
                        OwnerName = c.String(nullable: false),
                        ContactNo = c.String(),
                        Emailid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FixingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitedBy = c.String(nullable: false),
                        VisitedOn = c.String(nullable: false),
                        ActionPlanReportUrl = c.String(nullable: false),
                        NewProjectId = c.Int(nullable: false),
                        ProjectStatusId = c.Int(nullable: false),
                        ProjectHealthId = c.Int(nullable: false),
                        comments = c.String(),
                        notes = c.String(),
                        ProjectPictures = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectHealths", t => t.ProjectHealthId)
                .ForeignKey("dbo.ProjectStatus", t => t.ProjectStatusId)
                .ForeignKey("dbo.NewProjects", t => t.NewProjectId)
                .Index(t => t.NewProjectId)
                .Index(t => t.ProjectStatusId)
                .Index(t => t.ProjectHealthId);
            
            CreateTable(
                "dbo.Checklists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        VisitReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitReports", t => t.VisitReportId)
                .Index(t => t.VisitReportId);
            
            CreateTable(
                "dbo.ProjectHealths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(nullable: false),
                        VisitReportId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitReports", t => t.VisitReportId)
                .Index(t => t.VisitReportId);
            
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewProjects", "ApplicationsId", "dbo.Applications");
            DropForeignKey("dbo.VisitReports", "NewProjectId", "dbo.NewProjects");
            DropForeignKey("dbo.VisitReports", "ProjectStatusId", "dbo.ProjectStatus");
            DropForeignKey("dbo.ProjectPictures", "VisitReportId", "dbo.VisitReports");
            DropForeignKey("dbo.VisitReports", "ProjectHealthId", "dbo.ProjectHealths");
            DropForeignKey("dbo.Checklists", "VisitReportId", "dbo.VisitReports");
            DropForeignKey("dbo.NewProjects", "ProjectTypeId", "dbo.ProjectTypes");
            DropForeignKey("dbo.NewProjects", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.NewProjects", "FixingTypeId", "dbo.FixingTypes");
            DropForeignKey("dbo.NewProjects", "BusinessPartnerId", "dbo.BusinessPartners");
            DropForeignKey("dbo.AutocadDiagrams", "NewProjectId", "dbo.NewProjects");
            DropForeignKey("dbo.NewProjects", "ArchitectId", "dbo.Architects");
            DropIndex("dbo.ProjectPictures", new[] { "VisitReportId" });
            DropIndex("dbo.Checklists", new[] { "VisitReportId" });
            DropIndex("dbo.VisitReports", new[] { "ProjectHealthId" });
            DropIndex("dbo.VisitReports", new[] { "ProjectStatusId" });
            DropIndex("dbo.VisitReports", new[] { "NewProjectId" });
            DropIndex("dbo.AutocadDiagrams", new[] { "NewProjectId" });
            DropIndex("dbo.NewProjects", new[] { "ApplicationsId" });
            DropIndex("dbo.NewProjects", new[] { "FixingTypeId" });
            DropIndex("dbo.NewProjects", new[] { "ProjectTypeId" });
            DropIndex("dbo.NewProjects", new[] { "OwnerId" });
            DropIndex("dbo.NewProjects", new[] { "BusinessPartnerId" });
            DropIndex("dbo.NewProjects", new[] { "ArchitectId" });
            DropTable("dbo.ProjectStatus");
            DropTable("dbo.ProjectPictures");
            DropTable("dbo.ProjectHealths");
            DropTable("dbo.Checklists");
            DropTable("dbo.VisitReports");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.Owners");
            DropTable("dbo.FixingTypes");
            DropTable("dbo.BusinessPartners");
            DropTable("dbo.AutocadDiagrams");
            DropTable("dbo.Architects");
            DropTable("dbo.NewProjects");
            DropTable("dbo.Applications");
        }
    }
}
