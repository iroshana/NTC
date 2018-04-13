namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addntcNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "NTCNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "NTCNo");
        }
    }
}
