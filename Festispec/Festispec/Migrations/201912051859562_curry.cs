namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class curry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inspectieformulier", "Benodigde_Inspecteurs", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inspectieformulier", "Benodigde_Inspecteurs");
        }
    }
}
