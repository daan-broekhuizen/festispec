namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VestiginsSizeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klant", "Vestigingnummer", c => c.String(maxLength: 8));
        }
    }
}
