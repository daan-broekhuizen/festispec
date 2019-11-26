namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UndoAfkorting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Opdracht", "Status", "dbo.Status_lookup");
            DropIndex("dbo.Opdracht", new[] { "Status" });
            DropPrimaryKey("dbo.Status_lookup");
            AlterColumn("dbo.Opdracht", "Status", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.Status_lookup", "Betekenis");
            CreateIndex("dbo.Opdracht", "Status");
            AddForeignKey("dbo.Opdracht", "Status", "dbo.Status_lookup", "Betekenis");
            DropColumn("dbo.Status_lookup", "Afkorting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status_lookup", "Afkorting", c => c.String(nullable: false, maxLength: 2));
            DropForeignKey("dbo.Opdracht", "Status", "dbo.Status_lookup");
            DropIndex("dbo.Opdracht", new[] { "Status" });
            DropPrimaryKey("dbo.Status_lookup");
            AlterColumn("dbo.Opdracht", "Status", c => c.String(nullable: false, maxLength: 2));
            AddPrimaryKey("dbo.Status_lookup", "Afkorting");
            CreateIndex("dbo.Opdracht", "Status");
            AddForeignKey("dbo.Opdracht", "Status", "dbo.Status_lookup", "Afkorting");
        }
    }
}
