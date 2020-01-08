namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vestigigsnummer_fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.Int(nullable: false));
        }
    }
}
