namespace SourceControlFinalAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 80),
                        Phone = c.Long(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Gender = c.Short(nullable: false),
                        City = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Logs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false),
                    MachineName = c.String(nullable: true, maxLength: 200),
                    Username= c.String(nullable: false, maxLength: 200),
                    Level = c.String(nullable: false, maxLength: 5),
                    Logger= c.String(nullable: true, maxLength: 300),
                    Callsite = c.String(nullable: true, maxLength: 300),
                    Message = c.String(nullable: false),
                    Properties = c.String(nullable: true),
                    Exception = c.String(nullable: true),
                    Stacktrace = c.String(nullable: true)
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Logins");
            DropTable("dbo.Logs");
        }
    }
}
