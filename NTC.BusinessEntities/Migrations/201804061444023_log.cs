namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogMessages",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ApplicationName = c.String(nullable: false, maxLength: 500),
                        Message = c.String(nullable: false),
                        User = c.String(nullable: false, maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogMessages");
        }
    }
}
