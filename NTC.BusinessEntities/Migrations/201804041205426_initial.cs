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
                        DriverId = c.Int(nullable: false),
                        ConductorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.ConductorId, cascadeDelete: false)
                .ForeignKey("dbo.Members", t => t.DriverId, cascadeDelete: false)
                .Index(t => t.DriverId)
                .Index(t => t.ConductorId);
            
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
                        EmployeeId = c.Int(nullable: false),
                        IsEvidenceHave = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Evidences", t => t.EvidenceId)
                .Index(t => t.BusId)
                .Index(t => t.RouteId)
                .Index(t => t.UserId)
                .Index(t => t.EvidenceId)
                .Index(t => t.EmployeeId);
            
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
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        TypeId = c.Int(nullable: false),
                        TrainingCertificateNo = c.String(),
                        LicenceNo = c.String(),
                        TrainingCenter = c.String(),
                        HighestEducation = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MemberTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.DeMerits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        InqueryDate = c.DateTime(nullable: false),
                        OfficeriId = c.Int(nullable: false),
                        BusId = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Officers", t => t.OfficeriId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.EmployeeId)
                .Index(t => t.RouteId)
                .Index(t => t.OfficeriId)
                .Index(t => t.BusId)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.MemberDeMerits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeMeritId = c.Int(nullable: false),
                        MeritId = c.Int(nullable: false),
                        Point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DeMerits", t => t.DeMeritId, cascadeDelete: true)
                .ForeignKey("dbo.Merits", t => t.MeritId, cascadeDelete: true)
                .Index(t => t.DeMeritId)
                .Index(t => t.MeritId);
            
            CreateTable(
                "dbo.Merits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        ColorCodeId = c.Int(nullable: false),
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
                "dbo.MemberTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.LoginHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        LoggedInTime = c.DateTime(),
                        LoggedOutTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.MemberNotices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        NoticeId = c.Int(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                        IsOpened = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Members", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Notices", t => t.NoticeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
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
            DropForeignKey("dbo.Buses", "DriverId", "dbo.Members");
            DropForeignKey("dbo.Buses", "ConductorId", "dbo.Members");
            DropForeignKey("dbo.Complains", "EvidenceId", "dbo.Evidences");
            DropForeignKey("dbo.Complains", "EmployeeId", "dbo.Members");
            DropForeignKey("dbo.MemberNotices", "NoticeId", "dbo.Notices");
            DropForeignKey("dbo.MemberNotices", "EmployeeId", "dbo.Members");
            DropForeignKey("dbo.Members", "UserID", "dbo.Users");
            DropForeignKey("dbo.LoginHistories", "UserID", "dbo.Users");
            DropForeignKey("dbo.DeMerits", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Complains", "UserId", "dbo.Users");
            DropForeignKey("dbo.Members", "TypeId", "dbo.MemberTypes");
            DropForeignKey("dbo.DeMerits", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "Route_ID", "dbo.Routes");
            DropForeignKey("dbo.Complains", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.DeMerits", "OfficeriId", "dbo.Officers");
            DropForeignKey("dbo.MemberDeMerits", "MeritId", "dbo.Merits");
            DropForeignKey("dbo.MemberDeMerits", "DeMeritId", "dbo.DeMerits");
            DropForeignKey("dbo.DeMerits", "EmployeeId", "dbo.Members");
            DropForeignKey("dbo.DeMerits", "BusId", "dbo.Buses");
            DropForeignKey("dbo.CategoryComplains", "Complain_ID", "dbo.Complains");
            DropForeignKey("dbo.CategoryComplains", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Complains", "BusId", "dbo.Buses");
            DropIndex("dbo.CategoryComplains", new[] { "Complain_ID" });
            DropIndex("dbo.CategoryComplains", new[] { "Category_ID" });
            DropIndex("dbo.MemberNotices", new[] { "NoticeId" });
            DropIndex("dbo.MemberNotices", new[] { "EmployeeId" });
            DropIndex("dbo.LoginHistories", new[] { "UserID" });
            DropIndex("dbo.Routes", new[] { "Route_ID" });
            DropIndex("dbo.MemberDeMerits", new[] { "MeritId" });
            DropIndex("dbo.MemberDeMerits", new[] { "DeMeritId" });
            DropIndex("dbo.DeMerits", new[] { "User_ID" });
            DropIndex("dbo.DeMerits", new[] { "BusId" });
            DropIndex("dbo.DeMerits", new[] { "OfficeriId" });
            DropIndex("dbo.DeMerits", new[] { "RouteId" });
            DropIndex("dbo.DeMerits", new[] { "EmployeeId" });
            DropIndex("dbo.Members", new[] { "TypeId" });
            DropIndex("dbo.Members", new[] { "UserID" });
            DropIndex("dbo.Complains", new[] { "EmployeeId" });
            DropIndex("dbo.Complains", new[] { "EvidenceId" });
            DropIndex("dbo.Complains", new[] { "UserId" });
            DropIndex("dbo.Complains", new[] { "RouteId" });
            DropIndex("dbo.Complains", new[] { "BusId" });
            DropIndex("dbo.Buses", new[] { "ConductorId" });
            DropIndex("dbo.Buses", new[] { "DriverId" });
            DropTable("dbo.CategoryComplains");
            DropTable("dbo.Evidences");
            DropTable("dbo.Notices");
            DropTable("dbo.MemberNotices");
            DropTable("dbo.LoginHistories");
            DropTable("dbo.Users");
            DropTable("dbo.MemberTypes");
            DropTable("dbo.Routes");
            DropTable("dbo.Officers");
            DropTable("dbo.Merits");
            DropTable("dbo.MemberDeMerits");
            DropTable("dbo.DeMerits");
            DropTable("dbo.Members");
            DropTable("dbo.Categories");
            DropTable("dbo.Complains");
            DropTable("dbo.Buses");
        }
    }
}
