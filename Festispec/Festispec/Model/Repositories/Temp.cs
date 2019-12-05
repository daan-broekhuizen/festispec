using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class Temp
    {
        public void Test(string festival)
        {
            using (FestispecContext context = new FestispecContext())
            {
                var param = new SqlParameter("@Event", festival);
                Account acc = new Account();
               
                var result = context.Database.SqlQuery<Account>("GetInspectors @Event", param).ToList();

                foreach(var r in result)
                {
                    Console.WriteLine(r);
                }
                /*
                 * 
DROP PROCEDURE GetInspectors

CREATE PROCEDURE GetInspectors @Event nvarchar(100)
AS
SELECT Account.AccountID, Account.Gebruikersnaam as Inspecteur FROM  Account 
INNER JOIN Beschikbaarheid_inspecteurs
ON Account.AccountID = Beschikbaarheid_inspecteurs.MedewerkerID 
INNER JOIN Inspectieformulier
ON
Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie 
WHERE Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie
AND Inspectieformulier.InspectieFormulierTitel = @Event

EXEC GetInspectors @Event = 'Test'

SELECT * FROM Account

SELECT * FROM Beschikbaarheid_inspecteurs

SELECT * FROM Inspectieformulier


SELECT Account.AccountID, Account.Gebruikersnaam as Inspecteur, Inspectieformulier.Datum_Inspectie as Datum, Inspectieformulier.InspectieFormulierTitel FROM  Account 
INNER JOIN Beschikbaarheid_inspecteurs
ON Account.AccountID = Beschikbaarheid_inspecteurs.MedewerkerID 
INNER JOIN Inspectieformulier
ON
Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie 
WHERE Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie
AND Inspectieformulier.InspectieFormulierTitel = @Event

            }
        }
    }
}
