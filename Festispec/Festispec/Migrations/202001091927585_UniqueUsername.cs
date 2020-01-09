namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueUsername : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Account", "Gebruikersnaam", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Account", new[] { "Gebruikersnaam" });
        }
    }
}
