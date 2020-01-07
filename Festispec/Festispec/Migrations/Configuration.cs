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
            SeedOfferte(context);
            SeedInspectieFormulier(context);
            SeedVraag(context);
            SeedVraagMogelijkAntwoord(context);
            SeedBeschikbaarheidInspecteurs(context);
            SeedIngepladeInspecteurs(context);
            SeedRapportTemplates(context);
            SeedInspectionFormTemplates(context);
            SeedVraagAntwoorden(context);
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

            SaveChanges(context);
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

            SaveChanges(context);
        }

        private void SeedQuestionType(FestispecContext context)
        {
            Dictionary<string, string> statuses = new Dictionary<string, string>()
            {
                { "av", "Afbeelding vraag" },
                { "mv", "Meerkeuze vraag" },
                { "ov", "Open vraag" },
                { "sv", "Schaal vraag" },
                { "tv", "Tabel vraag"  },
                { "tx", "Pure text" }
            };

            foreach (KeyValuePair<string, string> status in statuses)
                context.VraagType.AddOrUpdate(x => x.Afkorting, new VraagType() { Afkorting = status.Key, Beschrijving = status.Value });

            SaveChanges(context);
        }

        private void SeedAccount(FestispecContext context)
        {
            Account[] accounts = new Account[10];

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
                Voornaam = "Harvie",
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

            accounts[7] = new Account()
            {
                Gebruikersnaam = "MargotRowland",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Margot",
                Tussenvoegsel = null,
                Achternaam = "Rowland",
                Stad = "Valkenswaard",
                Straatnaam = "Waalresweg",
                Huisnummer = "70",
                Email = "MRowland@gmail.nl",
                Telefoonnummer = "0494723100",
                DatumCertificering = DateTime.Now.AddMonths(-8).AddDays(3),
                EinddatumCertificering = DateTime.Now.AddYears(1),
                IBAN = "NLRABO2331224",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[8] = new Account()
            {
                Gebruikersnaam = "SanjeevPike",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Sanjeev",
                Tussenvoegsel = null,
                Achternaam = "Pike",
                Stad = "Dordrecht",
                Straatnaam = "Poelwijckstraat",
                Huisnummer = "3",
                Email = "S.Pike@hotmail.com",
                Telefoonnummer = "0648781812",
                DatumCertificering = DateTime.Now.AddMonths(-13).AddDays(3),
                EinddatumCertificering = DateTime.Now.AddMonths(4),
                IBAN = "NLRABO1123941",
                LaatsteWijziging = DateTime.Now,
            };

            accounts[9] = new Account()
            {
                Gebruikersnaam = "WillCollier",
                Wachtwoord = "ab123",
                Rol = context.Rol.First(x => x.Afkorting == "in").Afkorting,
                Voornaam = "Will",
                Tussenvoegsel = null,
                Achternaam = "Collier",
                Stad = "Amsterdam",
                Straatnaam = "Jan Puntstraat",
                Huisnummer = "18",
                Email = "willcollier@hotmail.nl",
                Telefoonnummer = "0690099889",
                DatumCertificering = DateTime.Now.AddMonths(-23).AddDays(3),
                EinddatumCertificering = DateTime.Now.AddMonths(2),
                IBAN = "NLRABO0098228",
                LaatsteWijziging = DateTime.Now,
            };

            context.Account.AddOrUpdate(x => x.Gebruikersnaam, accounts);

            SaveChanges(context);
        }

        private void SeedKlant(FestispecContext context)
        {
            Klant[] klanten = new Klant[8];
            klanten[0] = new Klant()
            {
                KvKNummer = "29387132",
                Vestigingnummer = "01156459",
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
                Vestigingnummer = "3216",
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
                Vestigingnummer = "12",
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
                Vestigingnummer = "3",
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
                Vestigingnummer = "2",
                Naam = "Bospop",
                Stad = "Weert",
                Straatnaam = "Pylsstraatje",
                Huisnummer = "2",
                Telefoonnummer = "0621235663",
                Email = "bospop@hotmail.com",
                Website = "www.bospop.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[5] = new Klant()
            {
                KvKNummer = "11250321",
                Vestigingnummer = "897573",
                Naam = "Electric Jungle",
                Stad = "Groningen",
                Straatnaam = "Regentesstraat",
                Huisnummer = "128",
                Telefoonnummer = "0495034831",
                Email = "electricjungle@hotmail.com",
                Website = "www.electricjungle.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[6] = new Klant()
            {
                KvKNummer = "88937800",
                Vestigingnummer = "8961531",
                Naam = "Beerland",
                Stad = "Hengelo",
                Straatnaam = "Beckumerstraat",
                Huisnummer = "16",
                Telefoonnummer = "0495724951",
                Email = "beerland@hotmail.com",
                Website = "www.beerland.nl",
                LaatsteWijziging = DateTime.Now
            };

            klanten[7] = new Klant()
            {
                KvKNummer = "15344454",
                Vestigingnummer = "985156",
                Naam = "Skalar",
                Stad = "Amsterdam",
                Straatnaam = "Lederambachtstraat",
                Huisnummer = "4",
                Telefoonnummer = "0495898485",
                Email = "skalar@hotmail.com",
                Website = "www.skalar.nl",
                LaatsteWijziging = DateTime.Now
            };
            context.Klant.AddOrUpdate(x => new { x.KvKNummer, x.Vestigingnummer }, klanten);

            SaveChanges(context);
        }

        private void SeedContactpersoon(FestispecContext context)
        {
            Contactpersoon[] contacts = new Contactpersoon[8];

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
                Telefoon = "3169573485",
                Notities = "Rapportage besproken",
                LaatsteWijziging = DateTime.Now
            };

            contacts[4] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == "2").KlantID,
                Voornaam = "Randy",
                Tussenvoegsel = null,
                Achternaam = "Marsh",
                Email = "rmarsh@bospop.nl",
                Telefoon = "0622935323",
                Rol = "Catering manager",
                Notities = "Offerte besproken",
                LaatsteWijziging = DateTime.Now
            };

            contacts[5] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Electric Jungle").KlantID,
                Voornaam = "Daniele",
                Tussenvoegsel = null,
                Achternaam = "Camacho",
                Email = "dcamacho@hotmail.com",
                Telefoon = "0622938763",
                Rol = "Social relationships manager",
                Notities = null,
                LaatsteWijziging = DateTime.Now
            };

            contacts[6] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Electric Jungle").KlantID,
                Voornaam = "Nida",
                Tussenvoegsel = null,
                Achternaam = "Pena",
                Email = "nidapena@gmail.nl",
                Telefoon = "0629353213",
                Rol = "security manager",
                Notities = null,
                LaatsteWijziging = DateTime.Now
            };

            contacts[7] = new Contactpersoon()
            {
                KlantID = context.Klant.First(x => x.Naam == "Electric Jungle").KlantID,
                Voornaam = "Lili",
                Tussenvoegsel = null,
                Achternaam = "Goff",
                Email = "lili.goff@hotmail.com",
                Telefoon = "0688455421",
                Rol = "Financieel manager",
                Notities = null,
                LaatsteWijziging = DateTime.Now
            };

            context.Contactpersoon.AddOrUpdate(x => x.Email, contacts);

            SaveChanges(context);
        }

        private void SeedOpdracht(FestispecContext context)
        {
            Opdracht[] opdrachten = new Opdracht[10];

            opdrachten[0] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Bospop",
                Status = context.Status.First(x => x.Betekenis == "Rekening verstuurt").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now,
                EindDatum = DateTime.Now.AddDays(2),
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == "2").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Grondige inspectie van de cateringtenten"
            };

            opdrachten[1] = new Opdracht()
             {
                 OpdrachtNaam = "Inspectie Pinkpop",
                 Status = context.Status.First(x => x.Betekenis == "Inspectieformulier aangemaakt").Betekenis,
                 CreatieDatum = DateTime.Now,
                 StartDatum = DateTime.Now.AddMonths(1),
                 EindDatum = DateTime.Now.AddMonths(1).AddDays(3),
                 KlantID = context.Klant.First(x => x.Naam == "Pinkpop" && x.Vestigingnummer == "3216").KlantID,
                 MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                 GebruikteRechtsgebieden = "Drank en horeca wet",
                 LaatsteWijziging = DateTime.Now,
                 Klantwensen = "Totale inspectie van het festival"
             };

            opdrachten[2] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Appelpop",
                Status = context.Status.First(x => x.Betekenis == "Offerte geaccepteerd").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(2),
                EindDatum = DateTime.Now.AddMonths(3).AddDays(3),
                KlantID = context.Klant.First(x => x.Naam == "Appelpop" && x.Vestigingnummer == "12").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Extra focus op de kwaliteit van de catering."
            };

            opdrachten[3] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie American Day",
                Status = context.Status.First(x => x.Betekenis == "Opdracht geannuleerd").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(1).AddDays(-7),
                EindDatum = DateTime.Now.AddMonths(1).AddDays(-4),
                KlantID = context.Klant.First(x => x.Naam == "American day" && x.Vestigingnummer == "3").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Zou graag een gedetailleerd rapport van de drukte willen"
            };

            opdrachten[4] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Bospop s-Hertogenbosch 2020",
                Status = context.Status.First(x => x.Betekenis == "Offerte geaccepteerd").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(3).AddDays(-7),
                EindDatum = DateTime.Now.AddMonths(3).AddDays(-4),
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == "01156459").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "De inspectie moet zo snel mogelijk worden uitgevoerd"
            };

            opdrachten[5] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Bospop s-Hertogenbosch 2021",
                Status = context.Status.First(x => x.Betekenis == "Nieuwe opdracht").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(3).AddDays(-7).AddYears(1),
                EindDatum = DateTime.Now.AddMonths(3).AddDays(-4).AddYears(1),
                KlantID = context.Klant.First(x => x.Naam == "Bospop" && x.Vestigingnummer == "01156459").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Jaarlijkse herhaling van de inpsectie"
            };

            opdrachten[6] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Beerland 2019",
                Status = context.Status.First(x => x.Betekenis == "Opdracht beindingt").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(-3).AddDays(-7),
                EindDatum = DateTime.Now.AddMonths(-3).AddDays(-4),
                KlantID = context.Klant.First(x => x.Naam == "Beerland" && x.Vestigingnummer == "8961531").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "De klant wil weten hoe de sfeer is in de verschillende gebieden van het festival"
            };

            opdrachten[7] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Skalar 2020",
                Status = context.Status.First(x => x.Betekenis == "Offerte verstuurt").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(8).AddDays(7),
                EindDatum = DateTime.Now.AddMonths(8).AddDays(11),
                KlantID = context.Klant.First(x => x.Naam == "Skalar" && x.Vestigingnummer == "985156").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "De klant wil weten waarom het bezoekersaantal minderd"
            };

            opdrachten[8] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Electric jungle voorjaar 2020",
                Status = context.Status.First(x => x.Betekenis == "Offerte verstuurt").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(2).AddDays(12),
                EindDatum = DateTime.Now.AddMonths(2).AddDays(15),
                KlantID = context.Klant.First(x => x.Naam == "Electric Jungle" && x.Vestigingnummer == "897573").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Klant wil weten hoe de bezoekers het festival ervaren"
            };

            opdrachten[9] = new Opdracht()
            {
                OpdrachtNaam = "Inspectie Appelpop najaar 2019",
                Status = context.Status.First(x => x.Betekenis == "Inspectie uitgevoerd").Betekenis,
                CreatieDatum = DateTime.Now,
                StartDatum = DateTime.Now.AddMonths(-1).AddDays(12),
                EindDatum = DateTime.Now.AddMonths(-1).AddDays(15),
                KlantID = context.Klant.First(x => x.Naam == "Appelpop" && x.Vestigingnummer == "12").KlantID,
                MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                GebruikteRechtsgebieden = null,
                LaatsteWijziging = DateTime.Now,
                Klantwensen = "Wil een beter beeld krijgen van wat bezoekers echt leuk vinden."
            };

            context.Opdracht.AddOrUpdate(x => x.OpdrachtNaam, opdrachten);

            SaveChanges(context);

        }
    


        private void SeedOfferte(FestispecContext context)
        {
            Offerte[] offertes = new Offerte[9];

            offertes[0]=new Offerte()
            {
                OpdrachtID = 1,
                Totaalbedrag = 2000,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "We gaan een inspectie doen",
                KlantbeslissingReden = "ze vonden het goed",
                LaatsteWijziging = DateTime.Now
            };

            offertes[1]=new Offerte()
            {
                OpdrachtID = 2,
                Totaalbedrag = 2530,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Inspectie horeca gelegenheden",
                KlantbeslissingReden = "afgewezen, de prijs was te hoog",
                LaatsteWijziging = DateTime.Now
            };

            offertes[2]=new Offerte()
            {
                OpdrachtID = 2,
                Totaalbedrag = 1500,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Inspectie horeca gelegenheden, aangepast nadat de vorige te hoog was",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[3] = new Offerte()
            {
                OpdrachtID = 3,
                Totaalbedrag = 1230,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "De inspectie zal bestaan uit 6 inspecteurs, deze zullen samen 2 inspectieformulieren invullen.",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[4] = new Offerte()
            {
                OpdrachtID = 5,
                Totaalbedrag = 2050,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "De details over de inspectie zijn doorgesproken met het hoofd management van Bospop",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[5] = new Offerte()
            {
                OpdrachtID = 7,
                Totaalbedrag = 2950,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Eerste schatting van kosten voor de inspectie",
                KlantbeslissingReden = "Afgekeurd, te duur.",
                LaatsteWijziging = DateTime.Now
            };

            offertes[6] = new Offerte()
            {
                OpdrachtID = 7,
                Totaalbedrag = 1860,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Aangepast nadat de vorige te duur was",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[7] = new Offerte()
            {
                OpdrachtID = 8,
                Totaalbedrag = 1985,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "De prijs viel hoger uit aangezien er meer inspecteurs dan verwacht nodig waren",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[8] = new Offerte()
            {
                OpdrachtID = 9,
                Totaalbedrag = 1890,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Deze offerte is goedkoper uitgevallen dan afgesproken wegens weizigingen in de wetgevingen die de inspectie makkelijker maken.",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            offertes[8] = new Offerte()
            {
                OpdrachtID = 10,
                Totaalbedrag = 1540,
                Aanmaakdatum = DateTime.Now,
                Beschrijving = "Kosten zijn wat hoger aangezien deze inspectie in korte tijd voorbereid moet worden",
                KlantbeslissingReden = "Goedgekeurd",
                LaatsteWijziging = DateTime.Now
            };

            context.Offerte.AddOrUpdate(x => new { x.OpdrachtID, x.Totaalbedrag }, offertes);

            SaveChanges(context);
        }

        private void SeedInspectieFormulier(FestispecContext context)
        {
            Inspectieformulier[] formulieren = new Inspectieformulier[6];

            formulieren[0] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Bospop algemeen",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 1,
                Beschrijving = "Vul dit formulier in voor Bospop inspectie",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(14),
                EindTijd = TimeSpan.FromHours(23),
                Stad = "Weert",
                Straatnaam = "Pylsstraatje",
                Huisnummer = "2"
            };

            formulieren[1] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Bospop catering",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 1,
                Beschrijving = "Vul dit formulier in voor Bospop inspectie catering",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(10),
                EindTijd = TimeSpan.FromHours(20),
                Stad = "Weert",
                Straatnaam = "Pylsstraatje",
                Huisnummer = "2"
            };

            formulieren[2] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Pinkpop muziek",
                DatumInspectie = DateTime.Now,
                OpdrachtID = 2,
                Beschrijving = "Vul dit formulier in voor Pinkpop inspectie muziek",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(16),
                EindTijd = TimeSpan.FromHours(19),
                Stad = "Utrecht",
                Straatnaam = "Amsterdamweg",
                Huisnummer = "23"
            };

            formulieren[3] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Pinkpop sanitair",
                DatumInspectie = DateTime.Now.AddDays(1).AddMonths(1),
                OpdrachtID = 2,
                Beschrijving = "Vul dit formulier in voor Pinkpop inspectie sanitair",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(12),
                EindTijd = TimeSpan.FromHours(17),
                Stad = "Utrecht",
                Straatnaam = "Amsterdamweg",
                Huisnummer = "23"
            };

            formulieren[4] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Beerland 2019 sfeer",
                DatumInspectie = DateTime.Now.AddMonths(-1).AddDays(-20),
                OpdrachtID = 7,
                Beschrijving = "Vul dit formulier in voor de Beerland inspectie, vergeet niet om rustig te drinken voordat dit compleet is ingevuld.",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(19),
                EindTijd = TimeSpan.FromHours(23),
                Stad = "Hengelo",
                Straatnaam = "Beckumerstraat",
                Huisnummer = "16"
            };

            formulieren[5] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Inspectie Appelpop najaar 2019",
                DatumInspectie = DateTime.Now.AddDays(-24),
                OpdrachtID = 10,
                Beschrijving = "Vul dit formulier in voor de Appelpop najaar inspectie",
                LaatsteWijziging = DateTime.Now,
                StartTijd = TimeSpan.FromHours(14),
                EindTijd = TimeSpan.FromHours(23),
                Stad = "Tilburg",
                Straatnaam = "Wolvenweg",
                Huisnummer = "10"
            };

            context.Inspectieformulier.AddOrUpdate(x => x.InspectieFormulierTitel, formulieren);
            SaveChanges(context);
        }

        private void SeedVraag(FestispecContext context)
        {
            Vraag[] vragen = new Vraag[29];
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
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Maak een foto van opvallende situaties",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 3

            };

            vragen[8] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Meet de afstand tussen de verschillende food trucks",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 4

            };

            vragen[9] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Teken de algemene stroom van mensen op een kaart, maak hier een foto van",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 5

            };

            vragen[10] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Tel het aantal tafels en zitplaatsen bij de foodtrucks",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 6

            };

            vragen[11] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Wat beschrijft het beste de sfeer bij het publiek na de shows bij de main stage? A: De sfeer is grimmig, B: Het publiek is rustig , C: Het publiek is dronken/aangeschoten, D: Het is chaos",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 7

            };

            vragen[12] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Geef het aantal mensen in de rij 5 minuten voor het begin van de elke theater show.",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 8

            };

            vragen[13] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Hoe druk was het bij de WC’s? Maak elk uur een schatting: * (Gebruik je inschattingstechniek geleerd op de training dag)",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 9

            };

            vragen[14] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Hoe druk is het bij de Main Stage? Maak elk half uur een schatting",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 10

            };

            vragen[15] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Hoe druk was het bij de foodtrucks? Maak elk uur een schatting",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "tv").Afkorting,
                VolgordeNummer = 11

            };

            vragen[16] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek").InspectieformulierID,
                Vraagstelling = "Plaats hier nog eventuele losse opmerkingen",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 12

            };

            vragen[17] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Bij binnenkomst hoe zou je de sfeer beschrijven",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 1

            };

            vragen[18] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Wat is de sfeer rond de eettenten",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 2

            };

            vragen[19] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Rond welke tijd kun je zeggen dat het meerendeel van de bezoekers onder invloed zijn",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 3

            };

            vragen[20] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Wat voor effect heeft het onder invloed zijn van bezoekers op de sfeer",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 4

            };

            vragen[21] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Hoe is de sfeer laat op de avond vergeleken met op het begin",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 5

            };

            vragen[22] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer").InspectieformulierID,
                Vraagstelling = "Plaats hier nog eventuele losse opmerkingen",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 6

            };

            vragen[23] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Waar lijken de meeste bezoekers zich te verzamelen?",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 1

            };

            vragen[24] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Vermaken de bezoekers zich ook op de delen van het festival waar geen optredens plaatsvinden, bijvoorbeeld rond de eettenten?",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 2

            };

            vragen[25] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Hoe druk bezocht zijn de veschillende optredens? Gebruik je schattings technieken.",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "sv").Afkorting,
                VolgordeNummer = 3

            };

            vragen[26] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Is er iets waarover de bezoekers veel klagen?",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 4

            };

            vragen[27] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Maak een afbeelding van het gebied waar de meeste bezoekers staan.",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "av").Afkorting,
                VolgordeNummer = 5

            };

            vragen[28] = new Vraag()
            {
                InspectieFormulierID = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019").InspectieformulierID,
                Vraagstelling = "Plaats hier nog eventuele opmerkingen",
                Vraagtype = context.VraagType.First(x => x.Afkorting == "ov").Afkorting,
                VolgordeNummer = 6

            };

            context.Vraag.AddOrUpdate(x => new { x.InspectieFormulierID, x.VolgordeNummer }, vragen);
            SaveChanges(context);
        }

        private void SeedVraagMogelijkAntwoord(FestispecContext context)
        {
           VraagMogelijkAntwoord[] antwoorden = new VraagMogelijkAntwoord[36];
            antwoorden[0] = new VraagMogelijkAntwoord()
            {
                VraagID = 2,
                AntwoordNummer = 1,
                AntwoordText = "Grimmig"
            };

            antwoorden[1] = new VraagMogelijkAntwoord()
            {
                VraagID = 2,
                AntwoordNummer = 2,
                AntwoordText = "Rustig"
            };

            antwoorden[2] = new VraagMogelijkAntwoord()
            {
                VraagID = 2,
                AntwoordNummer = 3,
                AntwoordText = "Gezellig"
            };

            antwoorden[3] = new VraagMogelijkAntwoord()
            {
                VraagID = 2,
                AntwoordNummer = 4,
                AntwoordText = "Druk"
            };

            for (int i = 0; i < 10; i++)
            {
                antwoorden[i + 4] = new VraagMogelijkAntwoord()
                {
                    VraagID = 3,
                    AntwoordNummer = i + 1,
                    AntwoordText = (i + 1).ToString()
                };
            }

            for (int i = 0; i < 10; i++)
            {
                antwoorden[i + 14] = new VraagMogelijkAntwoord()
                {
                    VraagID = 6,
                    AntwoordNummer = i + 1,
                    AntwoordText = (i + 1).ToString()
                };
            }

            antwoorden[24] = new VraagMogelijkAntwoord()
            {
                VraagID = 12,
                AntwoordNummer = 1,
                AntwoordText = "act"
            };

            antwoorden[25] = new VraagMogelijkAntwoord()
            {
                VraagID = 12,
                AntwoordNummer = 2,
                AntwoordText = "sfeer"
            };

            antwoorden[26] = new VraagMogelijkAntwoord()
            {
                VraagID = 13,
                AntwoordNummer = 1,
                AntwoordText = "Show"
            };

            antwoorden[27] = new VraagMogelijkAntwoord()
            {
                VraagID = 13,
                AntwoordNummer = 2,
                AntwoordText = "Aantal mensen in rij"
            };

            antwoorden[28] = new VraagMogelijkAntwoord()
            {
                VraagID = 14,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[29] = new VraagMogelijkAntwoord()
            {
                VraagID = 14,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            antwoorden[30] = new VraagMogelijkAntwoord()
            {
                VraagID = 15,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[31] = new VraagMogelijkAntwoord()
            {
                VraagID = 15,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            antwoorden[32] = new VraagMogelijkAntwoord()
            {
                VraagID = 16,
                AntwoordNummer = 1,
                AntwoordText = "Tijd"
            };

            antwoorden[33] = new VraagMogelijkAntwoord()
            {
                VraagID = 16,
                AntwoordNummer = 2,
                AntwoordText = "Drukte"
            };

            antwoorden[34] = new VraagMogelijkAntwoord()
            {
                VraagID = 26,
                AntwoordNummer = 1,
                AntwoordText = "Artiest"
            };

            antwoorden[35] = new VraagMogelijkAntwoord()
            {
                VraagID = 26,
                AntwoordNummer = 2,
                AntwoordText = "Geschatte drukte"
            };

            context.VraagMogelijkAntwoord.AddOrUpdate(x => new { x.AntwoordNummer, x.VraagID }, antwoorden);
            SaveChanges(context);
        }

        private void SeedVraagAntwoorden(FestispecContext context)
        {
           Inspectieformulier inspectieformulier = context.Inspectieformulier.Where(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen").FirstOrDefault();


            Antwoorden[] antwoorden = new Antwoorden[2];


            antwoorden[0] = new Antwoorden {
                VraagID = 1,
                InspecteurID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                AntwoordNummer = 1,
                AntwoordText = "0"
            };
            antwoorden[1] = new Antwoorden{
                VraagID = 2,
                InspecteurID = context.Account.First(x => x.Gebruikersnaam == "FransDeWanks").AccountID,
                AntwoordNummer = 1,
                AntwoordText = "0"
            };
                

            context.Antwoorden.AddOrUpdate(x => new { x.VraagID, x.AntwoordNummer }, antwoorden);
            SaveChanges(context);
            
        }

        private void SeedBeschikbaarheidInspecteurs(FestispecContext context)
        {
            if (context.BeschikbaarheidInspecteurs.Count() > 0)
                return;

            BeschikbaarheidInspecteurs[] beschikbaarheid = new BeschikbaarheidInspecteurs[800];

            for (int i = 0; i < 100; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HansKlok").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 100; i < 200; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "FreddyJohnson").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 200; i < 300; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "StijnSmulders").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 300; i < 400; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "AdrianaPitts").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 400; i < 500; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "HarvieDeBakker").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 500; i < 600; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "MargotRowland").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 600; i < 700; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "SanjeevPike").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            for (int i = 700; i < 800; i++)
            {
                beschikbaarheid[i] = new BeschikbaarheidInspecteurs()
                {
                    MedewerkerID = context.Account.First(x => x.Gebruikersnaam == "WillCollier").AccountID,
                    Datum = DateTime.Now.AddDays(i)
                };
            }

            context.BeschikbaarheidInspecteurs.AddOrUpdate(x => new { x.MedewerkerID, x.Datum}, beschikbaarheid);
            SaveChanges(context);
        }

        private void SeedIngepladeInspecteurs(FestispecContext context)
        {
            Inspectieformulier Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen");

            if(Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HansKlok"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "FreddyJohnson"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "StijnSmulders"));
            }

            Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop catering");

            if (Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "AdrianaPitts"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HarvieDeBakker"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "MargotRowland"));
            }

            Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek");

            if (Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "WillCollier"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HarvieDeBakker"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HansKlok"));
            }

            Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop sanitair");

            if (Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "AdrianaPitts"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "SanjeevPike"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "MargotRowland"));
            }

            Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer");

            if (Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "JohnWong"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "WillCollier"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "FreddyJohnson"));
            }

            Inspectieformulier = context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019");

            if (Inspectieformulier.Ingepland.Count == 0)
            {
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "HarvieDeBakker"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "SanjeevPike"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "JohnWong"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "MargotRowland"));
                Inspectieformulier.Ingepland.Add(context.Account.First(x => x.Gebruikersnaam == "StijnSmulders"));
            }

            Account account = context.Account.First(x => x.Gebruikersnaam == "HansKlok");

            if(account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "FreddyJohnson");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "JohnWong");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "StijnSmulders");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "AdrianaPitts");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop catering"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop sanitair"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "MargotRowland");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop catering"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop sanitair"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "HarvieDeBakker");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop catering"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "SanjeevPike");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Bospop algemeen"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop sanitair"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Appelpop najaar 2019"));
            }

            account = context.Account.First(x => x.Gebruikersnaam == "WillCollier");

            if (account.Ingepland.Count == 0)
            {
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Pinkpop muziek"));
                account.Ingepland.Add(context.Inspectieformulier.First(x => x.InspectieFormulierTitel == "Inspectie Beerland 2019 sfeer"));
            }


            SaveChanges(context);
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
            SaveChanges(context);
        }

        private void SeedInspectionFormTemplates(FestispecContext context)
        {
            Inspectieformulier[] templates = new Inspectieformulier[2];

            templates[0] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Leeg",
                Beschrijving = "Leeg inspectieformulier",
                InspectieFormulierTitel = "Leeg formulier",
                Beschrijving = "Nieuw leeg formulier",
                DatumInspectie = DateTime.Now,
                LaatsteWijziging = DateTime.Now
            };

            templates[1] = new Inspectieformulier()
            {
                InspectieFormulierTitel = "Test",
                Beschrijving = "prGbEM6flQ2YUckUEgO2Pdh4y9J8gRUbSEQw0boZCoIjgNhxoNGFVPQA7AzDUZowDkSLJ93WGHeeUKHZ1AKexT1a3wRjN5ONbhuExU8uig46QCW1UyzHwquDYu6fe6mwq8rnhiHFUXS21pOusA8OKm14p8asoFqyqdtGyLhTDtq8oENLP5Kazl6mjkgafspjfUFkjQYhortW23THikIuEm6DOesvRya6oki4VVLQDzDMTy3qaetESgV5n7IRR6SpScusPlPJG6kDUNiNJT4qxWFVK1wWhRDHXRjiMW9RP2VBjYJkbr7dDxpCq2gU6kKfrTMt5v4n4Lil2x6vsikTXwYyPeMO3HJUepBkUXEVLhthgee0v5L1gIl5yMCb2MRq4yVNzw35ZuAa0FXN",
                DatumInspectie = DateTime.Now,
                LaatsteWijziging = DateTime.Now
            };

            context.Inspectieformulier.AddOrUpdate(x => x.InspectieFormulierTitel, templates);
            SaveChanges(context);
        }

        private void SaveChanges(FestispecContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
