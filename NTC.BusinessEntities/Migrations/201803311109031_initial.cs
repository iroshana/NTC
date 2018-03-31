namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LicenceNo = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Complains",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ComplainNo = c.String(),
                        BusId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        Place = c.String(),
                        Time = c.String(),
                        Method = c.String(),
                        ComplainCode = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        EvidenceId = c.Int(),
                        DriverId = c.Int(),
                        ConductorId = c.Int(),
                        IsEvidenceHave = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Conductors", t => t.ConductorId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Evidences", t => t.EvidenceId)
                .Index(t => t.BusId)
                .Index(t => t.RouteId)
                .Index(t => t.UserId)
                .Index(t => t.EvidenceId)
                .Index(t => t.DriverId)
                .Index(t => t.ConductorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryNo = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Conductors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TrainingCertificateNo = c.String(),
                        HighestEducation = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        IssuedDate = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.DeMerits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        DriverId = c.Int(),
                        ConductorId = c.Int(),
                        MeritId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        InqueryDate = c.DateTime(nullable: false),
                        OfficeriId = c.Int(nullable: false),
                        BusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Conductors", t => t.ConductorId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Merits", t => t.MeritId, cascadeDelete: true)
                .ForeignKey("dbo.Officers", t => t.OfficeriId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.DriverId)
                .Index(t => t.ConductorId)
                .Index(t => t.MeritId)
                .Index(t => t.RouteId)
                .Index(t => t.OfficeriId)
                .Index(t => t.BusId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TrainingCertificateNo = c.String(),
                        LicenceNo = c.String(),
                        TrainingCenter = c.String(),
                        HighestEducation = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        IssuedDate = c.String(),
                        ExpireDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        DOB = c.DateTime(nullable: false),
                        PrivateAddress = c.String(maxLength: 1000),
                        CUrrentAddress = c.String(maxLength: 1000),
                        TelNo = c.String(),
                        NIC = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkerNotices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(),
                        ConductorId = c.Int(),
                        NoticeId = c.Int(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                        IsOpened = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Conductors", t => t.ConductorId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Notices", t => t.NoticeId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.ConductorId)
                .Index(t => t.NoticeId);
            
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoticeCode = c.String(),
                        Content = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Merits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Officers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TelNo = c.String(),
                        NIC = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        RouteNo = c.String(),
                        Route_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Routes", t => t.Route_ID)
                .Index(t => t.Route_ID);
            
            CreateTable(
                "dbo.Evidences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EvidenceNo = c.String(),
                        FileName = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CategoryComplains",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Complain_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Complain_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Complains", t => t.Complain_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Complain_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complains", "EvidenceId", "dbo.Evidences");
            DropForeignKey("dbo.Complains", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Complains", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.Conductors", "UserID", "dbo.Users");
            DropForeignKey("dbo.DeMerits", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "Route_ID", "dbo.Routes");
            DropForeignKey("dbo.Complains", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.DeMerits", "OfficeriId", "dbo.Officers");
            DropForeignKey("dbo.DeMerits", "MeritId", "dbo.Merits");
            DropForeignKey("dbo.WorkerNotices", "NoticeId", "dbo.Notices");
            DropForeignKey("dbo.WorkerNotices", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.WorkerNotices", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.Drivers", "UserID", "dbo.Users");
            DropForeignKey("dbo.DeMerits", "UserID", "dbo.Users");
            DropForeignKey("dbo.Complains", "UserId", "dbo.Users");
            DropForeignKey("dbo.DeMerits", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.DeMerits", "ConductorId", "dbo.Conductors");
            DropForeignKey("dbo.DeMerits", "BusId", "dbo.Buses");
            DropForeignKey("dbo.CategoryComplains", "Complain_ID", "dbo.Complains");
            DropForeignKey("dbo.CategoryComplains", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Complains", "BusId", "dbo.Buses");
            DropIndex("dbo.CategoryComplains", new[] { "Complain_ID" });
            DropIndex("dbo.CategoryComplains", new[] { "Category_ID" });
            DropIndex("dbo.Routes", new[] { "Route_ID" });
            DropIndex("dbo.WorkerNotices", new[] { "NoticeId" });
            DropIndex("dbo.WorkerNotices", new[] { "ConductorId" });
            DropIndex("dbo.WorkerNotices", new[] { "DriverId" });
            DropIndex("dbo.Drivers", new[] { "UserID" });
            DropIndex("dbo.DeMerits", new[] { "BusId" });
            DropIndex("dbo.DeMerits", new[] { "OfficeriId" });
            DropIndex("dbo.DeMerits", new[] { "RouteId" });
            DropIndex("dbo.DeMerits", new[] { "MeritId" });
            DropIndex("dbo.DeMerits", new[] { "ConductorId" });
            DropIndex("dbo.DeMerits", new[] { "DriverId" });
            DropIndex("dbo.DeMerits", new[] { "UserID" });
            DropIndex("dbo.Conductors", new[] { "UserID" });
            DropIndex("dbo.Complains", new[] { "ConductorId" });
            DropIndex("dbo.Complains", new[] { "DriverId" });
            DropIndex("dbo.Complains", new[] { "EvidenceId" });
            DropIndex("dbo.Complains", new[] { "UserId" });
            DropIndex("dbo.Complains", new[] { "RouteId" });
            DropIndex("dbo.Complains", new[] { "BusId" });
            DropTable("dbo.CategoryComplains");
            DropTable("dbo.Evidences");
            DropTable("dbo.Routes");
            DropTable("dbo.Officers");
            DropTable("dbo.Merits");
            DropTable("dbo.Notices");
            DropTable("dbo.WorkerNotices");
            DropTable("dbo.Users");
            DropTable("dbo.Drivers");
            DropTable("dbo.DeMerits");
            DropTable("dbo.Conductors");
            DropTable("dbo.Categories");
            DropTable("dbo.Complains");
            DropTable("dbo.Buses");
        }
    }
}
