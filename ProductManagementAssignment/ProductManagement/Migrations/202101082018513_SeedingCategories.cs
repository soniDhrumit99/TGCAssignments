namespace ProductManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Name) VALUES ('Electronics')");
            Sql("INSERT INTO Categories (Name) VALUES ('Home Appliances')");
            Sql("INSERT INTO Categories (Name) VALUES ('Kitchen Appliances')");
            Sql("INSERT INTO Categories (Name) VALUES ('Apparels')");
            Sql("INSERT INTO Categories (Name) VALUES ('Footwear')");
        }
        
        public override void Down()
        {
        }
    }
}
