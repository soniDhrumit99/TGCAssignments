namespace ProductManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingAccounts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Accounts (Username, Password) VALUES ('admin', 'admin')");
            Sql("INSERT INTO Accounts (Username, Password) VALUES ('customer', 'customer')");
        }
        
        public override void Down()
        {
        }
    }
}
