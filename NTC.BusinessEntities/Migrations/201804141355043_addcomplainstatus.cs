namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcomplainstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complains", "ComplainStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complains", "ComplainStatus");
        }
    }
}
