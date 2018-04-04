namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categorychanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryComplains", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.CategoryComplains", "Complain_ID", "dbo.Complains");
            DropIndex("dbo.CategoryComplains", new[] { "Category_ID" });
            DropIndex("dbo.CategoryComplains", new[] { "Complain_ID" });
            CreateTable(
                "dbo.ComplainCategories",
                c => new
                    {
                        ComplainId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.ComplainId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Complains", t => t.ComplainId, cascadeDelete: true)
                .Index(t => t.ComplainId)
                .Index(t => t.CategoryId);
            
            AddColumn("dbo.Complains", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Complains", "IsInqueryParticipation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Evidences", "FilePath", c => c.String());
            DropColumn("dbo.Complains", "Time");
            DropTable("dbo.CategoryComplains");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryComplains",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Complain_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Complain_ID });
            
            AddColumn("dbo.Complains", "Time", c => c.String());
            DropForeignKey("dbo.ComplainCategories", "ComplainId", "dbo.Complains");
            DropForeignKey("dbo.ComplainCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ComplainCategories", new[] { "CategoryId" });
            DropIndex("dbo.ComplainCategories", new[] { "ComplainId" });
            DropColumn("dbo.Evidences", "FilePath");
            DropColumn("dbo.Complains", "IsInqueryParticipation");
            DropColumn("dbo.Complains", "Date");
            DropTable("dbo.ComplainCategories");
            CreateIndex("dbo.CategoryComplains", "Complain_ID");
            CreateIndex("dbo.CategoryComplains", "Category_ID");
            AddForeignKey("dbo.CategoryComplains", "Complain_ID", "dbo.Complains", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CategoryComplains", "Category_ID", "dbo.Categories", "ID", cascadeDelete: true);
        }
    }
}
