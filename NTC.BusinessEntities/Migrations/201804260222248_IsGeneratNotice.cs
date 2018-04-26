namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsGeneratNotice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notices", "IsGeneratNotice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notices", "IsGeneratNotice");
        }
    }
}
