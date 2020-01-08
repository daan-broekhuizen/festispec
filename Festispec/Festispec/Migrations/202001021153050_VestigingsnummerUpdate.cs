namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VestigingsnummerUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.String(maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.Int(nullable: false));
        }
    }
}
