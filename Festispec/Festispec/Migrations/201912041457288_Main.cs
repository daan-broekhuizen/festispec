namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Main : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Gebruikersnaam = c.String(nullable: false, maxLength: 45),
                        Wachtwoord = c.String(nullable: false, maxLength: 45),
                        Rol = c.String(nullable: false, maxLength: 2),
                        Voornaam = c.String(nullable: false, maxLength: 30),
                        Tussenvoegsel = c.String(maxLength: 15),
                        Achternaam = c.String(nullable: false, maxLength: 30),
                        Stad = c.String(maxLength: 50),
                        Straatnaam = c.String(maxLength: 50),
                        Huisnummer = c.String(maxLength: 4),
                        Email = c.String(maxLength: 120),
                        Telefoonnummer = c.String(maxLength: 10),
                        Datum_certificering = c.DateTime(storeType: "date"),
                        Einddatum_certificering = c.DateTime(storeType: "date"),
                        IBAN = c.String(maxLength: 18),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.Rol_lookup", t => t.Rol)
                .Index(t => t.Rol);
            
            CreateTable(
                "dbo.Antwoorden",
                c => new
                    {
                        VraagID = c.Int(nullable: false),
                        AntwoordNummer = c.Int(nullable: false),
                        InspecteurID = c.Int(nullable: false),
                        Antwoord_text = c.String(unicode: false, storeType: "text"),
                        Antwoord_image = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => new { t.VraagID, t.AntwoordNummer, t.InspecteurID })
                .ForeignKey("dbo.Vraag", t => t.VraagID)
                .ForeignKey("dbo.Account", t => t.InspecteurID, cascadeDelete: true)
                .Index(t => t.VraagID)
                .Index(t => t.InspecteurID);
            
            CreateTable(
                "dbo.Vraag",
                c => new
                    {
                        VraagID = c.Int(nullable: false, identity: true),
                        Vraagstelling = c.String(nullable: false, unicode: false, storeType: "text"),
                        VolgordeNummer = c.Int(nullable: false),
                        InspectieFormulierID = c.Int(nullable: false),
                        Vraagtype = c.String(nullable: false, maxLength: 2),
                        AfbeeldingURL = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.VraagID)
                .ForeignKey("dbo.Vraagtype_lookup", t => t.Vraagtype)
                .ForeignKey("dbo.Inspectieformulier", t => t.InspectieFormulierID, cascadeDelete: true)
                .Index(t => t.InspectieFormulierID)
                .Index(t => t.Vraagtype);
            
            CreateTable(
                "dbo.Vraag_mogelijk_antwoord",
                c => new
                    {
                        VraagID = c.Int(nullable: false),
                        Antwoord_nummer = c.Int(nullable: false),
                        Antwoord_text = c.String(nullable: false, unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => new { t.VraagID, t.Antwoord_nummer })
                .ForeignKey("dbo.Vraag", t => t.VraagID)
                .Index(t => t.VraagID);
            
            CreateTable(
                "dbo.Vraagtype_lookup",
                c => new
                    {
                        Afkorting = c.String(nullable: false, maxLength: 2),
                        Beschrijving = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Afkorting);
            
            CreateTable(
                "dbo.Beschikbaarheid_inspecteurs",
                c => new
                    {
                        MedewerkerID = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => new { t.MedewerkerID, t.Datum })
                .ForeignKey("dbo.Account", t => t.MedewerkerID, cascadeDelete: true)
                .Index(t => t.MedewerkerID);
            
            CreateTable(
                "dbo.Opdracht",
                c => new
                    {
                        OpdrachtID = c.Int(nullable: false, identity: true),
                        Opdracht_naam = c.String(nullable: false, maxLength: 45),
                        Status = c.String(nullable: false, maxLength: 30),
                        Creatie_datum = c.DateTime(nullable: false, storeType: "date"),
                        Start_datum = c.DateTime(nullable: false, storeType: "date"),
                        Eind_datum = c.DateTime(nullable: false, storeType: "date"),
                        KlantID = c.String(nullable: false, maxLength: 8),
                        MedewerkerID = c.Int(nullable: false),
                        Klantwensen = c.String(nullable: false, unicode: false, storeType: "text"),
                        Gebruikte_rechtsgebieden = c.String(unicode: false, storeType: "text"),
                        Rapportage = c.String(unicode: false, storeType: "text"),
                        Rapportage_uses_template = c.Int(),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OpdrachtID)
                .ForeignKey("dbo.Klant", t => t.KlantID)
                .ForeignKey("dbo.Rapport_template", t => t.Rapportage_uses_template)
                .ForeignKey("dbo.Status_lookup", t => t.Status)
                .ForeignKey("dbo.Account", t => t.MedewerkerID)
                .Index(t => t.Status)
                .Index(t => t.KlantID)
                .Index(t => t.MedewerkerID)
                .Index(t => t.Rapportage_uses_template);
            
            CreateTable(
                "dbo.Inspectieformulier",
                c => new
                    {
                        InspectieformulierID = c.Int(nullable: false, identity: true),
                        InspectieFormulierTitel = c.String(nullable: false, maxLength: 45),
                        Datum_inspectie = c.DateTime(storeType: "date"),
                        Stad = c.String(maxLength: 50),
                        Straatnaam = c.String(maxLength: 50),
                        Huisnummer = c.String(maxLength: 4),
                        OpdrachtID = c.Int(),
                        Beschrijving = c.String(unicode: false, storeType: "text"),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InspectieformulierID)
                .ForeignKey("dbo.Opdracht", t => t.OpdrachtID)
                .Index(t => t.OpdrachtID);
            
            CreateTable(
                "dbo.Klant",
                c => new
                    {
                        KvK_nummer = c.String(nullable: false, maxLength: 8),
                        Naam = c.String(nullable: false, maxLength: 45),
                        Stad = c.String(nullable: false, maxLength: 50),
                        Straatnaam = c.String(nullable: false, maxLength: 50),
                        Huisnummer = c.String(nullable: false, maxLength: 4),
                        Telefoonnummer = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 130),
                        Website = c.String(maxLength: 100),
                        Klant_logo = c.Binary(storeType: "image"),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.KvK_nummer);
            
            CreateTable(
                "dbo.Contactpersoon",
                c => new
                    {
                        ContactpersoonID = c.Int(nullable: false, identity: true),
                        KlantID = c.String(nullable: false, maxLength: 8),
                        Voornaam = c.String(nullable: false, maxLength: 30),
                        Tussenvoegsel = c.String(maxLength: 15),
                        Achternaam = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 130),
                        Telefoon = c.String(nullable: false, maxLength: 10),
                        Notities = c.String(unicode: false, storeType: "text"),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactpersoonID)
                .ForeignKey("dbo.Klant", t => t.KlantID, cascadeDelete: true)
                .Index(t => t.KlantID);
            
            CreateTable(
                "dbo.Offerte",
                c => new
                    {
                        OfferteID = c.Int(nullable: false, identity: true),
                        OpdrachtID = c.Int(nullable: false),
                        Totaalbedrag = c.Decimal(precision: 10, scale: 2),
                        Aanmaakdatum = c.DateTime(nullable: false, storeType: "date"),
                        Beschrijving = c.String(unicode: false, storeType: "text"),
                        Klantbeslissing_reden = c.String(unicode: false, storeType: "text"),
                        Laatste_wijziging = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OfferteID)
                .ForeignKey("dbo.Opdracht", t => t.OpdrachtID, cascadeDelete: true)
                .Index(t => t.OpdrachtID);
            
            CreateTable(
                "dbo.Rapport_template",
                c => new
                    {
                        TemplateID = c.Int(nullable: false, identity: true),
                        TemplateText = c.String(unicode: false, storeType: "text"),
                        TemplateName = c.String(maxLength: 50),
                        TemplateDescription = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.TemplateID);
            
            CreateTable(
                "dbo.Status_lookup",
                c => new
                    {
                        Betekenis = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Betekenis);
            
            CreateTable(
                "dbo.Rol_lookup",
                c => new
                    {
                        Afkorting = c.String(nullable: false, maxLength: 2),
                        Betekenis = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Afkorting);
            
            CreateTable(
                "dbo.Ingeplande_inspecteurs",
                c => new
                    {
                        InspecteurID = c.Int(nullable: false),
                        OpdrachtID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InspecteurID, t.OpdrachtID })
                .ForeignKey("dbo.Account", t => t.InspecteurID, cascadeDelete: true)
                .ForeignKey("dbo.Opdracht", t => t.OpdrachtID, cascadeDelete: true)
                .Index(t => t.InspecteurID)
                .Index(t => t.OpdrachtID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Account", "Rol", "dbo.Rol_lookup");
            DropForeignKey("dbo.Opdracht", "MedewerkerID", "dbo.Account");
            DropForeignKey("dbo.Ingeplande_inspecteurs", "OpdrachtID", "dbo.Opdracht");
            DropForeignKey("dbo.Ingeplande_inspecteurs", "InspecteurID", "dbo.Account");
            DropForeignKey("dbo.Opdracht", "Status", "dbo.Status_lookup");
            DropForeignKey("dbo.Opdracht", "Rapportage_uses_template", "dbo.Rapport_template");
            DropForeignKey("dbo.Offerte", "OpdrachtID", "dbo.Opdracht");
            DropForeignKey("dbo.Opdracht", "KlantID", "dbo.Klant");
            DropForeignKey("dbo.Contactpersoon", "KlantID", "dbo.Klant");
            DropForeignKey("dbo.Vraag", "InspectieFormulierID", "dbo.Inspectieformulier");
            DropForeignKey("dbo.Inspectieformulier", "OpdrachtID", "dbo.Opdracht");
            DropForeignKey("dbo.Beschikbaarheid_inspecteurs", "MedewerkerID", "dbo.Account");
            DropForeignKey("dbo.Antwoorden", "InspecteurID", "dbo.Account");
            DropForeignKey("dbo.Vraag", "Vraagtype", "dbo.Vraagtype_lookup");
            DropForeignKey("dbo.Vraag_mogelijk_antwoord", "VraagID", "dbo.Vraag");
            DropForeignKey("dbo.Antwoorden", "VraagID", "dbo.Vraag");
            DropIndex("dbo.Ingeplande_inspecteurs", new[] { "OpdrachtID" });
            DropIndex("dbo.Ingeplande_inspecteurs", new[] { "InspecteurID" });
            DropIndex("dbo.Offerte", new[] { "OpdrachtID" });
            DropIndex("dbo.Contactpersoon", new[] { "KlantID" });
            DropIndex("dbo.Inspectieformulier", new[] { "OpdrachtID" });
            DropIndex("dbo.Opdracht", new[] { "Rapportage_uses_template" });
            DropIndex("dbo.Opdracht", new[] { "MedewerkerID" });
            DropIndex("dbo.Opdracht", new[] { "KlantID" });
            DropIndex("dbo.Opdracht", new[] { "Status" });
            DropIndex("dbo.Beschikbaarheid_inspecteurs", new[] { "MedewerkerID" });
            DropIndex("dbo.Vraag_mogelijk_antwoord", new[] { "VraagID" });
            DropIndex("dbo.Vraag", new[] { "Vraagtype" });
            DropIndex("dbo.Vraag", new[] { "InspectieFormulierID" });
            DropIndex("dbo.Antwoorden", new[] { "InspecteurID" });
            DropIndex("dbo.Antwoorden", new[] { "VraagID" });
            DropIndex("dbo.Account", new[] { "Rol" });
            DropTable("dbo.Ingeplande_inspecteurs");
            DropTable("dbo.Rol_lookup");
            DropTable("dbo.Status_lookup");
            DropTable("dbo.Rapport_template");
            DropTable("dbo.Offerte");
            DropTable("dbo.Contactpersoon");
            DropTable("dbo.Klant");
            DropTable("dbo.Inspectieformulier");
            DropTable("dbo.Opdracht");
            DropTable("dbo.Beschikbaarheid_inspecteurs");
            DropTable("dbo.Vraagtype_lookup");
            DropTable("dbo.Vraag_mogelijk_antwoord");
            DropTable("dbo.Vraag");
            DropTable("dbo.Antwoorden");
            DropTable("dbo.Account");
        }
    }
}
