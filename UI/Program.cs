using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;
using Model.Services;
using Model.Repositories;
using System.Text;

namespace UI
{
    internal class Program
    {
        private static readonly bool DarkMode = true;
        private static GemeenteBoekContext context = new GemeenteBoekContext();
        private static readonly GemeenteBoekService Service = new GemeenteBoekService(context);
        // . . . . 
        private static Persoon Account;
        private static string LoginGegevens => $"{(Account == null ? "Niet ingelogd" : (Account is Profiel ? "PROFIEL: " : "MEDEWERKER: ") + "Nr: " + Account.PersoonId + " - Naam: " + Account.LoginNaam)}";

        private static void Main(string[] args)
        {
            Console.BackgroundColor = DarkMode ? ConsoleColor.Black : ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("========================");
            Console.WriteLine("G E M E E N T E B O E K ");
            Console.WriteLine("========================");

            KiesHoofdmenu();

            Console.WriteLine("\nWij danken u voor uw medewerking. Tot de volgend keer....");
            Console.ReadKey();
        }

        public static void KiesHoofdmenu()
        {
            char? keuze = null;

            while (keuze != 'X')
            {
                string input;

                if (Account == null)
                    input = "AX";
                else    // Profiel
                    if (Account is Profiel)
                    input = "AXNR";
                else    // Medewerker
                    input = "AXGBD";

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine($"=================");
                Console.WriteLine($"H O O F D M E N U - {LoginGegevens}");
                Console.WriteLine($"=================");
                Console.WriteLine("<A>ccount");

                if (Account is Medewerker)
                {
                    Console.WriteLine("<G>oedkeuring nieuw profiel");
                    Console.WriteLine("<B>lokkeren van een profiel");
                    Console.WriteLine("<D>eblokkeren van een profiel");
                }

                if (Account is Profiel)
                {
                    Console.WriteLine("<N>ieuw bericht");
                    Console.WriteLine($"<R>aadplegen berichten van uw hoofdgemeente " +
                        $"{(Account.Adres.Straat.Gemeente.HoofdGemeente == null ? Account.Adres.Straat.Gemeente.GemeenteNaam : Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam)}");
                }

                Console.WriteLine("e<X>it");
                Console.WriteLine();

                keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }

                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Black;

                switch (keuze)
                {
                    case 'A':
                        KiesAccountMenu();
                        break;

                    case 'B':
                        BlokkerenProfiel();
                        break;

                    case 'D':
                        DeblokkerenProfiel();
                        break;

                    case 'G':
                        GoedkeurenNieuwProfiel();
                        break;

                    case 'N':
                        InvoerenNieuwBericht();
                        break;

                    case 'R':
                        RaadplegenBerichten();
                        break;
                }
            }
        }

        public static void KiesAccountMenu()
        {
            string input;

            char? keuze = null;

            while (keuze != 'X')
            {
                if (Account == null)
                    input = "IRX";
                else
                    if (Account is Profiel)
                    input = "UTWVX";
                else
                    input = "UTX";

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine($"===================");
                Console.WriteLine($"A C C O U N T M E N U - {LoginGegevens}");
                Console.WriteLine($"===================");

                if (Account == null)
                {
                    Console.WriteLine("<I>nloggen");
                    Console.WriteLine("<R>egistreren");
                }
                else
                {
                    Console.WriteLine("<U>itloggen");
                    Console.WriteLine("<T>oon profielgegevens");

                    if (Account is Profiel)
                    {
                        Console.WriteLine("<W>ijzig profielgegevens");
                        Console.WriteLine("<V>erwijder profiel");
                    }
                }

                Console.WriteLine("e<X>it");
                Console.WriteLine();

                keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }

                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Black;

                switch (keuze)
                {
                    case 'I':
                        Inloggen();
                        break;

                    case 'U':
                        Uitloggen();
                        break;

                    case 'R':
                        Registeren();
                        break;

                    case 'T':
                        ToonGegegevens(Account);
                        break;

                    case 'W':
                        WijzigGegevens();
                        break;

                    case 'V':
                        VerwijderGegevens(Account);
                        break;
                }
            }
        }

        public static void GoedkeurenNieuwProfiel()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Goedkeuring Profiel");
            Console.WriteLine("-------------------");

            string naam = " ";
            Profiel profiel = null;

            while (naam != string.Empty & profiel == null)
            {
                Console.Write("Geef de naam van het profiel dat moet goedgekeurd worden <Enter>=Terug: ");
                naam = Console.ReadLine();

                profiel = Service.GetProfielByName(naam);

                if (profiel == null)
                    Console.WriteLine("Profiel niet gevonden.  Probeer opnieuw...");
                else
                {
                    Service.GoedkeurenProfiel(profiel);
                    Console.WriteLine("Profiel is goedgekeurd...");
                }
            }
        }

        public static void BlokkerenProfiel()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Blokkeren van een Profiel");
            Console.WriteLine("-------------------------");
            string naam = " ";
            Profiel profiel = null;

            while (naam != string.Empty & profiel == null)
            {
                Console.Write("Geef de naam van het profiel dat moet geblokkeerd worden <Enter>=Terug: ");
                naam = Console.ReadLine();

                profiel = Service.GetProfielByName(naam);

                if (profiel == null)
                    Console.WriteLine("Het profiel niet gevonden.  Probeer opnieuw...");
                else
                {
                    Service.BlokkerenProfiel(profiel, true);
                    Console.WriteLine("Profiel werd geblokkeerd...");
                }
            }
        }

        public static void DeblokkerenProfiel()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Deblokkeren van een Profiel");
            Console.WriteLine("---------------------------");

            string naam = " ";
            Profiel profiel = null;

            while (naam != string.Empty & profiel == null)
            {
                Console.Write("Geef de naam van het profiel dat moet geblokkeerd worden <Enter>=Terug: ");
                naam = Console.ReadLine();

                profiel = Service.GetProfielByName(naam);

                if (profiel == null)
                    Console.WriteLine("Het profiel werd niet gevonden.  Probeer opnieuw...");
                else
                {
                    Service.BlokkerenProfiel(profiel, false);
                    Console.WriteLine("Profiel werd gedeblokkeerd...");
                }
            }
        }

        public static void Inloggen()
        {
            if (!(Account is null))
            {
                ConsoleHelper.ToonFoutBoodschap("Persoon is reeds ingelogd. Log eerst uit aub.");
                return;
            }

            var gebruikersnaam = "-";
            while (gebruikersnaam != string.Empty)
            {
                Console.WriteLine();
                Console.WriteLine("===============");
                Console.WriteLine("I N L O G G E N");
                Console.WriteLine("===============");
                Console.Write("Gebruikersnaam <Enter>=Terug: ");
                gebruikersnaam = Console.ReadLine();
                while ((gebruikersnaam != string.Empty) & (Account is null))
                {
                    Console.Write(" Wachtwoord: ");
                    var wachtwoord = Console.ReadLine();
                    Account = Service.GetAccount(gebruikersnaam, wachtwoord);
                    if (Account is null)
                    {
                        ConsoleHelper.ToonFoutBoodschap("Profiel werd niet gevonden. Probeer opnieuw.");
                        Console.Write("Gebruikersnaam <Enter>=Terug: ");
                        gebruikersnaam = Console.ReadLine();
                    }
                    else
                    {
                        if (Account is Profiel)
                        {
                            if (!Service.IsGoedgekeurdProfiel((Profiel)Account))
                            {
                                Console.WriteLine("Uw profiel werd nog niet goedgekeurd.");
                                return;
                            }

                            if (Service.IsGeblokkeerdProfiel((Profiel)Account))
                            {
                                ConsoleHelper.ToonFoutBoodschap("Uw profiel werd geblokkeerd...");
                                return;
                            }
                        }
                        Console.WriteLine("Inloggen met succes voltooid.");
                        return;
                    }
                }
            }
            return;
        }

        public static void Uitloggen()
        {
            if (Account is null)
                ConsoleHelper.ToonFoutBoodschap("Account is niet ingelogd.");
            Service.WijzigProfiel();
            Account = null;
        }

        public static Persoon Registeren()
        {
            Profiel profiel = null;
            var voornaam = "-";
            while (voornaam != string.Empty)
            {
                Console.WriteLine();
                Console.WriteLine("=====================");
                Console.WriteLine("R E G I S T R E R E N");
                Console.WriteLine("=====================");
                Console.WriteLine();
                voornaam = ConsoleHelper.LeesString("Voornaam (<Enter>=Terug)", 20, OptionMode.Optional);
                while (voornaam != string.Empty)
                {
                    // Input
                    var familienaam = ConsoleHelper.LeesString("Familienaam* ", 25, OptionMode.Mandatory);
                    DateTime? geboortedatum = ConsoleHelper.LeesDatum("Geboortedatum (DD/MM/JJJJ)", new DateTime(1900, 1, 1), DateTime.Now, OptionMode.Mandatory);
                    var telefoonnummer = ConsoleHelper.LeesTelefoonNummer("Telefoonnummer", OptionMode.Optional);
                    var kennismakingstekst = ConsoleHelper.LeesString("Kennismaking Tekst* ", 225, OptionMode.Mandatory);
                    var emailadres = ConsoleHelper.LeesEmailAdres("EmailAdres* ", OptionMode.Mandatory);
                    var beroep = ConsoleHelper.LeesString("Beroep ", 30, OptionMode.Optional);
                    var firmaNaam = ConsoleHelper.LeesString("Firma ", 25, OptionMode.Optional);
                    var facebooknaam = ConsoleHelper.LeesString(" FacebookNaam ", 30, OptionMode.Optional);
                    var website = ConsoleHelper.LeesWebsiteUrl("Website URL ", OptionMode.Optional);
                    var geslachtString = ConsoleHelper.LeesKeuzeUitLijst("Geslacht", new List<object>() { "M", "V" }, OptionMode.Mandatory);
                    DateTime? woonthiersinds = ConsoleHelper.LeesDatum("woont hier sinds (DD/MM/JJJJ) ", new DateTime(1900, 1, 1), DateTime.Now, OptionMode.Mandatory);

                    // Taal
                    Console.WriteLine("\n--> Ingave Taal");
                    var taal = KiesTaal("Kies taal: ", OptionMode.Mandatory);

                    // Geboorteplaats
                    Console.WriteLine("\n--> Ingave Geboorteplaats");
                    var geboortePlaats = KiesGemeente("Geboorteplaats", OptionMode.Optional);

                    // Adres
                    Console.WriteLine("\n--> Ingave Adres");
                    var adres = KiesAdres("Woonplaats ", OptionMode.Mandatory);

                    // Login
                    Console.WriteLine("\n-->Ingave Login");
                    var ok = false;
                    string gebruikersnaam = "", wachtwoord = "", wachtwoordbevestiging = "";
                    while (!ok)
                    {
                        gebruikersnaam = ConsoleHelper.LeesString("Gebruikersnaam", 8, OptionMode.Mandatory);
                        wachtwoord = ConsoleHelper.LeesString("Wachtwoord", 32, OptionMode.Mandatory);
                        wachtwoordbevestiging = ConsoleHelper.LeesString("Wachtwoord bevestigen", 32, OptionMode.Mandatory);
                        if (wachtwoord != wachtwoordbevestiging)
                        {
                            ConsoleHelper.ToonFoutBoodschap("Wachtwoord en Wachtwoordbevestiging moeten gelijk zijn.");
                            continue;
                        }
                        if (Service.LoginBestaat(gebruikersnaam))
                        {
                            ConsoleHelper.ToonFoutBoodschap("Deze login is reeds in gebruik.");
                            continue;
                        }
                        ok = true;
                    }

                    // Interesses
                    Console.WriteLine("\n-->Ingave Interesses");
                    List<ProfielInteresse> profielInteresses = KiesProfielInteresses("Kies Interesses: ", OptionMode.Optional);

                    Console.WriteLine();


                    Console.WriteLine();
                    if (ok)
                    {
                        // Toevoegen Profiel
                        profiel = new Profiel()
                        {
                            VoorNaam = voornaam,
                            FamilieNaam = familienaam,
                            GeboorteDatum = (DateTime)geboortedatum,
                            GeboortePlaats = geboortePlaats,
                            Geslacht = (Geslacht)((string)(geslachtString)).ToCharArray()[0],
                            Taal = taal,
                            TelefoonNr = telefoonnummer,

                            LoginNaam = gebruikersnaam,
                            LoginPaswoord = wachtwoord,

                            LoginAantal = 0,
                            VerkeerdeLoginsAantal = 0,
                            WoontHierSindsDatum = (DateTime)woonthiersinds,
                            EmailAdres = emailadres,
                            FacebookNaam = facebooknaam,
                            WebsiteAdres = website,
                            BeroepTekst = beroep,

                            Geblokkeerd = false,
                            GoedgekeurdTijdstip = null,
                            CreatieTijdstip = DateTime.Now,
                            LaatsteUpdateTijdstip = DateTime.Now,
                            Adres = adres,
                            KennismakingTekst = kennismakingstekst,
                            ProfielInteresses = profielInteresses,
                        };

                        // Overzicht
                        ToonGegegevens(profiel);

                        // Bevestiging + Bewaren
                        if ((bool)ConsoleHelper.LeesBool("Bewaren OK ?", OptionMode.Mandatory))

                        {
                            Service.VoegProfielToe(profiel);
                            Console.WriteLine($"U werd toegevoegd als gebruiker (id: {profiel.PersoonId}).");
                        }
                        else
                            Console.WriteLine("U werd niet toegevoegd als gebruiker.");
                        voornaam = string.Empty; // Stop
                    }
                }
            }
            return profiel;
        }

        public static void ToonGegegevens(Persoon persoon)
        {
            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine("Overzicht");
            Console.WriteLine("---------");

            Console.WriteLine($" Naam: {persoon.VoorNaam} {persoon.FamilieNaam}");
            Console.WriteLine();
            Console.WriteLine($" Adres: {persoon.Adres.Straat.StraatNaam} {persoon.Adres.HuisNr}");
            Console.WriteLine($" {persoon.Adres.Straat.Gemeente.Postcode} {persoon.Adres.Straat.Gemeente.GemeenteNaam}");
            Console.WriteLine($" {persoon.Adres.Straat.Gemeente.Provincie.Provincienaam} ");
            Console.WriteLine();
            Console.WriteLine($" Geboortedatum: {persoon.GeboorteDatum}");
            if (persoon is Profiel)
            {
                Console.WriteLine($" Geboorteplaats: {persoon.GeboortePlaats}");
            }
            Console.WriteLine($" Geslacht: {persoon.Geslacht} ");
            Console.WriteLine();
            if (persoon is Profiel)
            {
                Console.WriteLine($" Taal: {persoon.Taal}");
            }
            Console.WriteLine($" Telefoonnummer: {persoon.TelefoonNr} ");
            Console.WriteLine();
            Console.WriteLine($" Login: {persoon.LoginNaam}/{persoon.LoginPaswoord}");
            Console.WriteLine($" Aantal keer ingelogd: {persoon.LoginAantal}");
            Console.WriteLine($" Aantal verkeerd: {persoon.VerkeerdeLoginsAantal}");
            Console.WriteLine();
            if (persoon is Profiel)
            {
                var account = (Profiel)persoon;
                Console.WriteLine($" Woont hier sinds: {account.WoontHierSindsDatum}");
                Console.WriteLine($" Emailadres: {account.EmailAdres}");
                Console.WriteLine($" Facebook: {account.FacebookNaam}");
                Console.WriteLine($" Website: {account.WebsiteAdres}");
                Console.WriteLine($" Beroep: {account.BeroepTekst}");
                Console.WriteLine($" Profiel goedgekeurd op: {account.GoedgekeurdTijdstip}");
                Console.Write($" Aangemaakt op: {account.CreatieTijdstip}");
                Console.Write($" Laatst Gewijzigd: {account.LaatsteUpdateTijdstip}");
            }
        }

        public static Taal KiesTaal(string titel, OptionMode optionMode)
        {
            var talen = Service.GetAllTalen();
            var taal = (Taal)ConsoleHelper.LeesLijst($"{titel}", talen, talen.Select(b => b.TaalCode + "\t" + b.TaalNaam).ToList(), SelectionMode.Single, optionMode).FirstOrDefault();
            if (taal != null) Console.WriteLine($"De gekozen taal is {taal.TaalNaam}");
            return taal;
        }

        public static Gemeente KiesGemeente(string titel, OptionMode optionMode)
        {
            Gemeente gemeente = null;
            while (gemeente == null)
            {
                Console.Write($"Geef een aantal letters in van de {titel}{(optionMode == OptionMode.Mandatory ? "" : " (*=Stop)")}: ");
                var gemeenteKeuzeString = Console.ReadLine();

                if (optionMode == OptionMode.Optional)
                    if (gemeenteKeuzeString.ToUpper() == "*") break;

                var gemeenten = Service.GetFilteredGemeenten(gemeenteKeuzeString);
                gemeente = (Gemeente)ConsoleHelper.LeesLijst($"{titel}{(optionMode == OptionMode.Mandatory ? "*" : "")}", gemeenten, gemeenten.Select(b => b.GemeenteNaam).ToList(), SelectionMode.Single, OptionMode.Optional).FirstOrDefault();
            }
            if (gemeente != null) Console.WriteLine($"De gekozen Gemeente is {gemeente.GemeenteNaam}");
            return gemeente;
        }

        public static Straat KiesStraat(string titel, Gemeente gemeente, OptionMode optionMode)
        {
            Straat straat = null;
            while (straat == null)
            {
                Console.Write($"Geef een aantal letters in van de {titel}{(optionMode == OptionMode.Mandatory ? "" : " (*=Stop)")}: ");
                var straatKeuzeString = Console.ReadLine();

                if (optionMode == OptionMode.Optional)
                    if (straatKeuzeString.ToUpper() == "*") break;

                var straten = Service.GetFilteredStraten(gemeente, straatKeuzeString);

                if (straten.Count() == 0)
                {
                    ConsoleHelper.ToonFoutBoodschap("Er zijn geen straten voor deze gemeente of selectie.");
                    return null;
                }
                straat = (Straat)ConsoleHelper.LeesLijst($"{titel}{(optionMode == OptionMode.Mandatory ? "*" : "")}", straten, straten.Select(b => b.StraatNaam).ToList(), SelectionMode.Single, OptionMode.Optional).FirstOrDefault();
            }

            if (straat != null) Console.WriteLine($"De gekozen Straat is {straat.StraatNaam}");
            return straat;
        }
        public static Adres KiesAdres(string titel, OptionMode optionMode)
        {
            Adres adres = null;
            Gemeente gemeente = null;
            Straat straat = null;

            while (gemeente == null || straat == null)
            {
                gemeente = KiesGemeente(titel, optionMode);

                if (gemeente == null) return null;  // Enkel mogelijk indien optional

                straat = null;

                while (straat == null)
                {
                    straat = KiesStraat("Straat", gemeente, optionMode);

                    if (straat == null) break;  // Naar ingave gemeente

                    Console.Write("                   HuisNummer: ");
                    var huisNummer = Console.ReadLine();

                    Console.Write("                    BusNummer: ");
                    var busNummer = Console.ReadLine();

                    adres = Service.GetAdres(straat, huisNummer, busNummer);

                    if (adres == null)
                    {
                        // Insert new Adres
                        adres = new Adres
                        {
                            Straat = straat,
                            HuisNr = huisNummer,
                            BusNr = busNummer
                        };
                    }
                }
            }
            return adres;
        }
        public static List<ProfielInteresse> KiesProfielInteresses(string titel, OptionMode optionMode)
        {
            var interesseSoorten = Service.GetAllInteresseSoorten();
            List<Object> interesseObjecten = ConsoleHelper.LeesLijst(titel, interesseSoorten, interesseSoorten.Select(b => b.InteresseSoortNaam).ToList(), SelectionMode.Multiple, optionMode);
            List<ProfielInteresse> profielInteresses = new List<ProfielInteresse>();

            foreach (var interesseObject in interesseObjecten)
            {
                var interesse = (InteresseSoort)interesseObject;
                var interesseTekst = ConsoleHelper.LeesString($"Tekst voor {interesse.InteresseSoortNaam}", 256, OptionMode.Optional);
                profielInteresses.Add(new ProfielInteresse { InteresseSoortId = interesse.InteresseSoortId, ProfielInteresseTekst = interesseTekst, InteresseSoort = interesse });
            }

            return profielInteresses;
        }

        public static void WijzigGegevens()
        {
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine("Wijzigen Profiel");
            Console.WriteLine("----------------");

            var profiel = (Profiel)Account.Clone();

            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine("Wijzigen Profiel");
            Console.WriteLine("----------------");

            string[] menu = { "Voornaam", "Familienaam", "Geboortedatum", "Telefoonnummer", "Kennismakingstekst", "Email", "Beroep", "Firma", "FacebookNaam", "Website", "Geslacht", "Woont hier sinds", "Taal", "Geboorteplaats", "Adres", "Passwoord", "Interesses" };
            int? seqKeuze = 0;
            bool gewijzigd = false;

            while (seqKeuze != null)
            {
                int seq = 0;
                foreach (var item in menu) Console.WriteLine(++seq + $".\t{item}");

                seqKeuze = ConsoleHelper.LeesInt("Geef het volgnummer uit de lijst", 1, seq, OptionMode.Optional);

                switch (seqKeuze)
                {
                    case 1:
                        Console.WriteLine($"{profiel.VoorNaam}");
                        profiel.VoorNaam = ConsoleHelper.LeesString("Voornaam", 20, OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 2:
                        Console.WriteLine($"{profiel.FamilieNaam}");
                        profiel.FamilieNaam = ConsoleHelper.LeesString("Familienaam", 25, OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 3:
                        Console.WriteLine($"{profiel.GeboorteDatum}");
                        profiel.GeboorteDatum = (DateTime)ConsoleHelper.LeesDatum("Geboortedatum (DD/MM/JJJJ)", new DateTime(1900, 1, 1), DateTime.Now, OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 4:
                        Console.WriteLine($"{profiel.TelefoonNr}");
                        profiel.TelefoonNr = ConsoleHelper.LeesTelefoonNummer("TelefoonNr", OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 5:
                        Console.WriteLine($"{profiel.KennismakingTekst}");
                        profiel.KennismakingTekst = ConsoleHelper.LeesString("Kennismaking Tekst", 256, OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 6:
                        Console.WriteLine($"{profiel.EmailAdres}");
                        profiel.EmailAdres = ConsoleHelper.LeesEmailAdres("EmailAdres", OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 7:
                        Console.WriteLine($"{profiel.BeroepTekst}");
                        profiel.BeroepTekst = ConsoleHelper.LeesString("Beroep", 32, OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 8:
                        Console.WriteLine($"{profiel.FirmaNaam}");
                        profiel.FirmaNaam = ConsoleHelper.LeesString("Firma", 32, OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 9:
                        Console.WriteLine($"{profiel.FacebookNaam}");
                        profiel.FacebookNaam = ConsoleHelper.LeesString("FacebookNaam", 32, OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 10:
                        Console.WriteLine($"{profiel.WebsiteAdres}");
                        profiel.WebsiteAdres = ConsoleHelper.LeesWebsiteUrl("Website URL", OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 11:
                        Console.WriteLine($"{profiel.Geslacht}");
                        profiel.Geslacht = (Geslacht)((string)(ConsoleHelper.LeesKeuzeUitLijst("Geslacht", new List<object>() { "M", "V" }, OptionMode.Mandatory))).ToCharArray()[0];
                        gewijzigd = true;
                        break;

                    case 12:
                        Console.WriteLine($"{profiel.WoontHierSindsDatum}");
                        profiel.WoontHierSindsDatum = ConsoleHelper.LeesDatum("Woont hier sinds (DD/MM/JJJJ)", new DateTime(1900, 1, 1), DateTime.Now, OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 13:
                        Console.WriteLine($"{profiel.Taal}");
                        profiel.Taal = KiesTaal("Kies taal: ", OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 14:
                        Console.WriteLine($"{profiel.GeboortePlaats}");
                        profiel.GeboortePlaats = KiesGemeente("Geboorteplaats: ", OptionMode.Optional);
                        gewijzigd = true;
                        break;

                    case 15:
                        Console.WriteLine($"{profiel.Adres}");
                        Console.WriteLine("\n--> Ingave Adres");
                        profiel.Adres = KiesAdres("Woonplaats ", OptionMode.Mandatory);
                        gewijzigd = true;
                        break;

                    case 16:
                        Console.WriteLine($"{profiel.LoginNaam}/{profiel.LoginPaswoord}");
                        Console.WriteLine("\n-->Wijzigen paswoord");

                        var ok = false;
                        string wachtWoord = "", wachtWoordBevestiging = "";

                        while (!ok)
                        {
                            wachtWoord = ConsoleHelper.LeesString("Wachtwoord", 32, OptionMode.Mandatory);
                            wachtWoordBevestiging = ConsoleHelper.LeesString("Wachtwoord bevestigen", 32, OptionMode.Mandatory);

                            if (wachtWoord != wachtWoordBevestiging)
                            {
                                ConsoleHelper.ToonFoutBoodschap("Wachtwoord en Wachtwoordbevestiging moeten gelijk zijn.");
                                continue;
                            }
                            ok = true;
                        }

                        profiel.LoginPaswoord = wachtWoord;

                        gewijzigd = true;
                        break;

                    case 17:
                        Console.WriteLine("\n-->Ingave Interesses");
                        foreach (var interesse in profiel.ProfielInteresses) Console.WriteLine($"\t{interesse.InteresseSoort.InteresseSoortNaam}\t{interesse.ProfielInteresseTekst}");
                        profiel.ProfielInteresses = KiesProfielInteresses("Kies Interesses: ", OptionMode.Optional);   //!!!!
                        gewijzigd = true;
                        break;
                }
            }

            if (gewijzigd)
            {
                // Bevestiging + Bewaren
                if ((bool)ConsoleHelper.LeesBool("Wijzigen OK ?", OptionMode.Mandatory))
                {
                    var persoon = (Profiel)Account;

                    persoon.VoorNaam = profiel.VoorNaam;
                    persoon.FamilieNaam = profiel.FamilieNaam;
                    persoon.GeboorteDatum = profiel.GeboorteDatum;
                    persoon.TelefoonNr = profiel.TelefoonNr;
                    persoon.KennismakingTekst = profiel.KennismakingTekst;
                    persoon.EmailAdres = profiel.EmailAdres;
                    persoon.BeroepTekst = profiel.BeroepTekst;
                    persoon.FirmaNaam = profiel.FirmaNaam;
                    persoon.FacebookNaam = profiel.FacebookNaam;
                    persoon.WebsiteAdres = profiel.WebsiteAdres;
                    persoon.Geslacht = profiel.Geslacht;
                    persoon.WoontHierSindsDatum = profiel.WoontHierSindsDatum;
                    persoon.Taal = profiel.Taal;
                    persoon.GeboortePlaats = profiel.GeboortePlaats;
                    persoon.Adres = profiel.Adres;
                    persoon.LoginPaswoord = profiel.LoginPaswoord;
                    persoon.ProfielInteresses = profiel.ProfielInteresses;

                    Service.WijzigProfiel();
                    Console.WriteLine($"Uw profiel werd werd gewijzigd.");
                }
                else
                    Console.WriteLine("Uw profiel werd niet gewijzigd.");
            }
        }

        public static void VerwijderGegevens(Persoon persoon)
        {
            if ((bool)ConsoleHelper.LeesBool("Bewaren OK ?", OptionMode.Mandatory))

            {
                context.Personen.Remove(persoon);
                context.SaveChanges();
                Console.WriteLine("U werd verwijdert als gebruiker.");
            }
            else
                Console.WriteLine("U werd niet verwijdert als gebruiker.");
        }

        // PART TWO
        public static void InvoerenNieuwBericht()
        {
            //var berichtTypes = Service.GetAllBerichtTypes();
            var berichtType = KiesBerichtType("Kies BerichtType: ", OptionMode.Mandatory);
            var berichtTitel = ConsoleHelper.LeesString("Titel Bericht", 32, OptionMode.Mandatory);
            var berichtTekst = ConsoleHelper.LeesString("Bericht", 512, OptionMode.Mandatory);

            // Bevestiging + Bewaren
            if ((bool)ConsoleHelper.LeesBool("Nieuw bericht toevoegen OK ?", OptionMode.Mandatory))
            {
                var bericht = new Bericht
                {
                    BerichtTitel = berichtTitel,
                    BerichtTekst = berichtTekst,
                    BerichtTijdstip = DateTime.Now,
                    BerichtType = berichtType,
                    Gemeente = Account.Adres.Straat.Gemeente,
                    HoofdBericht = null,
                    Profiel = (Profiel)Account,
                    Level = 1
                };
                //weergeven bericht
                Service.ToevoegenBericht(bericht);

                Console.WriteLine($"\nGemeente: {bericht.Profiel.Adres.Straat.Gemeente.GemeenteNaam}\nBerichtType: {bericht.BerichtType.BerichtTypeNaam}\nTitel: {bericht.BerichtTitel}\nTekst: {bericht.BerichtTekst}\nTijdstip: {bericht.BerichtTijdstip}\nProfiel: {bericht.Profiel.LoginNaam}");
                Console.WriteLine("Het bericht werd toegevoegd.");
            }
            else
                Console.WriteLine("Het bericht werd niet toegevoegd.");
        }

        public static void RaadplegenBerichten()
        {
            char? keuze = null;

            while (keuze != 'X')
            {
                Console.WriteLine();
                var bericht = KiesBericht($"Kies Berichten voor hoofdgemeente {(Account.Adres.Straat.Gemeente.HoofdGemeente == null ? Account.Adres.Straat.Gemeente.GemeenteNaam : Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam)}: ", OptionMode.Optional);

                if (bericht == null) return;

                string input;
                string menu;

                if ((bericht.Profiel == Account))
                {
                    input = "XWVA";
                    menu = "e<X>it, <W>ijzigen, <V>erwijderen, <A>ntwoorden";
                }
                else
                {
                    input = "XA";
                    menu = "e<X>it, <A>ntwoorden";
                }
               
                keuze = ConsoleHelper.LeesString($"Geef uw keuze ({menu}) Geen keuze klik *.  ", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
//////////// mogelijkheid tot weg klikken uit keuzemenu
                if (input.Contains((char)'*'))
                    break;

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
                    keuze = ConsoleHelper.LeesString($"Geef uw keuze ({menu})", 1, OptionMode.Mandatory).ToUpper().ToCharArray()[0];
                }

                Console.ForegroundColor = DarkMode ? ConsoleColor.White : ConsoleColor.Black;

                switch (keuze)
                {
                    case 'A':
                        AntwoordBericht(bericht);
                        break;

                    case 'W':
                        WijzigBericht(bericht);
                        break;

                    case 'V':
                        VerwijderBericht(bericht);
                        break;
                }
            }
        }
        public static void AntwoordBericht(Bericht hoofdBericht)
        {
            // Invoer gegevens
            var berichtTekst = ConsoleHelper.LeesString("Antwoord", 512, OptionMode.Mandatory);

            // Bevestiging + Bewaren
            if ((bool)ConsoleHelper.LeesBool("Antwoord toevoegen OK ?", OptionMode.Mandatory))
            {
                var bericht = new Bericht
                {
                    BerichtTitel = hoofdBericht.BerichtTitel,
                    BerichtTekst = berichtTekst,
                    BerichtTijdstip = DateTime.Now,
                    BerichtType = hoofdBericht.BerichtType,
                    Gemeente = Account.Adres.Straat.Gemeente,
                    HoofdBericht = hoofdBericht,
                    Profiel = (Profiel)Account,
                    Level = hoofdBericht.Level + 1
                };
              
                Service.ToevoegenBericht(bericht);

                Console.WriteLine($"\nGemeente: {bericht.Profiel.Adres.Straat.Gemeente.GemeenteNaam}\nBerichtType: {bericht.BerichtType.BerichtTypeNaam}\nTitel: {bericht.BerichtTitel}\nTekst: {bericht.BerichtTekst}\nTijdstip: {bericht.BerichtTijdstip}\nProfiel: {bericht.Profiel.LoginNaam}");
                Console.WriteLine("Het bericht werd toegevoegd.");
            }
            else
                Console.WriteLine("Het bericht werd niet toegevoegd.");
        }

        public static void WijzigBericht(Bericht bericht)
        {
            // Invoer gegevens
            var berichtTekst = ConsoleHelper.LeesString("Wijzig berichtTekst", 512, OptionMode.Mandatory);

            // Bevestiging + Bewaren
            if ((bool)ConsoleHelper.LeesBool("Bericht wijzigen OK ?", OptionMode.Mandatory))
            {
                bericht.BerichtTekst = berichtTekst;
                Service.WijzigenBericht(bericht);

                Console.WriteLine($"\nGemeente: {bericht.Profiel.Adres.Straat.Gemeente.GemeenteNaam}\nBerichtType: {bericht.BerichtType.BerichtTypeNaam}\nTitel: {bericht.BerichtTitel}\nTekst: {bericht.BerichtTekst}\nTijdstip: {bericht.BerichtTijdstip}\nProfiel: {bericht.Profiel.LoginNaam}");
                Console.WriteLine("Het bericht werd gewijzigd.");
            }
            else
                Console.WriteLine("Het bericht werd niet gewijzigd.");
        }

        public static void VerwijderBericht(Bericht bericht)
        {
            // Bevestiging + Bewaren
            if ((bool)ConsoleHelper.LeesBool("Bericht verwijderen OK ?", OptionMode.Mandatory))
            {
                Service.VerwijderBericht(bericht);

                Console.WriteLine($"\nGemeente: {bericht.Profiel.Adres.Straat.Gemeente.GemeenteNaam}\nBerichtType: {bericht.BerichtType.BerichtTypeNaam}\nTitel: {bericht.BerichtTitel}\nTekst: {bericht.BerichtTekst}\nTijdstip: {bericht.BerichtTijdstip}\nProfiel: {bericht.Profiel.LoginNaam}");
                Console.WriteLine("Het bericht werd verwijderd.");
            }
            else
                Console.WriteLine("Het bericht werd niet verwijderd.");
        }

        public static Bericht KiesBericht(string titel, OptionMode optionMode)
        {
            var berichten = Service.GetBerichtenGemeente(Account.Adres.Straat.Gemeente);

            var displayList = new List<string>();
            int seq = 0;                
            foreach (var bericht in berichten)
            {
                seq++;
                var indent1 = new String('\t', bericht.Level - 1);
                var indent2 = new String('\t', bericht.Level);

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append($"{indent1}{new String('.', 40)}");
                stringBuilder.Append($"\n{indent2}Van: {bericht.Profiel.Naam} Op: {bericht.BerichtTijdstip}");

                if (bericht.Level == 1)
                {
                    stringBuilder.Append($"\n{indent2}Type: {bericht.BerichtType.BerichtTypeNaam}");
                    stringBuilder.Append($"\n{indent2}Titel: {bericht.BerichtTitel}");
                }

                stringBuilder.Append($"\n{indent2}Tekst: {bericht.BerichtTekst}");

                if (seq == berichten.Count()) stringBuilder.Append($"\n{new String('.', 40)}");

                displayList.Add(stringBuilder.ToString());
            }

            var gekozenBericht = (Bericht)ConsoleHelper.LeesLijst($"{titel}", berichten, displayList, SelectionMode.Single, optionMode).FirstOrDefault();
            
            if (gekozenBericht != null)
                Console.WriteLine($"\nGekozen Bericht is {gekozenBericht.BerichtTijdstip} - {gekozenBericht.BerichtTitel} - {gekozenBericht.BerichtTekst}.");
            Console.WriteLine();

            return gekozenBericht;
        }
        public static BerichtType KiesBerichtType(string titel, OptionMode optionMode)
        {
            var berichtTypes = Service.GetAllBerichtTypes();
            var berichtType = (BerichtType)ConsoleHelper.LeesLijst($"{titel}", berichtTypes, berichtTypes.Select(b => b.BerichtTypeNaam + "\t" + b.BerichtTypeTekst).ToList(), SelectionMode.Single, optionMode).FirstOrDefault();
            if (berichtType != null) Console.WriteLine($"Gekozen BerichtType is {berichtType.BerichtTypeCode} - {berichtType.BerichtTypeNaam} - {berichtType.BerichtTypeTekst}.");
            Console.WriteLine();
            return berichtType;
        }
    }
}





