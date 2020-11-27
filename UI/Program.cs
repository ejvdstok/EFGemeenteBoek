using System;
//using System.Collections.Generic;
//using System.Linq;
//using Castle.Core.Internal;
using Model.Entities;
//using Model.Services;
//using Model.Repositories;

namespace UI
{
    internal class Program
    {
        private static readonly bool DarkMode = false;

        // . . . . 

        private static Persoon Account;
        private static string LoginGegevens => $"{(Account == null ? "Niet ingelogd" : (Account is Profiel ? "PROFIEL: " : "MEDEWERKER: ") + "Nr: " + Account.PersoonId + " - Naam: " + Account.LoginNaam)}";

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
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

                Console.ForegroundColor = ConsoleColor.Blue;
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
        //            Console.WriteLine($"<R>aadplegen berichten van uw hoofdgemeente {(Account.Adres.Straat.Gemeente.HoofdGemeente==null ? Account.Adres.Straat.Gemeente.GemeenteNaam : Account.Adres.Straat.Gemeente.HoofdGemeente.GemeenteNaam)}");
                }

                Console.WriteLine("e<X>it");
                Console.WriteLine();

             //   keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, true).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
            //        keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, true).ToUpper().ToCharArray()[0];
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

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine($"===================");
                Console.WriteLine($"A C C O U T M E N U - {LoginGegevens}");
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

       //         keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, true).ToUpper().ToCharArray()[0];

                while (!input.Contains((char)keuze))
                {
                    ConsoleHelper.ToonFoutBoodschap($"Verkeerde keuze ({input}): ");
        //            keuze = ConsoleHelper.LeesString($"Geef uw keuze ({input})", 1, true).ToUpper().ToCharArray()[0];
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

            throw new NotImplementedException();
        }

        public static void BlokkerenProfiel()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Blokkeren van een Profiel");
            Console.WriteLine("-------------------------");

            throw new NotImplementedException();
        }

        public static void DeblokkerenProfiel()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Deblokkeren van een Profiel");
            Console.WriteLine("---------------------------");

            throw new NotImplementedException();
        }

        public static void Inloggen()
        {
            throw new NotImplementedException();
        }

        public static void Uitloggen()
        {
            throw new NotImplementedException();
        }

        public static Persoon Registeren()
        {
            throw new NotImplementedException();
        }

        public static void ToonGegegevens(Persoon persoon)
        {
            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine("Overzicht");
            Console.WriteLine("---------");

            throw new NotImplementedException();
        }

        public static void WijzigGegevens()
        {
            Console.WriteLine();
            Console.WriteLine("----------------");
            Console.WriteLine("Wijzigen Profiel");
            Console.WriteLine("----------------");

            throw new NotImplementedException();
        }

        public static void VerwijderGegevens(Persoon persoon)
        {
            throw new NotImplementedException();
        }

        public static void InvoerenNieuwBericht()
        {
            throw new NotImplementedException();
        }

        public static void RaadplegenBerichten()
        {
            throw new NotImplementedException();
        }

        public static void AntwoordBericht(Bericht hoofdBericht)
        {
            throw new NotImplementedException();
        }

        public static void WijzigBericht(Bericht bericht)
        {
            throw new NotImplementedException();
        }

        public static void VerwijderBericht(Bericht bericht)
        {
            throw new NotImplementedException();
        }
    }
}





