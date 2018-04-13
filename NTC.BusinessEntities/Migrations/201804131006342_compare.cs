namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class compare : DbMigration
    {
        public override void Up()
        {
            //CreateIndex("dbo.Complains", "ConductorId");
            //AddForeignKey("dbo.Complains", "ConductorId", "dbo.Members", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complains", "ConductorId", "dbo.Members");
            DropIndex("dbo.Complains", new[] { "ConductorId" });
        }
    }
}
