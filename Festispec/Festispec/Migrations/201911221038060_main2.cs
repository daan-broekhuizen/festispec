namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class main2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Opdracht", "Start_datum", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Opdracht", "Eind_datum", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Opdracht", "Eind_datum");
            DropColumn("dbo.Opdracht", "Start_datum");
        }
    }
}
