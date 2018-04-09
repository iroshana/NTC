namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeMemIdfromNotice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notices", "MemberId", "dbo.Members");
            DropIndex("dbo.Notices", new[] { "MemberId" });
            DropColumn("dbo.Notices", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notices", "MemberId", c => c.Int());
            CreateIndex("dbo.Notices", "MemberId");
            AddForeignKey("dbo.Notices", "MemberId", "dbo.Members", "ID");
        }
    }
}
