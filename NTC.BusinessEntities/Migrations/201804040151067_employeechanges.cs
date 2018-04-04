namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeechanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "IssuedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "IssuedDate", c => c.String());
        }
    }
}
