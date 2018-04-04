namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbcorrections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "Route_ID", "dbo.Routes");
            DropForeignKey("dbo.DeMerits", "User_ID", "dbo.Users");
            DropIndex("dbo.DeMerits", new[] { "User_ID" });
            DropIndex("dbo.Routes", new[] { "Route_ID" });
            RenameColumn(table: "dbo.DeMerits", name: "EmployeeId", newName: "MemberId");
            RenameColumn(table: "dbo.MemberNotices", name: "EmployeeId", newName: "MemberId");
            RenameColumn(table: "dbo.Complains", name: "EmployeeId", newName: "MemberId");
            RenameIndex(table: "dbo.Complains", name: "IX_EmployeeId", newName: "IX_MemberId");
            RenameIndex(table: "dbo.DeMerits", name: "IX_EmployeeId", newName: "IX_MemberId");
            RenameIndex(table: "dbo.MemberNotices", name: "IX_EmployeeId", newName: "IX_MemberId");
            AddColumn("dbo.Buses", "RouteId", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "FullName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Members", "ShortName", c => c.String(maxLength: 100));
            AddColumn("dbo.Members", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.Members", "PermanetAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.Members", "CurrentAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.Members", "TelNo", c => c.String());
            AddColumn("dbo.Members", "NIC", c => c.String());
            AddColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "TelNo", c => c.String(maxLength: 100));
            CreateIndex("dbo.Buses", "RouteId");
            AddForeignKey("dbo.Buses", "RouteId", "dbo.Routes", "ID", cascadeDelete: false);
            DropColumn("dbo.DeMerits", "User_ID");
            DropColumn("dbo.Routes", "Route_ID");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "DOB");
            DropColumn("dbo.Users", "PrivateAddress");
            DropColumn("dbo.Users", "CUrrentAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CUrrentAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.Users", "PrivateAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.Users", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "LastName", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Routes", "Route_ID", c => c.Int());
            AddColumn("dbo.DeMerits", "User_ID", c => c.Int());
            DropForeignKey("dbo.Buses", "RouteId", "dbo.Routes");
            DropIndex("dbo.Buses", new[] { "RouteId" });
            AlterColumn("dbo.Users", "TelNo", c => c.String());
            DropColumn("dbo.Users", "LastLogin");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "UserName");
            DropColumn("dbo.Members", "NIC");
            DropColumn("dbo.Members", "TelNo");
            DropColumn("dbo.Members", "CurrentAddress");
            DropColumn("dbo.Members", "PermanetAddress");
            DropColumn("dbo.Members", "DOB");
            DropColumn("dbo.Members", "ShortName");
            DropColumn("dbo.Members", "FullName");
            DropColumn("dbo.Buses", "RouteId");
            RenameIndex(table: "dbo.MemberNotices", name: "IX_MemberId", newName: "IX_EmployeeId");
            RenameIndex(table: "dbo.DeMerits", name: "IX_MemberId", newName: "IX_EmployeeId");
            RenameIndex(table: "dbo.Complains", name: "IX_MemberId", newName: "IX_EmployeeId");
            RenameColumn(table: "dbo.Complains", name: "MemberId", newName: "EmployeeId");
            RenameColumn(table: "dbo.MemberNotices", name: "MemberId", newName: "EmployeeId");
            RenameColumn(table: "dbo.DeMerits", name: "MemberId", newName: "EmployeeId");
            CreateIndex("dbo.Routes", "Route_ID");
            CreateIndex("dbo.DeMerits", "User_ID");
            AddForeignKey("dbo.DeMerits", "User_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Routes", "Route_ID", "dbo.Routes", "ID");
        }
    }
}
