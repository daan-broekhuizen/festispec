namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Template_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rapport_template", "TemplateName", c => c.String(maxLength: 20));
            AddColumn("dbo.Rapport_template", "TemplateDescription", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rapport_template", "TemplateDescription");
            DropColumn("dbo.Rapport_template", "TemplateName");
        }
    }
}
