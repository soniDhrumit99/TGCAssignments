namespace ProductManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingProducts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products (Name, Category, Price, Quantity, Description, ImagePath) VALUES ('First Product', 1, 123.213, 5, 'Some description', 'some path')");
            Sql("INSERT INTO Products (Name, Category, Price, Quantity, Description, ImagePath) VALUES ('Second Product', 2, 123.213, 10, 'Some description', 'some path')");
            Sql("INSERT INTO Products (Name, Category, Price, Quantity, Description, ImagePath) VALUES ('Third Product', 3, 123.213, 15, 'Some description', 'some path')");
            Sql("INSERT INTO Products (Name, Category, Price, Quantity, Description, ImagePath) VALUES ('Fourth Product', 4, 123.213, 20, 'Some description', 'some path')");
            Sql("INSERT INTO Products (Name, Category, Price, Quantity, Description, ImagePath) VALUES ('Fifth Product', 5, 123.213, 25, 'Some description', 'some path')");
        }
        
        public override void Down()
        {
        }
    }
}
