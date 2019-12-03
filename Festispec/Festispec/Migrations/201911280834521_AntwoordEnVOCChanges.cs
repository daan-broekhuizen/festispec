namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AntwoordEnVOCChanges : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Antwoorden");
            AddColumn("dbo.Antwoorden", "AntwoordNummer", c => c.Int(nullable: false));
            AddColumn("dbo.Inspectieformulier_vragenlijst_combinatie", "VraagVolgordeNummer", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Antwoorden", new[] { "VIC_ID", "AntwoordNummer", "InspecteurID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Antwoorden");
            DropColumn("dbo.Inspectieformulier_vragenlijst_combinatie", "VraagVolgordeNummer");
            DropColumn("dbo.Antwoorden", "AntwoordNummer");
            AddPrimaryKey("dbo.Antwoorden", new[] { "VIC_ID", "InspecteurID" });
        }
    }
}
