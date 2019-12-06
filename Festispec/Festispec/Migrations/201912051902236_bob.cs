namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inspectieformulier", "Benodigde_Inspecteurs", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inspectieformulier", "Benodigde_Inspecteurs", c => c.Int(nullable: false));
        }
    }
}
