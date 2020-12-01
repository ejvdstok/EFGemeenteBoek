using Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
namespace Model.Repositories
{
    public class GemeenteBoekContext : DbContext
    {
        public static IConfigurationRoot configuration;
        private bool testMode = false;
        // ------
        // DbSets
        // ------
        public DbSet<Bericht> Berichten { get; set; }
        public DbSet<BerichtType> BerichtTypes { get; set; }
        public DbSet<ProfielInteresse> ProfielInteresses { get; set; }
        public DbSet<InteresseSoort> InteresseSoorten { get; set; }
        public DbSet<Profiel> Profielen { get; set; }
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Afdeling> Afdelingen { get; set; }
        public DbSet<Persoon> Personen { get; set; }
        public DbSet<Taal> Talen { get; set; }
        public DbSet<Gemeente> Gemeenten { get; set; }
        public DbSet<Provincie> Provincies { get; set; }
        public DbSet<Straat> Straten { get; set; }
        public DbSet<Adres> Adressen { get; set; }

        // ------------
        // Constructors
        // ------------
        public GemeenteBoekContext() { }
        public GemeenteBoekContext(DbContextOptions<GemeenteBoekContext> options) : base(options) { }
        // -------
        // Logging
        // -------
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging
            (
            builder => builder.AddConsole()
            .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
            );
            return serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Zoek de naam in de connectionStrings section - appsettings.json
                configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
                var connectionString = configuration.GetConnectionString("efgemeenteboek");
                if (connectionString != null) // Indien de naam is gevonden
                {
                    optionsBuilder.UseSqlServer(
                    connectionString
                    , options => options.MaxBatchSize(150)); // Max aantal SQL commands die kunnen doorgestuurd worden naar de database
                //.UseLoggerFactory(GetLoggerFactory())
                //.EnableSensitiveDataLogging(true) // Toont de waarden van de parameters bij de logging
                //.UseLazyLoadingProxies();
                }
            }
            else
            {
                testMode = true;
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --------
            // Personen + Medewerkers + Profiel
            // --------

            modelBuilder.Entity<Persoon>()
                    .ToTable("Personen")
                    .HasDiscriminator<string>("PersoonType")
                    .HasValue<Medewerker>("M")
                    .HasValue<Profiel>("P");

            // -----
            // Provincie
            // -----
            modelBuilder.Entity<Provincie>().ToTable("Provincies");
            modelBuilder.Entity<Provincie>().HasKey(b => b.ProvincieId);
            modelBuilder.Entity<Provincie>().Property(b => b.ProvincieId)
            .IsRequired();
            modelBuilder.Entity<Provincie>().Property(b => b.ProvincieCode)
            .IsRequired()
            .HasMaxLength(3);
            modelBuilder.Entity<Provincie>().Property(b => b.Provincienaam)
            .IsRequired()
            .HasMaxLength(30);

            // -----
            // Gemeente
            // -----
            modelBuilder.Entity<Gemeente>
           (
           t =>
            {
                t.ToTable("Gemeenten");
                t.HasKey(b => b.GemeenteId);
                t.Property(b => b.GemeenteId).IsRequired();
                t.Property(b => b.GemeenteNaam).IsRequired().HasMaxLength(50);
                t.Property(b => b.Postcode).IsRequired();
                t.Property(b => b.ProvincieId).IsRequired();
                t.Property(b => b.HoofdGemeenteId).IsRequired(false);
                t.Property(b => b.TaalId).IsRequired();
            });
            modelBuilder.Entity<Gemeente>
           (
           entity =>
           {
               entity.HasIndex(e => e.GemeenteNaam).IsUnique();
           }
           );

            modelBuilder.Entity<Gemeente>()
            .HasOne(x => x.Provincie)
            .WithMany(y => y.Gemeenten)
            .HasForeignKey(x => x.ProvincieId);

            modelBuilder.Entity<Gemeente>()
            .HasOne(x => x.Taal)
            .WithMany(y => y.Gemeenten)
            .HasForeignKey(x => x.TaalId);

            modelBuilder.Entity<Gemeente>()
            .HasOne(x => x.HoofdGemeente)
            .WithMany(y => y.Gemeenten)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.HoofdGemeenteId);

            // -----
            // Straat
            // -----

            modelBuilder.Entity<Straat>().ToTable("Straten");
            modelBuilder.Entity<Straat>().HasKey(b => b.StraatId);
            modelBuilder.Entity<Straat>().Property(b => b.StraatNaam)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Straat>().Property(b => b.GemeenteId)
            .IsRequired();

            modelBuilder.Entity<Straat>()
            .HasOne(x => x.Gemeente)
             .WithMany(y => y.Straten)
             .HasForeignKey(x => x.GemeenteId);

            // -----
            // Adres
            // -----

            modelBuilder.Entity<Adres>().ToTable("Adressen");
            modelBuilder.Entity<Adres>().HasKey(b => b.AdresId);
            modelBuilder.Entity<Adres>().Property(b => b.AdresId)
            .IsRequired();
            modelBuilder.Entity<Adres>().Property(b => b.HuisNr)
            .IsRequired()
            .HasMaxLength(5);
            modelBuilder.Entity<Adres>().Property(b => b.BusNr)
            .HasMaxLength(5);
            modelBuilder.Entity<Adres>().Property(b => b.StraatId)
           .IsRequired();

            modelBuilder.Entity<Adres>
            (
            entity =>
            {
                 entity.HasIndex(e => new { e.StraatId, e.HuisNr, e.BusNr }).IsUnique();
             }
            );

            modelBuilder.Entity<Adres>()
             .HasOne(x => x.Straat)
            .WithMany(y => y.Adressen)
            .HasForeignKey(x => x.StraatId);

            // Concurrency
            modelBuilder.Entity<Adres>()
            .Property(b => b.Aangepast).HasColumnType("timestamp");
            modelBuilder.Entity<Adres>()
            .Property(b => b.Aangepast)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

            // -----
            // Taal
            // -----
            modelBuilder.Entity<Taal>
                (
                t =>
                {
                    t.ToTable("Talen");
                    t.HasKey(b => b.TaalId);
                    t.Property(b => b.TaalId).IsRequired();
                    t.Property(b => b.TaalCode).IsRequired()
                      .HasMaxLength(2);
                    t.Property(b => b.TaalNaam).IsRequired()
                     .HasMaxLength(20);
                }
                );
            
            modelBuilder.Entity<Taal>
            (
            entity =>
            {
                entity.HasIndex(e => e.TaalId).IsUnique();
                entity.HasIndex(e => e.TaalNaam).IsUnique();
            }
            );

            // -----
            // Persoon
            // -----
            modelBuilder.Entity<Persoon>().ToTable("Personen");
            modelBuilder.Entity<Persoon>().HasKey(b => b.PersoonId);
            modelBuilder.Entity<Persoon>().Property(b => b.PersoonId)
            .IsRequired()
            .HasMaxLength(2);
       
            modelBuilder.Entity<Persoon>().Property(b => b.FamilieNaam)
            .IsRequired()
            .HasMaxLength(30);
            modelBuilder.Entity<Persoon>().Property(b => b.Geblokkeerd)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Persoon>().Property(b => b.GeboorteDatum);
            modelBuilder.Entity<Persoon>().Property(b => b.GeboorteplaatsId);
            modelBuilder.Entity<Persoon>().Property(b => b.LoginAantal)
            .IsRequired();
            modelBuilder.Entity<Persoon>().Property(b => b.LoginNaam)
            .IsRequired()
            .HasMaxLength(25);
            modelBuilder.Entity<Persoon>().Property(b => b.LoginPaswoord)
            .IsRequired()
            .HasMaxLength(255);
            modelBuilder.Entity<Persoon>().Property(b => b.TaalId)
            .IsRequired();
            modelBuilder.Entity<Persoon>().Property(b => b.TelefoonNr)
            .HasMaxLength(30);
            modelBuilder.Entity<Persoon>().Property(b => b.VerkeerdeLoginsAantal)
            .IsRequired();
            modelBuilder.Entity<Persoon>().Property(b => b.VoorNaam)
            .IsRequired()
            .HasMaxLength(20);
            // Concurrency
            modelBuilder.Entity<Persoon>()
            .Property(b => b.Aangepast).HasColumnType("timestamp");
            modelBuilder.Entity<Persoon>()
            .Property(b => b.Aangepast)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Persoon>
            (
            entity =>
            {
                entity.HasIndex(e => e.LoginNaam).IsUnique();
            }
            );

            modelBuilder.Entity<Persoon>()
              .HasOne(x => x.Taal)
              .WithMany(y => y.Personen)
              .OnDelete(DeleteBehavior.NoAction)
              .HasForeignKey(x => x.TaalId);

            modelBuilder.Entity<Persoon>()
             .HasOne(x => x.GeboortePlaats)
             .WithMany(y => y.Personen)
             .OnDelete(DeleteBehavior.NoAction)
             .HasForeignKey(x => x.GeboorteplaatsId);

            modelBuilder.Entity<Persoon>()
                 .HasOne(x => x.Adres)
                 .WithMany(y => y.Personen)
                 .HasForeignKey(x => x.AdresId);

            // -----
            // Medewerker
            // -----
            modelBuilder.Entity<Medewerker>().ToTable("Personen");
            modelBuilder.Entity<Medewerker>().Property(b => b.AfdelingId)
            .IsRequired();

            modelBuilder.Entity<Medewerker>()
                .HasOne(x => x.Afdeling)
                .WithMany(y => y.Medewerkers)
                .HasForeignKey(x => x.AfdelingId);


            // -----
            // Profiel
            // -----
            modelBuilder.Entity<Profiel>().ToTable("Personen");
            modelBuilder.Entity<Profiel>().Property(b => b.KennismakingTekst)
            .IsRequired()
            .HasMaxLength(225);
            modelBuilder.Entity<Profiel>().Property(b => b.WoontHierSindsDatum);
            modelBuilder.Entity<Profiel>().Property(b => b.BeroepTekst)
            .HasMaxLength(30);
            modelBuilder.Entity<Profiel>().Property(b => b.FirmaNaam)
            .HasMaxLength(30);
            modelBuilder.Entity<Profiel>().Property(b => b.WebsiteAdres)
            .HasMaxLength(50);
            modelBuilder.Entity<Profiel>().Property(b => b.EmailAdres)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Profiel>().Property(b => b.FacebookNaam)
            .HasMaxLength(30);
            modelBuilder.Entity<Profiel>().Property(b => b.GoedgekeurdTijdstip);
            modelBuilder.Entity<Profiel>().Property(b => b.CreatieTijdstip)
            .IsRequired();
            modelBuilder.Entity<Profiel>().Property(b => b.LaatsteUpdateTijdstip)
            .IsRequired();

            // -----
            // Afdeling
            // -----
            modelBuilder.Entity<Afdeling>().ToTable("Afdelingen");
            modelBuilder.Entity<Afdeling>().HasKey(b => b.AfdelingId);
            modelBuilder.Entity<Afdeling>().Property(b => b.AfdelingId)
            .IsRequired();
            modelBuilder.Entity<Afdeling>().Property(b => b.AfdelingCode)
            .IsRequired()
            .HasMaxLength(4);
            modelBuilder.Entity<Afdeling>().Property(b => b.AfdelingNaam)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Afdeling>().Property(b => b.AfdelingTekst)
            .HasMaxLength(255);

            modelBuilder.Entity<Afdeling>
            (
            entity =>
            {
                entity.HasIndex(e => e.AfdelingCode).IsUnique();
                entity.HasIndex(e => e.AfdelingNaam).IsUnique();
            }
            );

            // ------------
            // InteresseSoort
            // ------------
            modelBuilder.Entity<InteresseSoort>().ToTable("InteresseSoort");
            modelBuilder.Entity<InteresseSoort>().HasKey(b => b.InteresseSoortId);
            modelBuilder.Entity<InteresseSoort>().Property(b => b.InteresseSoortId)
            .IsRequired();
            modelBuilder.Entity<InteresseSoort>().Property(b => b.InteresseSoortNaam)
            .IsRequired()
            .HasMaxLength(50);

            // ----------
            // ProfielInteresse
            // ----------

            modelBuilder.Entity<ProfielInteresse>
            (
             t =>
             {
                 t.HasOne(b => b.Profiel)
                .WithMany(b => b.ProfielInteresses);
                 t.HasOne(b => b.InteresseSoort)
                .WithMany(b => b.ProfielInteresses);
             }
             );


            modelBuilder.Entity<ProfielInteresse>
            (
            t =>
            { 
                t.ToTable("ProfielInteresses");
            t.HasKey(t => new { t.PersoonId, t.InteresseSoortId });
            t.Property(t => t.PersoonId).IsRequired();
            t.Property(t => t.InteresseSoortId).IsRequired();
            });

            modelBuilder.Entity<ProfielInteresse>()
                .HasOne(x => x.InteresseSoort)
                .WithMany(y => y.ProfielInteresses);

            modelBuilder.Entity<ProfielInteresse>()
                .HasOne(x => x.Profiel)
                .WithMany(y => y.ProfielInteresses);

            // Concurrency
            modelBuilder.Entity<ProfielInteresse>()
            .Property(b => b.Aangepast).HasColumnType("timestamp");
            modelBuilder.Entity<ProfielInteresse>()
            .Property(b => b.Aangepast)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();

            // -----
            // Bericht
            // -----
            modelBuilder.Entity<Bericht>().ToTable("Berichten");
            modelBuilder.Entity<Bericht>().HasKey(b => b.BerichtId);
            modelBuilder.Entity<Bericht>().Property(b => b.BerichtId)
            .IsRequired();
            modelBuilder.Entity<Bericht>().Property(b => b.HoofdBerichtId);
            modelBuilder.Entity<Bericht>().Property(b => b.PersoonId)
            .IsRequired();
            modelBuilder.Entity<Bericht>().Property(b => b.BerichtTypeId)
            .IsRequired();
            modelBuilder.Entity<Bericht>().Property(b => b.BerichtTijdstip)
            .IsRequired();
            modelBuilder.Entity<Bericht>().Property(b => b.BerichtTitel)
            .IsRequired()
            .HasMaxLength(50);
            modelBuilder.Entity<Bericht>().Property(b => b.BerichtTekst)
            .IsRequired()
            .HasMaxLength(225);

            modelBuilder.Entity<Bericht>()
                .HasOne(x => x.HoofdBericht)
                .WithMany(y => y.Berichten)
                .HasForeignKey(x => x.HoofdBerichtId)
                .OnDelete(DeleteBehavior.ClientNoAction);
      

            modelBuilder.Entity<Bericht>()
                .HasOne(x => x.BerichtType)
                .WithMany(y => y.Berichten)
                .HasForeignKey(x => x.BerichtTypeId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<Bericht>()
                .HasOne(x => x.Profiel)
                .WithMany(y => y.Berichten)
                .HasForeignKey(x => x.PersoonId)
                .OnDelete(DeleteBehavior.ClientNoAction);


            // -----
            // BerichtType
            // -----
            modelBuilder.Entity<BerichtType>().ToTable("BerichtTypes");
            modelBuilder.Entity<BerichtType>().HasKey(b => b.BerichtTypeId);
            modelBuilder.Entity<BerichtType>().Property(b => b.BerichtTypeId)
            .IsRequired();
            modelBuilder.Entity<BerichtType>().Property(b => b.BerichtTypeCode)
            .IsRequired()
            .HasMaxLength(2);
            modelBuilder.Entity<BerichtType>().Property(b => b.BerichtTypeNaam)
            .IsRequired()
            .HasMaxLength(20);
            modelBuilder.Entity<BerichtType>().Property(b => b.BerichtTypeTekst);

            modelBuilder.Entity<BerichtType>
            (
            entity =>
            {
               entity.HasIndex(e => e.BerichtTypeCode).IsUnique();
            }
            );



            ///////////////////////////////////////

            // -------
            // Seeding
            // -------
            if (!testMode)
            {

                // --------------
                // Seeding Provincies
                // --------------
                modelBuilder.Entity<Provincie>().HasData
                (
                new Provincie { ProvincieId = 1, ProvincieCode = "ANT", Provincienaam = "Antwerpen" },
                new Provincie { ProvincieId = 2, ProvincieCode = "LIM", Provincienaam = "Limburg" },
                new Provincie { ProvincieId = 3, ProvincieCode = "OVL", Provincienaam = "Oost-Vlaanderen" },
                new Provincie { ProvincieId = 4, ProvincieCode = "VBR", Provincienaam = "Vlaams-Brabant" },
                new Provincie { ProvincieId = 5, ProvincieCode = "WVL", Provincienaam = "West-Vlaanderen" },
                new Provincie { ProvincieId = 6, ProvincieCode = "WBR", Provincienaam = "Waals-Brabant" },
                new Provincie { ProvincieId = 7, ProvincieCode = "HEN", Provincienaam = "Henegouwen" },
                new Provincie { ProvincieId = 8, ProvincieCode = "LUI", Provincienaam = "Luik" },
                new Provincie { ProvincieId = 9, ProvincieCode = "LUX", Provincienaam = "Luxemburg" },
                new Provincie { ProvincieId = 10, ProvincieCode = "NAM", Provincienaam = "Namen" }
                );

                // --------------
                // Seeding Gemeente
                // --------------
                modelBuilder.Entity<Gemeente>().HasData
                (
                new Gemeente { GemeenteId = 1730, GemeenteNaam = "Beernem", Postcode = 8730, ProvincieId = 5, TaalId = 1 },
                new Gemeente { GemeenteId = 1731, GemeenteNaam = "Oedelem", Postcode = 8730, ProvincieId = 5, HoofdGemeenteId = 1730, TaalId = 1 },
                new Gemeente { GemeenteId = 1732, GemeenteNaam = "Sint-Joris", Postcode = 8730, ProvincieId = 5, HoofdGemeenteId = 1730, TaalId = 1 },
                new Gemeente { GemeenteId = 1790, GemeenteNaam = "Oostkamp", Postcode = 8020, ProvincieId = 5,  TaalId = 1 },
                new Gemeente { GemeenteId = 1791, GemeenteNaam = "Hertsberge", Postcode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 },
                new Gemeente { GemeenteId = 1792, GemeenteNaam = "Ruddervoorde", Postcode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 },
                new Gemeente { GemeenteId = 1793, GemeenteNaam = "Waardamme", Postcode = 8020, ProvincieId = 5, HoofdGemeenteId = 1790, TaalId = 1 }
                );

                // --------------
                // Seeding Straat
                // --------------
                modelBuilder.Entity<Straat>().HasData
                (
                new Straat { StraatId = 1, StraatNaam = "Lange baan", GemeenteId = 1730 },
                new Straat { StraatId = 2, StraatNaam = "Achterom", GemeenteId = 1730 },
                new Straat { StraatId = 3, StraatNaam = "Bolwerk", GemeenteId = 1730 },
                new Straat { StraatId = 4, StraatNaam = "Dijkstraat", GemeenteId = 1730 },
                new Straat { StraatId = 5, StraatNaam = "Winkelstraat", GemeenteId = 1730 },
                new Straat { StraatId = 6, StraatNaam = "Kerkstraat", GemeenteId = 1790 },
                new Straat { StraatId = 7, StraatNaam = "Eikenlaan", GemeenteId = 1790 },
                new Straat { StraatId = 8, StraatNaam = "Kastanjedreef", GemeenteId = 1790 },
                new Straat { StraatId = 9, StraatNaam = "Bosweg", GemeenteId = 1790 },
                new Straat { StraatId = 10, StraatNaam = "Meibloemstraat", GemeenteId = 1790 }
                 );

                // ----------------------
                // Seeding Adres
                // ----------------------
                modelBuilder.Entity<Adres>().HasData
                (
                new Adres { AdresId = 1, StraatId = 1, HuisNr = "69", BusNr = "b" },
                new Adres { AdresId = 2, StraatId = 4, HuisNr = "88", BusNr = "7" }
                );

                // ----------------------
                // Seeding Taal
                // ----------------------
                modelBuilder.Entity<Taal>().HasData
                (
                new Taal { TaalId = 1, TaalCode = "nl", TaalNaam = "Nederlands" },
                new Taal { TaalId = 2, TaalCode = "fr", TaalNaam = "Frans" },
                new Taal { TaalId = 3, TaalCode = "en", TaalNaam = "Engels" }
                );

                // ----------------------
                // Seeding Afdeling
                // ----------------------
                modelBuilder.Entity<Afdeling>().HasData
                (
                new Afdeling { AfdelingId = 1, AfdelingCode = "VERK", AfdelingNaam = "Verkoop" },
                new Afdeling { AfdelingId = 2, AfdelingCode = "BOEK", AfdelingNaam = "Boekhouding" },
                new Afdeling { AfdelingId = 3, AfdelingCode = "AANK", AfdelingNaam = "Aankoop" }
                );

                // ----------------------
                // Seeding Persoon(medewerkers)
                // ----------------------
                modelBuilder.Entity<Medewerker>().HasData
                (
                new Medewerker
                {
                    PersoonId = 1,
                    VoorNaam = "Jan",
                    FamilieNaam = "Janssens",
                    Geslacht = Geslacht.M,
                    GeboorteDatum = new DateTime(1980, 1, 1),
                    TelefoonNr = "02/2222222222",
                    LoginNaam = "Jan",
                    LoginPaswoord = "Baarden",
                    VerkeerdeLoginsAantal = 0,
                    LoginAantal = 0,
                    Geblokkeerd = false,
                    AfdelingId = 1,
                    AdresId = 1,
                    GeboorteplaatsId = 1730,
                    TaalId = 1,

                },
                new Medewerker
                {
                    PersoonId = 2,
                    VoorNaam = "Piet",
                    FamilieNaam = "Pieters",
                    Geslacht = Geslacht.M,
                    GeboorteDatum = new DateTime(1980, 1, 1),
                    TelefoonNr = "02/2222222222",
                    LoginNaam = "Piet",
                    LoginPaswoord = "Baarden",
                    VerkeerdeLoginsAantal = 0,
                    LoginAantal = 0,
                    Geblokkeerd = false,
                    AfdelingId = 1,
                    AdresId = 1,
                    GeboorteplaatsId = 1731,
                    TaalId = 1,
                },
                new Medewerker
                {
                    PersoonId = 3,
                    VoorNaam = "Joris",
                    FamilieNaam = "Jorissens",
                    Geslacht = Geslacht.M,
                    GeboorteDatum = new DateTime(1980, 1, 1),
                    TelefoonNr = "01/11111111",
                    LoginNaam = "Joris",
                    LoginPaswoord = "Baarden",
                    VerkeerdeLoginsAantal = 0,
                    LoginAantal = 0,
                    Geblokkeerd = false,
                    AfdelingId = 1,
                    AdresId = 1,
                    GeboorteplaatsId = 1731,
                    TaalId = 1,
                },
                new Medewerker
                {
                    PersoonId = 4,
                    VoorNaam = "Korneel",
                    FamilieNaam = "Cornelis",
                    Geslacht = Geslacht.M,
                    GeboorteDatum = new DateTime(1980, 1, 1),
                    TelefoonNr = "02/2222222222",
                    LoginNaam = "Korneel",
                    LoginPaswoord = "Baarden",
                    VerkeerdeLoginsAantal = 0,
                    LoginAantal = 0,
                    Geblokkeerd = false,
                    AfdelingId = 1,
                    AdresId = 1,
                    GeboorteplaatsId = 1792,
                    TaalId = 1,
                }

                ) ;

                // ----------------------
                // Seeding InteresseSoort
                // ----------------------
                modelBuilder.Entity<InteresseSoort>().HasData
                (
                new InteresseSoort { InteresseSoortId = 1, InteresseSoortNaam = "Vrijwilligerswerk" },
                new InteresseSoort { InteresseSoortId = 2, InteresseSoortNaam = "Wandelen" },
                new InteresseSoort { InteresseSoortId = 3, InteresseSoortNaam = "Klussen" },
                new InteresseSoort { InteresseSoortId = 4, InteresseSoortNaam = "Tv Kijken" },
                new InteresseSoort { InteresseSoortId = 5, InteresseSoortNaam = "ICT" },
                new InteresseSoort { InteresseSoortId = 6, InteresseSoortNaam = "Muziek Spelen" },
                new InteresseSoort { InteresseSoortId = 7, InteresseSoortNaam = "Muziek Beluisteren" },
                new InteresseSoort { InteresseSoortId = 8, InteresseSoortNaam = "Natuur" },
                new InteresseSoort { InteresseSoortId = 9, InteresseSoortNaam = "Fietsen" },
                new InteresseSoort { InteresseSoortId = 10, InteresseSoortNaam = "Zwemmen" }
                );

                // ----------------------
                // Seeding BerichtType
                // ----------------------
                modelBuilder.Entity<BerichtType>().HasData
                (
                new BerichtType { BerichtTypeId = 1, BerichtTypeCode = "AL", BerichtTypeNaam = "Algemeen", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 2, BerichtTypeCode = "TK", BerichtTypeNaam = "Te koop", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 3, BerichtTypeCode = "IZ", BerichtTypeNaam = "Ik zoek", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 4, BerichtTypeCode = "ID", BerichtTypeNaam = "Idee", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 5, BerichtTypeCode = "LN", BerichtTypeNaam = "Lenen", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 6, BerichtTypeCode = "WG", BerichtTypeNaam = "Weggeven", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 7, BerichtTypeCode = "AC", BerichtTypeNaam = "Activiteit", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 8, BerichtTypeCode = "MD", BerichtTypeNaam = "Melding", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 9, BerichtTypeCode = "BS", BerichtTypeNaam = "Babysit", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 10, BerichtTypeCode = "HD", BerichtTypeNaam = "Huisdieren", BerichtTypeTekst = "" },
                new BerichtType { BerichtTypeId = 11, BerichtTypeCode = "GH", BerichtTypeNaam = "Gezondheid", BerichtTypeTekst = "" }
                );
            }
        }
    }
}