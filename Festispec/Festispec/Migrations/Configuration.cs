namespace Festispec.Migrations
{
    using Festispec.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

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
            //SeedOfferte(context);
            //SeedInspectieFormulier(context);
            //SeedVraag(context);
            //SeedVraagMogelijkAntwoord(context);
            //SeedBeschikbaarheidInspecteurs(context);
            //SeedIngepladeInspecteurs(context);
            //SeedRapportTemplates(context);
            //SeedInspectionFormTemplates(context);
            //SeedVraagAntwoorden(context);
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
            Dictionary<string, string> statuses = new Dictionary<string, string>()
            {
                { "ia", "Inspectieformulier aangemaakt" },
                { "iu", "Inspectie uitgevoerd" },
                { "no", "Nieuwe opdracht" },
                { "oa", "Offerte geaccepteerd" },
                { "ob", "Opdracht beindingt" },
                { "og", "Offerte geweigerd" },
                { "ov", "Offerte verstuurt" },
                { "pg", "Opdracht geannuleerd" },
                { "ro", "Rapport opgesteld" },
                { "rv", "Rekening verstuurt" },
            };

            foreach (KeyValuePair<string, string> status in statuses)
                context.Status.AddOrUpdate(x => x.Betekenis, new Status() { Betekenis = status.Value });

            context.SaveChanges();
        }

        private void SeedQuestionType(FestispecContext context)
        {
            Dictionary<string, string> statuses = new Dictionary<string, string>()
            {
                { "av", "Afbeelding vraag" },
                { "mv", "Meerkeuze vraag" },
                { "ov", "Open vraag" },
                { "sv", "Schaal vraag" },
                { "tv", "Tabel vraag"},
                { "tx", "Pure text" }
            };

            foreach (KeyValuePair<string, string> status in statuses)
                context.VraagType.AddOrUpdate(x => x.Afkorting, new VraagType() { Afkorting = status.Key, Beschrijving = status.Value });

            context.SaveChanges();
        }

        private void SeedAccount(FestispecContext context)
        {
            Account[] accounts = new Account[7];

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
                IBAN = "NLRABO913644750",
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

            accounts[3] = new Account()
            {
                Gebruikersnaam = "FreddyJohnson",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Freddy",
                Tussenvoegsel = null,
                Achternaam = "Johnson",
                Stad = "Hilversum",
                Straatnaam = "Albertus Perkstraat",
                Huisnummer = "65",
                Email = "FJohnson@hotmail.nl",
                Telefoonnummer = "0604621581",
                DatumCertificering = DateTime.Now.AddMonths(-5),
                EinddatumCertificering = DateTime.Now.AddMonths(6),
                IBAN = "NLRABO91385350",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[4] = new Account()
            {
                Gebruikersnaam = "StijnSmulders",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Stijn",
                Tussenvoegsel = null,
                Achternaam = "Smulders",
                Stad = "'s-Hertogenbosch",
                Straatnaam = "Onderwijsboulevard",
                Huisnummer = "215",
                Email = "ssmulder@avans.nl",
                Telefoonnummer = "049563121",
                DatumCertificering = DateTime.Now.AddDays(-90),
                EinddatumCertificering = DateTime.Now.AddYears(1),
                IBAN = "NLRABO8863164",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[5] = new Account()
            {
                Gebruikersnaam = "AdrianaPitts",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Adriana",
                Tussenvoegsel = null,
                Achternaam = "Pitts",
                Stad = "Zuidwolde",
                Straatnaam = "Wethouder Klunderstraat",
                Huisnummer = "13",
                Email = "Pitts@ziggo.com",
                Telefoonnummer = "0657554555",
                DatumCertificering = DateTime.Now.AddDays(-10),
                EinddatumCertificering = DateTime.Now.AddYears(1).AddMonths(1),
                IBAN = "NLRABO8877103",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[6] = new Account()
            {
                Gebruikersnaam = "HarvieDeBakker",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Harvy",
                Tussenvoegsel = "de",
                Achternaam = "Bakker",
                Stad = "Emmeloord",
                Straatnaam = "Ijsselmeerlaan",
                Huisnummer = "125",
                Email = "HarveyDBakker@live.com",
                Telefoonnummer = "0494725015",
                DatumCertificering = DateTime.Now.AddMonths(-7).AddDays(13),
                EinddatumCertificering = DateTime.Now.AddYears(2).AddMonths(-6),
                IBAN = "NLRABO2442986",
                LaatsteWijziging = DateTime.Now,
            };

            context.Account.AddOrUpdate(x => x.Gebruikersnaam, accounts);

            context.SaveChanges();
        }

        private void SeedKlant(FestispecContext context)
        {
            Klant[] klanten = new Klant[5];
            klanten[0] = new Klant()
            {
                KvKNummer = "29387132",
                Vestigingnummer = 1,
                Naam = "Bospop",
                Stad = "s-Hertogenbosch",
                Straatnaam = "Van Voornestraat",
                Huisnummer = "30",
                Telefoonnummer = "0495678123",
                Email = "bospop@hotmail.com",
                Website = "www.bospop.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[1] = new Klant()
            {
                KvKNummer = "12345678",
                Vestigingnummer = 3216,
                Naam = "Pinkpop",
                Stad = "Utrecht",
                Straatnaam = "Amsterdamweg",
                Huisnummer = "23",
                Telefoonnummer = "0900616172",
                Email = "info@pinkpop.nl",
                Website = "www.pinkpop.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[2] = new Klant()
            {
                KvKNummer = "03810391",
                Vestigingnummer = 12,
                Naam = "Appelpop",
                Stad = "Tilburg",
                Straatnaam = "Wolvenweg",
                Huisnummer = "10",
                Telefoonnummer = "0495204233",
                Email = "business@appelpop.nl",
                Website = "www.appelpop.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[3] = new Klant()
            {
                KvKNummer = "29183110",
                Vestigingnummer = 3,
                Naam = "American day",
                Stad = "Amsterdam",
                Straatnaam = "Noordhollandstraat",
                Huisnummer = "2A",
                Telefoonnummer = "0495129102",
                Email = "AmericanDay@gmail.com",
                Website = "www.americanDay.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[4] = new Klant()
            {
                KvKNummer = "29387132",
                Vestigingnummer = 2,
                Naam = "Bospop",
                Stad = "Weert",
                Straatnaam = "Pylsstraatje",
                Huisnummer = "2",
                Telefoonnummer = "0621235663",
                Email = "bospop@hotmail.com",
                Website = "www.bospop.nl",
                LaatsteWijziging = DateTime.Now
            };

            context.Klant.AddOrUpdate(x => new { x.KvKNummer, x.Vestigingnummer }, klanten);

            context.SaveChanges();
        }

        private void SeedContactpersoon(FestispecContext context)
        {
            Contactpersoon[] contacts = new Contactpersoon[5];

            contacts[0] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Bospop").KlantID,
                Voornaam = "Dave",
                Tussenvoegsel = null,
                Achternaam = "Davidson",
                Email = "DaveDavidson@gmail.com",
                Telefoon = "0611111111",
                Notities = "Klaagt veel.",
                LaatsteWijziging = DateTime.Now
            };

            contacts[1] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Appelpop").KlantID,
                Voornaam = "Maarten",
                Tussenvoegsel = "Den",
                Achternaam = "Boer",
                Email = "Maarten@DenBoer.nl",
                Telefoon = "0698123572",
                Notities = "Laatste contact in Januari",
                LaatsteWijziging = DateTime.Now
            };

            contacts[2] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Pinkpop").KlantID,
                Voornaam = "Micheal",
                Tussenvoegsel = "de",
                Achternaam = "Vries",
                Email = "mdevries@pinkpop.nl",
                Rol = "Sales manager",
                Telefoon = "3137328131",
                Notities = "Eerste contact in Maart 2016, Extra contract omtrent factuur, Koffie afspraak verzet",
                LaatsteWijziging = DateTime.Now
            };

            contacts[3] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Pinkpop").KlantID,
                Voornaam = "Peter",
                Tussenvoegsel = null,
                Achternaam = "Smulders",
                Email = "psmulde@pinkpop.nl",
                Rol = "Quality manager",
                Telefoon = "31695734859",
                Notities = "Rapportage besproken",
                LaatsteWijziging = DateTime.Now
            };

            contacts[4] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == 2).KlantID,
                Voornaam = "Randy",
                Tussenvoegsel = null,
                Achternaam = "Marsh",
                Email = "rmarsh@bospop.nl",
                Telefoon = "0622935323",
                Rol = "Catering manager",
                Notities = "Offerte besproken",
                LaatsteWijziging = DateTime.Now
            };


            context.Contactpersoon.AddOrUpdate(x => x.Email, contacts);

            context.SaveChanges();
        }

        private void SeedOpdracht(FestispecContext context)
        {
            Opdracht[] opdrachten = new Opdracht[2];

            opdrachten[0] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Bospop",
                Status = context.Status.First(x => x.Betekenis == "Rekening verstuurt").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now,
                EindDatum = DateTime.Now.AddDays(2),
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == 2).KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Grondigeinspectievandecateringtenten"
            };

            opdrachten[1] = new Opdracht()
             {
                 OpdrachtNaam = "Inspectie Pinkpop",
                 Status = context.Status.First(x => x.Betekenis == "Inspectieformulier aangemaakt").Betekenis,
                 CreatieDatum = DateTime.Now,
                 StartDatum = DateTime.Now.AddMonths(1),
                 EindDatum = DateTime.Now.AddMonths(1).AddDays(3),
                 KlantID = context.Klant.First(x => x.Naam == "Pinkpop" && x.Vestigingnummer == 3216).KlantID,
                 MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                 GebruikteRechtsgebieden = "Drank en horeca wet",
                 LaatsteWijziging = DateTime.Now,
                 Klantwensen = "Totale inspectie van het festival"
             };

            context.Opdracht.AddOrUpdate(x => x.OpdrachtNaam, opdrachten);
                context.SaveChanges();

        }
    


        private void SeedOfferte(FestispecContext context)
        {
            Offerte[] offertes = new Offerte[3];

            offertes[0] = new Offerte()
            {
                OpdrachtID = context.Opdracht.First(x => x.OpdrachtNaam == "Inspectie Bospop").OpdrachtID,
                Totaalbedrag = (decimal)2000.50,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "We gaan een inspectie doen",
                KlantbeslissingReden = "ze vonden het goed",
                LaatsteWijziging = DateTime.Now
            };

            offertes[1] = new Offerte()
            {
                OpdrachtID = context.Opdracht.First(x => x.OpdrachtNaam == "Pinkpop inspectie").OpdrachtID,
                Totaalbedrag = (decimal)2530,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Inspectie horeca gelegenheden",
                KlantbeslissingReden = "afgewezen, de prijs was te hoog",
                LaatsteWijziging = DateTime.Now
            };

            offertes[2] = new Offerte()
            {
                OpdrachtID = context.Opdracht.First(x => x.OpdrachtNaam == "Pinkpop inspectie").OpdrachtID,
                Totaalbedrag = (decimal)1500.75,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Inspectie horeca gelegenheden",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            context.Offerte.AddOrUpdate(x => x.OpdrachtID, offertes);
            context.SaveChanges();
        }

        private void SeedInspectieFormulier(FestispecContext context)
        {
            Inspectieformulier[] formulieren = new Inspectieformulier[4];

            formulieren[0] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Bospop algemeen",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 0/*context.Opdracht.First(x => x.OpdrachtNaam == "Bospop inspectie").OpdrachtID,*/,
                Beschrijving = "Vul dit formulier in voor Bospop inspectie",
                LaatsteWijziging = DateTime.Now
            };

            formulieren[1] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Bospop catering",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 0/*context.Opdracht.First(x => x.OpdrachtNaam == "Bospop inspectie").OpdrachtID,*/,
                Beschrijving = "Vul dit formulier in voor Bospop inspectie catering",
                LaatsteWijziging = DateTime.Now
            };

            formulieren[2] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Pinkpop muziek",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 0/*context.Opdracht.First(x => x.OpdrachtNaam == "Pinkpop inspectie").OpdrachtID,*/,
                Beschrijving = "Vul dit formulier in voor Pinkpop inspectie muziek",
                LaatsteWijziging = DateTime.Now
            };

            formulieren[3] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Pinkpop sanitair",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 0/*context.Opdracht.First(x => x.OpdrachtNaam == "Pinkpop inspectie").OpdrachtID,*/,
                Beschrijving = "Vul dit formulier in voor Pinkpop inspectie sanitair",
                LaatsteWijziging = DateTime.Now
            };

            context.Inspectieformulier.AddOrUpdate(x => x.InspectieFormulierTitel, formulieren);
            context.SaveChanges();
        }

        private void SeedVraag(FestispecContext context)
        {
            Vraag[] vragen = new Vraag[17];
            vragen[0] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Bij binnenkomst, welk onderdeel van het festival valt het meest op.",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 1
                
            };

            vragen[1] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Hoe is de sfeer op het festival",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "mv").Afkorting,
                VolgordeNummer = 2

            };

            vragen[2] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Wat is de sfeer bij de eetgelegenheden",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "sv").Afkorting,
                VolgordeNummer = 3

            };

            vragen[3] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Maak een foto van het drukste gedeelte van het festival om 12:00",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 4

            };

            vragen[4] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Hoe zou jij het het typen mensen die rondlopen op het festival beschrijven?",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 5

            };

            vragen[5] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Op schaal van 1 tot 10 wat was de sfeer bij de eetgelegenheden. *1- er is niemand, 5*- Het is redelijk druk, *10- er is geen doorkomen aan",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "sv").Afkorting,
                VolgordeNummer = 1

            };

            vragen[6] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Geef een indruk van de sfeer impressie bij de eetgelegenheden",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 2

            };

            vragen[7] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Maak een foto van opvallende situaties",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 3

            };

            vragen[8] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Meet de afstand tussen de verschillende food trucks",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 4

            };

            vragen[9] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Teken de algemene stroom van mensen op een kaart, maak hier een foto van",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 5

            };

            vragen[10] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Tel het aantal tafels en zitplaatsen bij de foodtrucks",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 6

            };

            vragen[11] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Wat beschrijft het beste de sfeer bij het publiek na de shows bij de main stage? A: De sfeer is grimmig, B: Het publiek is rustig , C: Het publiek is dronken/aangeschoten, D: Het is chaos",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 7

            };

            vragen[12] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Geef het aantal mensen in de rij 5 minuten voor het begin van de elke theater show.",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 8

            };

            vragen[13] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Hoe druk was het bij de WC’s? Maak elk uur een schatting: * (Gebruik je inschattingstechniek geleerd op de training dag)",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 9

            };

            vragen[14] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Hoe druk is het bij de Main Stage? Maak elk half uur een schatting",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 10

            };

            vragen[15] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Hoe druk was het bij de foodtrucks? Maak elk uur een schatting",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 11

            };

            vragen[16] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").InspectieformulierID,
                Vraagstelling = "Plaats hier nog eventuele losse opmerkingen",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 12

            };

            context.Vraag.AddOrUpdate(vragen);
            context.SaveChanges();
        }

        private void SeedVraagMogelijkAntwoord(FestispecContext context)
        {
            VraagMogelijkAntwoord[] antwoorden = new VraagMogelijkAntwoord[35];
            antwoorden[0] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe is de sfeer op het festival").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "Grimmig"
            };

            antwoorden[1] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe is de sfeer op het festival").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "Rustig"
            };

            antwoorden[2] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe is de sfeer op het festival").VraagID,
                AntwoordNummer = 3,
                AntwoordText = "Gezellig"
            };

            antwoorden[3] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe is de sfeer op het festival").VraagID,
                AntwoordNummer = 4,
                AntwoordText = "Druk"
            };

            for (int i = 0; i < 10; i++)
            {
                antwoorden[i + 4] = new VraagMogelijkAntwoord()
                {
                    VraagID = context.Vraag.First(x => x.Vraagstelling == "Wat is de sfeer bij de eetgelegenheden").VraagID,
                    AntwoordNummer = i + 1,
                    AntwoordText = (i + 1).ToString()
                };
            }

            for (int i = 0; i < 10; i++)
            {
                antwoorden[i + 14] = new VraagMogelijkAntwoord()
                {
                    VraagID = context.Vraag.First(x => x.Vraagstelling == "Op schaal van 1 tot 10 wat was de sfeer bij de eetgelegenheden. *1- er is niemand, 5*- Het is redelijk druk, *10- er is geen doorkomen aan").VraagID,
                    AntwoordNummer = i + 1,
                    AntwoordText = (i + 1).ToString()
                };
            }

            antwoorden[25] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Wat beschrijft het beste de sfeer bij het publiek na de shows bij de main stage? A: De sfeer is grimmig, B: Het publiek is rustig , C: Het publiek is dronken/aangeschoten, D: Het is chaos").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "act"
            };

            antwoorden[26] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Wat beschrijft het beste de sfeer bij het publiek na de shows bij de main stage? A: De sfeer is grimmig, B: Het publiek is rustig , C: Het publiek is dronken/aangeschoten, D: Het is chaos").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "sfeer"
            };

            antwoorden[27] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Geef het aantal mensen in de rij 5 minuten voor het begin van de elke theater show.").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "Show"
            };

            antwoorden[28] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Geef het aantal mensen in de rij 5 minuten voor het begin van de elke theater show.").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "Aantal mensen in rij"
            };

            antwoorden[29] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk was het bij de WC’s? Maak elk uur een schatting: * (Gebruik je inschattingstechniek geleerd op de training dag)").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[30] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk was het bij de WC’s? Maak elk uur een schatting: * (Gebruik je inschattingstechniek geleerd op de training dag)").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            antwoorden[31] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk is het bij de Main Stage? Maak elk half uur een schatting").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[32] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk is het bij de Main Stage? Maak elk half uur een schatting").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            antwoorden[33] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk was het bij de foodtrucks? Maak elk uur een schatting").VraagID,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[34] = new VraagMogelijkAntwoord()
            {
                VraagID = context.Vraag.First(x => x.Vraagstelling == "Hoe druk was het bij de foodtrucks? Maak elk uur een schatting").VraagID,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            context.VraagMogelijkAntwoord.AddOrUpdate(x => x.AntwoordNummer, antwoorden);
            context.SaveChanges();
        }

        private void SeedVraagAntwoorden(FestispecContext context)
        {
            Inspectieformulier inspectieformulier = context.Inspectieformulier.Where(x => x.InspectieFormulierTitel == "Inspectie Bospop festival").FirstOrDefault();

            foreach(Vraag vraag in inspectieformulier.Vraag)
            {
                Antwoorden[] antwoorden = new Antwoorden[]
                {
                    new Antwoorden()
                    {
                        VraagID = vraag.VraagID,
                        InspecteurID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                        AntwoordNummer = 1,
                        AntwoordText = "0"
                    },
                    new Antwoorden()
                    {
                        VraagID = vraag.VraagID,
                        InspecteurID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                        AntwoordNummer = 1,
                        AntwoordText = "0"
                    }
                };

                context.Antwoorden.AddOrUpdate(x => new { x.VraagID, x.AntwoordNummer }, antwoorden);
                context.SaveChanges();
            }
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
            Inspectieformulier Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop festival");

            if(Inspectieformulier.Ingepland.Count == 0)
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HansKlok"));

            Account account = context.Account.First(x => x.Gebruikersnaam == "HansKlok");

            if(account.Ingepland.Count == 0)
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop festival"));

            context.SaveChanges();
        }

        private void SeedRapportTemplates(FestispecContext context)
        {
            RapportTemplate[] templates = new RapportTemplate[1];

            templates[0] = new RapportTemplate()
            {
                TemplateName = "Basis",
                TemplateText = "<html><head><title></title></head><body><p><font size=\"6\">Opdrachtnaam <strong>rapportage ...-...-...</strong></font></p><p align=\"right\"><img style=\"HEIGHT: 64px; WIDTH: 64px; POSITION: absolute; LEFT: 589px; TOP: 30px\" src=\"http://imagizer.imageshack.com/img924/5389/UoQcuY.png\" width=\"31\" height=\"23\"></p><p><strong><font size=\"4\">Klant gegevens</font></strong></p><p><strong>....</strong></p><p>&nbsp;</p><p><strong><font size=\"4\">Opdrachtgegevens</font></strong></p><p><strong>...</strong></p><p>&nbsp;</p><p>- Introductie</p><p>&nbsp;</p><p>- Omschrijving</p><p>&nbsp;</p><p>- Advies</p><p>&nbsp;</p><p>- Resultaten Inspectie</p></body></html>"
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
