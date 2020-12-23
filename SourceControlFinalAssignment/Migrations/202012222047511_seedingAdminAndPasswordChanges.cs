namespace SourceControlFinalAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingAdminAndPasswordChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logins", "password", c => c.String(nullable: false, maxLength: 16));
            Sql("INSERT INTO Logins VALUES('admin', 'admin')");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logins", "password", c => c.String(nullable: false));
        }
    }
}
