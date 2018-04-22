namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcomplaincontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complains", "ComplainerName", c => c.String());
            AddColumn("dbo.Complains", "ComplainerAddress", c => c.String());
            AddColumn("dbo.Complains", "ComplainerTel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complains", "ComplainerTel");
            DropColumn("dbo.Complains", "ComplainerAddress");
            DropColumn("dbo.Complains", "ComplainerName");
        }
    }
}
