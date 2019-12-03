namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedQustionTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Vraag", "Laatste_wijziging");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vraag", "Laatste_wijziging", c => c.DateTime(nullable: false));
        }
    }
}
