namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkedInspectionformQuestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Antwoorden", "VIC_ID", "dbo.Inspectieformulier_vragenlijst_combinatie");
            DropForeignKey("dbo.Inspectieformulier_vragenlijst_combinatie", "InspectieformulierID", "dbo.Inspectieformulier");
            DropForeignKey("dbo.Inspectieformulier_vragenlijst_combinatie", "VraagID", "dbo.Vraag");
            DropIndex("dbo.Antwoorden", new[] { "VIC_ID" });
            DropIndex("dbo.Inspectieformulier_vragenlijst_combinatie", new[] { "InspectieformulierID" });
            DropIndex("dbo.Inspectieformulier_vragenlijst_combinatie", new[] { "VraagID" });
            DropPrimaryKey("dbo.Antwoorden");
            AddColumn("dbo.Antwoorden", "VraagID", c => c.Int(nullable: false));
            AddColumn("dbo.Vraag", "VolgordeNummer", c => c.Int(nullable: false));
            AddColumn("dbo.Vraag", "InspectieFormulierID", c => c.Int(nullable: false));
            AlterColumn("dbo.Antwoorden", "AntwoordNummer", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Antwoorden", new[] { "VraagID", "AntwoordNummer", "InspecteurID" });
            CreateIndex("dbo.Vraag", "InspectieFormulierID");
            AddForeignKey("dbo.Vraag", "InspectieFormulierID", "dbo.Inspectieformulier", "InspectieformulierID", cascadeDelete: true);
            DropColumn("dbo.Antwoorden", "VIC_ID");
            DropColumn("dbo.Antwoorden", "Laatste_wijziging");
            DropTable("dbo.Inspectieformulier_vragenlijst_combinatie");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Inspectieformulier_vragenlijst_combinatie",
                c => new
                    {
                        VIC_ID = c.Int(nullable: false, identity: true),
                        InspectieformulierID = c.Int(nullable: false),
                        VraagID = c.Int(nullable: false),
                        VraagVolgordeNummer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VIC_ID);
            
            AddColumn("dbo.Antwoorden", "Laatste_wijziging", c => c.DateTime(nullable: false));
            AddColumn("dbo.Antwoorden", "VIC_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Vraag", "InspectieFormulierID", "dbo.Inspectieformulier");
            DropIndex("dbo.Vraag", new[] { "InspectieFormulierID" });
            DropPrimaryKey("dbo.Antwoorden");
            AlterColumn("dbo.Antwoorden", "AntwoordNummer", c => c.Int(nullable: false));
            DropColumn("dbo.Vraag", "InspectieFormulierID");
            DropColumn("dbo.Vraag", "VolgordeNummer");
            DropColumn("dbo.Antwoorden", "VraagID");
            AddPrimaryKey("dbo.Antwoorden", new[] { "VIC_ID", "AntwoordNummer", "InspecteurID" });
            CreateIndex("dbo.Inspectieformulier_vragenlijst_combinatie", "VraagID");
            CreateIndex("dbo.Inspectieformulier_vragenlijst_combinatie", "InspectieformulierID");
            CreateIndex("dbo.Antwoorden", "VIC_ID");
            AddForeignKey("dbo.Inspectieformulier_vragenlijst_combinatie", "VraagID", "dbo.Vraag", "VraagID", cascadeDelete: true);
            AddForeignKey("dbo.Inspectieformulier_vragenlijst_combinatie", "InspectieformulierID", "dbo.Inspectieformulier", "InspectieformulierID", cascadeDelete: true);
            AddForeignKey("dbo.Antwoorden", "VIC_ID", "dbo.Inspectieformulier_vragenlijst_combinatie", "VIC_ID", cascadeDelete: true);
        }
    }
}
