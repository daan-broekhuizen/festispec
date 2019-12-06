namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Begin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Opdracht", "MedewerkerID", "dbo.Account");
            DropIndex("dbo.Opdracht", new[] { "MedewerkerID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Opdracht", "MedewerkerID");
            AddForeignKey("dbo.Opdracht", "MedewerkerID", "dbo.Account", "AccountID");
        }
    }
}
