namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddriverConductortocomplain : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Complains", name: "MemberId", newName: "DriverId");
            RenameIndex(table: "dbo.Complains", name: "IX_MemberId", newName: "IX_DriverId");
            AddColumn("dbo.Complains", "ConductorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complains", "ConductorId");
            RenameIndex(table: "dbo.Complains", name: "IX_DriverId", newName: "IX_MemberId");
            RenameColumn(table: "dbo.Complains", name: "DriverId", newName: "MemberId");
        }
    }
}
