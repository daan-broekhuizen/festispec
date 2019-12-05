namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class badjas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspectieformulier", "StartTijd", c => c.Time(precision: 7));
            AddColumn("dbo.Inspectieformulier", "EindTijd", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inspectieformulier", "EindTijd");
            DropColumn("dbo.Inspectieformulier", "StartTijd");
        }
    }
}
