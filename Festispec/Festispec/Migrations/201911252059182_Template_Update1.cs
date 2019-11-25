namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Template_Update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rapport_template", "TemplateName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Rapport_template", "TemplateDescription", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rapport_template", "TemplateDescription", c => c.String(maxLength: 50));
            AlterColumn("dbo.Rapport_template", "TemplateName", c => c.String(maxLength: 20));
        }
    }
}
