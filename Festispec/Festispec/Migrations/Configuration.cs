namespace Festispec.Migrations
{
    using Festispec.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FestispecContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FestispecContext context)
        {
            SeedRoles(context);
            SeedStatus(context);
            SeedQuestionType(context);
            SeedAccount(context);
            SeedKlant(context);
            SeedContactpersoon(context);
            SeedOpdracht(context);
            SeedOfferte(context);
            SeedInspectieFormulier(context);
            SeedVraag(context);
            SeedVraagMogelijkAntwoord(context);
            SeedBeschikbaarheidInspecteurs(context);
            SeedIngepladeInspecteurs(context);
            SeedRapportTemplates(context);
            SeedInspectionFormTemplates(context);
        }

        private void SeedRoles(FestispecContext context)
        {
            Dictionary<string, string> roles = new Dictionary<string, string>()
            {
                { "ad", "Admin" },
                { "in", "Inspecteur" },
                { "ma", "Management" },
                { "om", "Operationeelmedewerker" },
                { "sm", "Salesmedewerker" },
            };

            foreach (KeyValuePair<string, string> role in roles)
                context.Rol.AddOrUpdate(x => x.Afkorting, new Rol() { Afkorting = role.Key, Betekenis = role.Value });

            context.SaveChanges();
        }

        private void SeedStatus(FestispecContext context)
        {
            List<string> statuses = new List<string>()
            {
                { "Inspectieformulier aangemaakt" },
                { "Inspectie uitgevoerd" },
                { "Nieuwe opdracht" },
                { "Offerte geaccepteerd" },
                { "Opdracht beindingt" },
                { "Offerte geweigerd" },
                { "Offerte verstuurt" },
                { "Opdracht geannuleerd" },
                { "Rapport opgesteld" },
                { "Rekening verstuurt" },
            };

            foreach (string status in statuses)
                context.Status.AddOrUpdate(x => x.Betekenis, new Status() { Betekenis = status });

            context.SaveChanges();
        }

        private void SeedQuestionType(FestispecContext context)
        {
            Dictionary<string, string> statuses = new Dictionary<string, string>()
            {
                { "av", "Afbeelding vraag" },
                { "lv", "Lijst vraag" },
                { "mv", "Meerkeuze vraag" },
                { "ov", "Open vraag" },
                { "sv", "Schaal vraag" },
            };

            foreach (KeyValuePair<string, string> status in statuses)
                context.VraagType.AddOrUpdate(x => x.Afkorting, new VraagType() { Afkorting = status.Key, Beschrijving = status.Value });

            context.SaveChanges();
        }

        private void SeedAccount(FestispecContext context)
        {
            Account[] accounts = new Account[3];

            accounts[0] = new Account()
            {
                Gebruikersnaam = "HansKlok",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Hans",
                Tussenvoegsel = null,
                Achternaam = "Klok",
                Stad = "Amsterdam",
                Straatnaam = "Dom H. van der Laanstraat",
                Huisnummer = "10",
                Email = "HansKlok@gmail.be",
                Telefoonnummer = "0495555555",
                DatumCertificering = DateTime.Now,
                EinddatumCertificering = DateTime.Now,
                IBAN = "RABO001",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[1] = new Account()
            {
                Gebruikersnaam = "FransDeWanks",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "om").Afkorting,
                Voornaam = "Frans",
                Tussenvoegsel = "de",
                Achternaam = "Wanks",
                Stad = "Amsterdam",
                Straatnaam = "David Vosstraat",
                Huisnummer = "29",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[2] = new Account()
            {
                Gebruikersnaam = "JohnWong",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "sm").Afkorting,
                Voornaam = "John",
                Tussenvoegsel = null,
                Achternaam = "Wong",
                Stad = "Tilburg",
                Straatnaam = "Nimrodstraat",
                Huisnummer = "19",
                LaatsteWijziging = DateTime.Now,
            };

            context.Account.AddOrUpdate(x => x.Gebruikersnaam, accounts);

            context.SaveChanges();
        }

        private void SeedKlant(FestispecContext context)
        {
            Klant[] klanten = new Klant[1];
            klanten[0] = new Klant()
            {
                KvKNummer = "293871",
                Naam = "Bospop",
                Stad = "s-Hertogenbosch",
                Straatnaam = "Van Voornestraat",
                Huisnummer = "30",
                Telefoonnummer = "0495678123",
                Email = "bospop@hotmail.com",
                Website = "www.bospop.nl",
                LaatsteWijziging = DateTime.Now
            };

            context.Klant.AddOrUpdate(x => x.KvKNummer, klanten);

            context.SaveChanges();
        }

        private void SeedContactpersoon(FestispecContext context)
        {
            Contactpersoon[] contacts = new Contactpersoon[1];

            contacts[0] = new Contactpersoon()
            {
                KlantID = "293871",
                Voornaam = "Dave",
                Tussenvoegsel = null,
                Achternaam = "Davidson",
                Email = "DaveDavidson@gmail.com",
                Telefoon = "0611111111",
                Notities = "Klaagt veel.",
                LaatsteWijziging = DateTime.Now
            };

            context.Contactpersoon.AddOrUpdate(x => x.Email, contacts);

            context.SaveChanges();
        }

        private void SeedOpdracht(FestispecContext context)
        {
            Opdracht[] opdrachten = new Opdracht[1];

            opdrachten[0] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Bospop",
                Status = context.Status.First(x => x.Betekenis == "Rekening verstuurt").Betekenis,
                CreatieDatum = DateTime.Now,
                KlantID = "293871",
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Wensen"
            };

            context.Opdracht.AddOrUpdate(x => x.OpdrachtNaam, opdrachten);
            context.SaveChanges();
        }

        private void SeedOfferte(FestispecContext context)
        {
            Offerte[] offertes = new Offerte[1];

            offertes[0] = new Offerte()
            {
                OpdrachtID = context.Opdracht.First(x => x.OpdrachtNaam == "Inspectie Bospop").OpdrachtID,
                Totaalbedrag = (decimal)2000.50,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "We gaan een inspectie doen",
                KlantbeslissingReden = "ze vonden het goed",
                LaatsteWijziging = DateTime.Now
            };

            context.Offerte.AddOrUpdate(x => x.OpdrachtID, offertes);
            context.SaveChanges();
        }

        private void SeedInspectieFormulier(FestispecContext context)
        {
            Inspectieformulier[] formulieren = new Inspectieformulier[1];

            formulieren[0] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Bospop festival",
                DatumInspectie = DateTime.Now,
                OpdrachtID = context.Opdracht.First(x => x.OpdrachtNaam == "Inspectie Bospop").OpdrachtID,
                Beschrijving = "Vul dit formulier in voor Bospop inspectie",
                LaatsteWijziging = DateTime.Now
            };

            context.Inspectieformulier.AddOrUpdate(x => x.InspectieFormulierTitel, formulieren);
            context.SaveChanges();
        }

        private void SeedVraag(FestispecContext context)
        {
            Vraag[] vragen = new Vraag[2];
            vragen[0] = new Vraag()
            {
                Vraagstelling = "Open vraag",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                LaatsteWijziging = DateTime.Now
            };

            vragen[1] = new Vraag()
            {
                Vraagstelling = "Meerkeuze vraag",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "mv").Afkorting,
                LaatsteWijziging = DateTime.Now
            };

            context.Vraag.AddOrUpdate(vragen);
            context.SaveChanges();
        }

        private void SeedVraagMogelijkAntwoord(FestispecContext context)
        {
            VraagMogelijkAntwoord[] antwoorden = new VraagMogelijkAntwoord[3];
            antwoorden[0] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagtype == "mv").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "a"
            };

            antwoorden[1] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagtype == "mv").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "b"
            };

            antwoorden[2] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagtype == "mv").VraagID,
                AntwoordNummer = 3,
                AntwoordText = "c"
            };

            context.VraagMogelijkAntwoord.AddOrUpdate(x => x.AntwoordNummer, antwoorden);
            context.SaveChanges();
        }

        private void SeedBeschikbaarheidInspecteurs(FestispecContext context)
        {
            if (context.BeschikbaarheidInspecteurs.Count() > 0)
                return;

            BeschikbaarheidInspecteurs[] beschikbaarheid = new BeschikbaarheidInspecteurs[4];
            beschikbaarheid[0] = new BeschikbaarheidInspecteurs()
            {
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                Datum = DateTime.Now
            };

            beschikbaarheid[1] = new BeschikbaarheidInspecteurs()
            {
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                Datum = DateTime.Now.AddDays(1)
            };

            beschikbaarheid[2] = new BeschikbaarheidInspecteurs()
            {
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                Datum = DateTime.Now.AddDays(2)
            };

            beschikbaarheid[3] = new BeschikbaarheidInspecteurs()
            {
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                Datum = DateTime.Now.AddDays(3)
            };

            context.BeschikbaarheidInspecteurs.AddOrUpdate(x => new { x.MedewerkerID, x.Datum}, beschikbaarheid);
            context.SaveChanges();
        }

        private void SeedIngepladeInspecteurs(FestispecContext context)
        {
            Opdracht opdracht = context.Opdracht.First(x => x.OpdrachtNaam == "Inspectie Bospop");

            if(opdracht.Ingepland.Count == 0)
                opdracht.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HansKlok"));

            Account account = context.Account.First(x => x.Gebruikersnaam == "HansKlok");

            if(account.Ingepland.Count == 0)
                account.Ingepland.Add(context.Opdracht.First(x => x.OpdrachtNaam == "Inspectie Bospop"));

            context.SaveChanges();
        }

        private void SeedRapportTemplates(FestispecContext context)
        {
            RapportTemplate[] templates = new RapportTemplate[1];

            templates[0] = new RapportTemplate()
            {
                TemplateName = "Test",
                TemplateText = "prGbEM6flQ2YUckUEgO2Pdh4y9J8gRUbSEQw0boZCoIjgNhxoNGFVPQA7AzDUZowDkSLJ93WGHeeUKHZ1AKexT1a3wRjN5ONbhuExU8uig46QCW1UyzHwquDYu6fe6mwq8rnhiHFUXS21pOusA8OKm14p8asoFqyqdtGyLhTDtq8oENLP5Kazl6mjkgafspjfUFkjQYhortW23THikIuEm6DOesvRya6oki4VVLQDzDMTy3qaetESgV5n7IRR6SpScusPlPJG6kDUNiNJT4qxWFVK1wWhRDHXRjiMW9RP2VBjYJkbr7dDxpCq2gU6kKfrTMt5v4n4Lil2x6vsikTXwYyPeMO3HJUepBkUXEVLhthgee0v5L1gIl5yMCb2MRq4yVNzw35ZuAa0FXN"
            };

            context.RapportTemplate.AddOrUpdate(x => x.TemplateID, templates);
            context.SaveChanges();
        }

        private void SeedInspectionFormTemplates(FestispecContext context)
        {
            Inspectieformulier[] templates = new Inspectieformulier[1];

            templates[0] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Test",
                Beschrijving = "prGbEM6flQ2YUckUEgO2Pdh4y9J8gRUbSEQw0boZCoIjgNhxoNGFVPQA7AzDUZowDkSLJ93WGHeeUKHZ1AKexT1a3wRjN5ONbhuExU8uig46QCW1UyzHwquDYu6fe6mwq8rnhiHFUXS21pOusA8OKm14p8asoFqyqdtGyLhTDtq8oENLP5Kazl6mjkgafspjfUFkjQYhortW23THikIuEm6DOesvRya6oki4VVLQDzDMTy3qaetESgV5n7IRR6SpScusPlPJG6kDUNiNJT4qxWFVK1wWhRDHXRjiMW9RP2VBjYJkbr7dDxpCq2gU6kKfrTMt5v4n4Lil2x6vsikTXwYyPeMO3HJUepBkUXEVLhthgee0v5L1gIl5yMCb2MRq4yVNzw35ZuAa0FXN",
                DatumInspectie = DateTime.Now,
                LaatsteWijziging = DateTime.Now
            };

            context.Inspectieformulier.AddOrUpdate(x => x.InspectieformulierID, templates);
            context.SaveChanges();
        }
    }
}
