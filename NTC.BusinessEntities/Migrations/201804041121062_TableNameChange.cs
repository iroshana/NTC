namespace NTC.BusinessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNameChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Employees", newName: "Members");
            RenameTable(name: "dbo.EmployeeTypes", newName: "MemberTypes");
            RenameTable(name: "dbo.EmployeeNotices", newName: "MemberNotices");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MemberNotices", newName: "EmployeeNotices");
            RenameTable(name: "dbo.MemberTypes", newName: "EmployeeTypes");
            RenameTable(name: "dbo.Members", newName: "Employees");
        }
    }
}
