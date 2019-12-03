namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionImageUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vraag", "AfbeeldingURL", c => c.String(maxLength: 150));
            DropColumn("dbo.Vraag", "Bijlage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vraag", "Bijlage", c => c.Binary(storeType: "image"));
            DropColumn("dbo.Vraag", "AfbeeldingURL");
        }
    }
}
