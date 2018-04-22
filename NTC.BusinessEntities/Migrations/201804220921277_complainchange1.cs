namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complainchange1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complains", "DriverId", "dbo.Members");
            DropForeignKey("dbo.Complains", "ConductorId", "dbo.Members");
            DropIndex("dbo.Complains", new[] { "ConductorId" });
            DropIndex("dbo.Complains", new[] { "DriverId" });
            AlterColumn("dbo.Complains", "ConductorId", c => c.Int());
            AlterColumn("dbo.Complains", "DriverId", c => c.Int());
            CreateIndex("dbo.Complains", "ConductorId");
            CreateIndex("dbo.Complains", "DriverId");
            AddForeignKey("dbo.Complains", "DriverId", "dbo.Members", "ID");
            AddForeignKey("dbo.Complains", "ConductorId", "dbo.Members", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complains", "ConductorId", "dbo.Members");
            DropForeignKey("dbo.Complains", "DriverId", "dbo.Members");
            DropIndex("dbo.Complains", new[] { "DriverId" });
            DropIndex("dbo.Complains", new[] { "ConductorId" });
            AlterColumn("dbo.Complains", "DriverId", c => c.Int(nullable: false));
            AlterColumn("dbo.Complains", "ConductorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Complains", "DriverId");
            CreateIndex("dbo.Complains", "ConductorId");
            AddForeignKey("dbo.Complains", "ConductorId", "dbo.Members", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Complains", "DriverId", "dbo.Members", "ID", cascadeDelete: true);
        }
    }
}
