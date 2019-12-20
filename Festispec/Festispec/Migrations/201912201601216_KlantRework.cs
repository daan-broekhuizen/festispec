namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRework : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contactpersoon", "KlantID", "dbo.Klant");
            DropForeignKey("dbo.Opdracht", "KlantID", "dbo.Klant");
            DropIndex("dbo.Opdracht", new[] { "KlantID" });
            DropIndex("dbo.Contactpersoon", new[] { "KlantID" });
            DropPrimaryKey("dbo.Klant");
            AddColumn("dbo.Klant", "KlantID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Klant", "Vestigingnummer", c => c.Int(nullable: false));
            AddColumn("dbo.Contactpersoon", "Rol", c => c.String(maxLength: 30));
            AlterColumn("dbo.Opdracht", "KlantID", c => c.Int(nullable: false));
            AlterColumn("dbo.Klant", "KvK_nummer", c => c.String(maxLength: 8));
            AlterColumn("dbo.Contactpersoon", "KlantID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Klant", "KlantID");
            CreateIndex("dbo.Opdracht", "KlantID");
            CreateIndex("dbo.Contactpersoon", "KlantID");
            AddForeignKey("dbo.Contactpersoon", "KlantID", "dbo.Klant", "KlantID", cascadeDelete: true);
            AddForeignKey("dbo.Opdracht", "KlantID", "dbo.Klant", "KlantID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opdracht", "KlantID", "dbo.Klant");
            DropForeignKey("dbo.Contactpersoon", "KlantID", "dbo.Klant");
            DropIndex("dbo.Contactpersoon", new[] { "KlantID" });
            DropIndex("dbo.Opdracht", new[] { "KlantID" });
            DropPrimaryKey("dbo.Klant");
            AlterColumn("dbo.Contactpersoon", "KlantID", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Klant", "KvK_nummer", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.Opdracht", "KlantID", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.Contactpersoon", "Rol");
            DropColumn("dbo.Klant", "Vestigingnummer");
            DropColumn("dbo.Klant", "KlantID");
            AddPrimaryKey("dbo.Klant", "KvK_nummer");
            CreateIndex("dbo.Contactpersoon", "KlantID");
            CreateIndex("dbo.Opdracht", "KlantID");
            AddForeignKey("dbo.Opdracht", "KlantID", "dbo.Klant", "KvK_nummer");
            AddForeignKey("dbo.Contactpersoon", "KlantID", "dbo.Klant", "KvK_nummer", cascadeDelete: true);
        }
    }
}
