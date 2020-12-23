namespace SourceControlFinalAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRegistrationViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 80),
                        Phone = c.Long(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Gender = c.Short(nullable: false),
                        City = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRegistrationViewModels");
        }
    }
}
