namespace SourceControlFInalAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using SourceControlFInalAssignment.Context;
    
    public partial class adminSeed : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO LoginModels VALUES ('admin', 'admin', 'false')");
            Sql("INSERT INTO LoginModels VALUES ('admin2', 'admin2', 'false')");
        }
        
        public override void Down()
        {

        }
    }
}
