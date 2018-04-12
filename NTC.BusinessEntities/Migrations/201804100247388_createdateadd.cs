namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdateadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evidences", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeMerits", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notices", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notices", "CreatedDate");
            DropColumn("dbo.DeMerits", "CreatedDate");
            DropColumn("dbo.Evidences", "CreatedDate");
        }
    }
}
