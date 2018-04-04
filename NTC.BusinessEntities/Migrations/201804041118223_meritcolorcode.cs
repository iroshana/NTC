namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meritcolorcode : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "UserID", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "UserID" });
            AddColumn("dbo.Merits", "Point", c => c.Int(nullable: false));
            AddColumn("dbo.Merits", "ColorCodeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "UserID", c => c.Int());
            CreateIndex("dbo.Employees", "UserID");
            AddForeignKey("dbo.Employees", "UserID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "UserID", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "UserID" });
            AlterColumn("dbo.Employees", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Merits", "ColorCodeId");
            DropColumn("dbo.Merits", "Point");
            CreateIndex("dbo.Employees", "UserID");
            AddForeignKey("dbo.Employees", "UserID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
