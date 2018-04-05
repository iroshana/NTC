namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complainchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complains", "UserId", "dbo.Users");
            DropIndex("dbo.Complains", new[] { "UserId" });
            AddColumn("dbo.DeMerits", "DeMeritNo", c => c.String());
            AddColumn("dbo.Notices", "MemberId", c => c.Int());
            AlterColumn("dbo.Complains", "UserId", c => c.Int());
            CreateIndex("dbo.Complains", "UserId");
            CreateIndex("dbo.Notices", "MemberId");
            AddForeignKey("dbo.Notices", "MemberId", "dbo.Members", "ID");
            AddForeignKey("dbo.Complains", "UserId", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complains", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notices", "MemberId", "dbo.Members");
            DropIndex("dbo.Notices", new[] { "MemberId" });
            DropIndex("dbo.Complains", new[] { "UserId" });
            AlterColumn("dbo.Complains", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Notices", "MemberId");
            DropColumn("dbo.DeMerits", "DeMeritNo");
            CreateIndex("dbo.Complains", "UserId");
            AddForeignKey("dbo.Complains", "UserId", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
