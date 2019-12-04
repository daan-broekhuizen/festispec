namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VraagAntwoord : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Antwoorden", "VraagID");
            AddForeignKey("dbo.Antwoorden", "VraagID", "dbo.Vraag", "VraagID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Antwoorden", "VraagID", "dbo.Vraag");
            DropIndex("dbo.Antwoorden", new[] { "VraagID" });
        }
    }
}
