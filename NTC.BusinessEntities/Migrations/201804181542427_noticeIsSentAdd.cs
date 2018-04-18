namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noticeIsSentAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notices", "IsSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notices", "IsSent");
        }
    }
}
