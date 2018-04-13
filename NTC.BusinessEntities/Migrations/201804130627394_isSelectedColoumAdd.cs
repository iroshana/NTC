namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isSelectedColoumAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComplainCategories", "IsSelected", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComplainCategories", "IsSelected");
        }
    }
}
