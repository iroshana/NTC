namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class es : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complains", "ConductorId", "dbo.Members");
            DropIndex("dbo.Complains", new[] { "ConductorId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Complains", "ConductorId");
            AddForeignKey("dbo.Complains", "ConductorId", "dbo.Members", "ID", cascadeDelete: true);
        }
    }
}
