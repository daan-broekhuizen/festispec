namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spGetChartData : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("sp_GetChartData", p => new { VraagID = p.Int() }, 
                @"BEGIN
                    SELECT VraagID AS QuestionID, CAST(Antwoord_text AS VARCHAR(max)) AS Answer, COUNT(CAST(Antwoord_text AS VARCHAR(max))) AS Amount
                    FROM Antwoorden
                    GROUP BY VraagID, CAST(Antwoord_text AS VARCHAR(max))
                    HAVING VraagID = @VraagID;
                END;"
            );
        }
        
        public override void Down()
        {
            DropStoredProcedure("sp_GetChartData");
        }
    }
}
