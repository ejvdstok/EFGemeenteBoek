﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Repositories;

namespace Model.Migrations
{
    [DbContext(typeof(EFCultuurhuisContext))]
    partial class EFCultuurhuisContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Model.Entities.Adres", b =>
                {
                    b.Property<int>("AdresId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("BusNr")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("HuisNr")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("StraatId")
                        .HasColumnType("int");

                    b.HasKey("AdresId");

                    b.HasIndex("StraatId", "HuisNr", "BusNr")
                        .IsUnique();

                    b.ToTable("Adressen");

                    b.HasData(
                        new
                        {
                            AdresId = 1,
                            BusNr = "b",
                            HuisNr = "69",
                            StraatId = 1
                        },
                        new
                        {
                            AdresId = 2,
                            BusNr = "7",
                            HuisNr = "88",
                            StraatId = 4
                        });
                });

            modelBuilder.Entity("Model.Entities.Afdeling", b =>
                {
                    b.Property<int>("AfdelingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AfdelingCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("AfdelingNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AfdelingTekst")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AfdelingId");

                    b.HasIndex("AfdelingCode")
                        .IsUnique();

                    b.HasIndex("AfdelingNaam")
                        .IsUnique();

                    b.ToTable("Afdelingen");

                    b.HasData(
                        new
                        {
                            AfdelingId = 1,
                            AfdelingCode = "VERK",
                            AfdelingNaam = "Verkoop"
                        },
                        new
                        {
                            AfdelingId = 2,
                            AfdelingCode = "BOEK",
                            AfdelingNaam = "Boekhouding"
                        },
                        new
                        {
                            AfdelingId = 3,
                            AfdelingCode = "AANK",
                            AfdelingNaam = "Aankoop"
                        });
                });

            modelBuilder.Entity("Model.Entities.Bericht", b =>
                {
                    b.Property<int>("BerichtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BerichtTekst")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<DateTime>("BerichtTijdstip")
                        .HasColumnType("datetime2");

                    b.Property<string>("BerichtTitel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("BerichtTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GemeenteId")
                        .HasColumnType("int");

                    b.Property<int>("HoofdBerichtId")
                        .HasColumnType("int");

                    b.Property<int>("PersoonId")
                        .HasColumnType("int");

                    b.HasKey("BerichtId");

                    b.HasIndex("BerichtTypeId");

                    b.HasIndex("GemeenteId");

                    b.HasIndex("HoofdBerichtId");

                    b.ToTable("Berichten");
                });

            modelBuilder.Entity("Model.Entities.BerichtType", b =>
                {
                    b.Property<int>("BerichtTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BerichtTypeCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("BerichtTypeNaam")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("BerichtTypeTekst")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BerichtTypeId");

                    b.HasIndex("BerichtTypeCode")
                        .IsUnique();

                    b.ToTable("BerichtTypes");

                    b.HasData(
                        new
                        {
                            BerichtTypeId = 1,
                            BerichtTypeCode = "AL",
                            BerichtTypeNaam = "Algemeen",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 2,
                            BerichtTypeCode = "TK",
                            BerichtTypeNaam = "Te koop",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 3,
                            BerichtTypeCode = "IZ",
                            BerichtTypeNaam = "Ik zoek",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 4,
                            BerichtTypeCode = "ID",
                            BerichtTypeNaam = "Idee",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 5,
                            BerichtTypeCode = "LN",
                            BerichtTypeNaam = "Lenen",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 6,
                            BerichtTypeCode = "WG",
                            BerichtTypeNaam = "Weggeven",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 7,
                            BerichtTypeCode = "AC",
                            BerichtTypeNaam = "Activiteit",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 8,
                            BerichtTypeCode = "MD",
                            BerichtTypeNaam = "Melding",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 9,
                            BerichtTypeCode = "BS",
                            BerichtTypeNaam = "Babysit",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 10,
                            BerichtTypeCode = "HD",
                            BerichtTypeNaam = "Huisdieren",
                            BerichtTypeTekst = ""
                        },
                        new
                        {
                            BerichtTypeId = 11,
                            BerichtTypeCode = "GH",
                            BerichtTypeNaam = "Gezondheid",
                            BerichtTypeTekst = ""
                        });
                });

            modelBuilder.Entity("Model.Entities.Gemeente", b =>
                {
                    b.Property<int>("GemeenteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GemeenteNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("HoofdGemeenteId")
                        .HasColumnType("int");

                    b.Property<int>("Postcode")
                        .HasColumnType("int");

                    b.Property<int>("ProvincieId")
                        .HasColumnType("int");

                    b.Property<int>("TaalId")
                        .HasColumnType("int");

                    b.HasKey("GemeenteId");

                    b.HasIndex("GemeenteNaam")
                        .IsUnique();

                    b.HasIndex("HoofdGemeenteId");

                    b.HasIndex("ProvincieId");

                    b.HasIndex("TaalId");

                    b.ToTable("Gemeenten");

                    b.HasData(
                        new
                        {
                            GemeenteId = 1730,
                            GemeenteNaam = "Beernem",
                            Postcode = 8730,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1731,
                            GemeenteNaam = "Oedelem",
                            HoofdGemeenteId = 1730,
                            Postcode = 8730,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1732,
                            GemeenteNaam = "Sint-Joris",
                            HoofdGemeenteId = 1730,
                            Postcode = 8730,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1790,
                            GemeenteNaam = "Oostkamp",
                            Postcode = 8020,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1791,
                            GemeenteNaam = "Hertsberge",
                            HoofdGemeenteId = 1790,
                            Postcode = 8020,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1792,
                            GemeenteNaam = "Ruddervoorde",
                            HoofdGemeenteId = 1790,
                            Postcode = 8020,
                            ProvincieId = 5,
                            TaalId = 1
                        },
                        new
                        {
                            GemeenteId = 1793,
                            GemeenteNaam = "Waardamme",
                            HoofdGemeenteId = 1790,
                            Postcode = 8020,
                            ProvincieId = 5,
                            TaalId = 1
                        });
                });

            modelBuilder.Entity("Model.Entities.InteresseSoort", b =>
                {
                    b.Property<int>("InteresseSoortId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("InteresseSoortNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("InteresseSoortId");

                    b.ToTable("InteresseSoort");

                    b.HasData(
                        new
                        {
                            InteresseSoortId = 1,
                            InteresseSoortNaam = "Vrijwilligerswerk"
                        },
                        new
                        {
                            InteresseSoortId = 2,
                            InteresseSoortNaam = "Wandelen"
                        },
                        new
                        {
                            InteresseSoortId = 3,
                            InteresseSoortNaam = "Klussen"
                        },
                        new
                        {
                            InteresseSoortId = 4,
                            InteresseSoortNaam = "Tv Kijken"
                        },
                        new
                        {
                            InteresseSoortId = 5,
                            InteresseSoortNaam = "ICT"
                        },
                        new
                        {
                            InteresseSoortId = 6,
                            InteresseSoortNaam = "Muziek Spelen"
                        },
                        new
                        {
                            InteresseSoortId = 7,
                            InteresseSoortNaam = "Muziek Beluisteren"
                        },
                        new
                        {
                            InteresseSoortId = 8,
                            InteresseSoortNaam = "Natuur"
                        },
                        new
                        {
                            InteresseSoortId = 9,
                            InteresseSoortNaam = "Fietsen"
                        },
                        new
                        {
                            InteresseSoortId = 10,
                            InteresseSoortNaam = "Zwemmen"
                        });
                });

            modelBuilder.Entity("Model.Entities.Persoon", b =>
                {
                    b.Property<int>("PersoonId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<int>("AdresId")
                        .HasColumnType("int");

                    b.Property<string>("FamilieNaam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("Geblokkeerd")
                        .HasMaxLength(50)
                        .HasColumnType("bit");

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("GeboorteplaatsId")
                        .HasColumnType("int");

                    b.Property<int>("LoginAantal")
                        .HasColumnType("int");

                    b.Property<string>("LoginNaam")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LoginPaswoord")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TaalId")
                        .HasColumnType("int");

                    b.Property<string>("TelefoonNr")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("VerkeerdeLoginsAantal")
                        .HasColumnType("int");

                    b.Property<string>("VoorNaam")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PersoonId");

                    b.HasIndex("AdresId");

                    b.HasIndex("GeboorteplaatsId");

                    b.HasIndex("LoginNaam")
                        .IsUnique();

                    b.HasIndex("TaalId");

                    b.ToTable("Personen");
                });

            modelBuilder.Entity("Model.Entities.ProfielInteresse", b =>
                {
                    b.Property<int>("PersoonId")
                        .HasColumnType("int");

                    b.Property<int>("InteresseSoortId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Aangepast")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("ProfielInteresseTekst")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfielPersoonId")
                        .HasColumnType("int");

                    b.HasKey("PersoonId", "InteresseSoortId");

                    b.HasIndex("InteresseSoortId");

                    b.HasIndex("ProfielPersoonId");

                    b.ToTable("ProfielInteresses");
                });

            modelBuilder.Entity("Model.Entities.Provincie", b =>
                {
                    b.Property<int>("ProvincieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ProvincieCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Provincienaam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ProvincieId");

                    b.ToTable("Provincies");

                    b.HasData(
                        new
                        {
                            ProvincieId = 1,
                            ProvincieCode = "ANT",
                            Provincienaam = "Antwerpen"
                        },
                        new
                        {
                            ProvincieId = 2,
                            ProvincieCode = "LIM",
                            Provincienaam = "Limburg"
                        },
                        new
                        {
                            ProvincieId = 3,
                            ProvincieCode = "OVL",
                            Provincienaam = "Oost-Vlaanderen"
                        },
                        new
                        {
                            ProvincieId = 4,
                            ProvincieCode = "VBR",
                            Provincienaam = "Vlaams-Brabant"
                        },
                        new
                        {
                            ProvincieId = 5,
                            ProvincieCode = "WVL",
                            Provincienaam = "West-Vlaanderen"
                        },
                        new
                        {
                            ProvincieId = 6,
                            ProvincieCode = "WBR",
                            Provincienaam = "Waals-Brabant"
                        },
                        new
                        {
                            ProvincieId = 7,
                            ProvincieCode = "HEN",
                            Provincienaam = "Henegouwen"
                        },
                        new
                        {
                            ProvincieId = 8,
                            ProvincieCode = "LUI",
                            Provincienaam = "Luik"
                        },
                        new
                        {
                            ProvincieId = 9,
                            ProvincieCode = "LUX",
                            Provincienaam = "Luxemburg"
                        },
                        new
                        {
                            ProvincieId = 10,
                            ProvincieCode = "NAM",
                            Provincienaam = "Namen"
                        });
                });

            modelBuilder.Entity("Model.Entities.Straat", b =>
                {
                    b.Property<int>("StraatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GemeenteId")
                        .HasColumnType("int");

                    b.Property<string>("StraatNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StraatId");

                    b.HasIndex("GemeenteId");

                    b.ToTable("Straten");

                    b.HasData(
                        new
                        {
                            StraatId = 1,
                            GemeenteId = 1730,
                            StraatNaam = "Lange baan"
                        },
                        new
                        {
                            StraatId = 2,
                            GemeenteId = 1730,
                            StraatNaam = "Achterom"
                        },
                        new
                        {
                            StraatId = 3,
                            GemeenteId = 1730,
                            StraatNaam = "Bolwerk"
                        },
                        new
                        {
                            StraatId = 4,
                            GemeenteId = 1730,
                            StraatNaam = "Dijkstraat"
                        },
                        new
                        {
                            StraatId = 5,
                            GemeenteId = 1730,
                            StraatNaam = "Winkelstraat"
                        },
                        new
                        {
                            StraatId = 6,
                            GemeenteId = 1790,
                            StraatNaam = "Kerkstraat"
                        },
                        new
                        {
                            StraatId = 7,
                            GemeenteId = 1790,
                            StraatNaam = "Eikenlaan"
                        },
                        new
                        {
                            StraatId = 8,
                            GemeenteId = 1790,
                            StraatNaam = "Kastanjedreef"
                        },
                        new
                        {
                            StraatId = 9,
                            GemeenteId = 1790,
                            StraatNaam = "Bosweg"
                        },
                        new
                        {
                            StraatId = 10,
                            GemeenteId = 1790,
                            StraatNaam = "Meibloemstraat"
                        });
                });

            modelBuilder.Entity("Model.Entities.Taal", b =>
                {
                    b.Property<int>("TaalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("TaalCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("TaalNaam")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TaalId");

                    b.HasIndex("TaalId")
                        .IsUnique();

                    b.HasIndex("TaalNaam")
                        .IsUnique();

                    b.ToTable("Talen");

                    b.HasData(
                        new
                        {
                            TaalId = 1,
                            TaalCode = "nl",
                            TaalNaam = "Nederlands"
                        },
                        new
                        {
                            TaalId = 2,
                            TaalCode = "fr",
                            TaalNaam = "Frans"
                        },
                        new
                        {
                            TaalId = 3,
                            TaalCode = "en",
                            TaalNaam = "Engels"
                        });
                });

            modelBuilder.Entity("Model.Entities.Medewerker", b =>
                {
                    b.HasBaseType("Model.Entities.Persoon");

                    b.Property<int>("AfdelingId")
                        .HasColumnType("int");

                    b.Property<int?>("PersoonId1")
                        .HasColumnType("int");

                    b.HasIndex("AfdelingId");

                    b.HasIndex("PersoonId1");

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("Model.Entities.Profiel", b =>
                {
                    b.HasBaseType("Model.Entities.Persoon");

                    b.Property<string>("BeroepTekst")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("CreatieTijdstip")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAdres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FacebookNaam")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirmaNaam")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("GoedgekeurdTijdstip")
                        .HasColumnType("datetime2");

                    b.Property<string>("KennismakingsTekst")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<DateTime>("LaatsteUpdateTijdstip")
                        .HasColumnType("datetime2");

                    b.Property<string>("WebsiteAdres")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("WoontHierSindsDatum")
                        .HasColumnType("datetime2");

                    b.ToTable("Profielen");
                });

            modelBuilder.Entity("Model.Entities.Adres", b =>
                {
                    b.HasOne("Model.Entities.Straat", "Straat")
                        .WithMany("Adressen")
                        .HasForeignKey("StraatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Straat");
                });

            modelBuilder.Entity("Model.Entities.Bericht", b =>
                {
                    b.HasOne("Model.Entities.BerichtType", "BerichtType")
                        .WithMany("Berichten")
                        .HasForeignKey("BerichtTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Profiel", "Profiel")
                        .WithMany("Berichten")
                        .HasForeignKey("GemeenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Bericht", "HoofdBericht")
                        .WithMany("Berichten")
                        .HasForeignKey("HoofdBerichtId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BerichtType");

                    b.Navigation("HoofdBericht");

                    b.Navigation("Profiel");
                });

            modelBuilder.Entity("Model.Entities.Gemeente", b =>
                {
                    b.HasOne("Model.Entities.Gemeente", "HoofdGemeente")
                        .WithMany("Gemeenten")
                        .HasForeignKey("HoofdGemeenteId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Model.Entities.Provincie", "Provincie")
                        .WithMany("Gemeenten")
                        .HasForeignKey("ProvincieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Taal", "Taal")
                        .WithMany("Gemeenten")
                        .HasForeignKey("TaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoofdGemeente");

                    b.Navigation("Provincie");

                    b.Navigation("Taal");
                });

            modelBuilder.Entity("Model.Entities.Persoon", b =>
                {
                    b.HasOne("Model.Entities.Adres", "Adres")
                        .WithMany("Personen")
                        .HasForeignKey("AdresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Gemeente", "GeboortePlaats")
                        .WithMany("Personen")
                        .HasForeignKey("GeboorteplaatsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Model.Entities.Taal", "Taal")
                        .WithMany("Personen")
                        .HasForeignKey("TaalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Adres");

                    b.Navigation("GeboortePlaats");

                    b.Navigation("Taal");
                });

            modelBuilder.Entity("Model.Entities.ProfielInteresse", b =>
                {
                    b.HasOne("Model.Entities.InteresseSoort", "InteresseSoort")
                        .WithMany("ProfielInteresses")
                        .HasForeignKey("InteresseSoortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Profiel", "Profiel")
                        .WithMany("ProfielInteresses")
                        .HasForeignKey("ProfielPersoonId");

                    b.Navigation("InteresseSoort");

                    b.Navigation("Profiel");
                });

            modelBuilder.Entity("Model.Entities.Straat", b =>
                {
                    b.HasOne("Model.Entities.Gemeente", "Gemeente")
                        .WithMany("Straten")
                        .HasForeignKey("GemeenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gemeente");
                });

            modelBuilder.Entity("Model.Entities.Medewerker", b =>
                {
                    b.HasOne("Model.Entities.Afdeling", "Afdeling")
                        .WithMany("Medewerkers")
                        .HasForeignKey("AfdelingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Persoon", null)
                        .WithOne()
                        .HasForeignKey("Model.Entities.Medewerker", "PersoonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Persoon", "Persoon")
                        .WithMany()
                        .HasForeignKey("PersoonId1");

                    b.Navigation("Afdeling");

                    b.Navigation("Persoon");
                });

            modelBuilder.Entity("Model.Entities.Profiel", b =>
                {
                    b.HasOne("Model.Entities.Persoon", null)
                        .WithOne()
                        .HasForeignKey("Model.Entities.Profiel", "PersoonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Entities.Adres", b =>
                {
                    b.Navigation("Personen");
                });

            modelBuilder.Entity("Model.Entities.Afdeling", b =>
                {
                    b.Navigation("Medewerkers");
                });

            modelBuilder.Entity("Model.Entities.Bericht", b =>
                {
                    b.Navigation("Berichten");
                });

            modelBuilder.Entity("Model.Entities.BerichtType", b =>
                {
                    b.Navigation("Berichten");
                });

            modelBuilder.Entity("Model.Entities.Gemeente", b =>
                {
                    b.Navigation("Gemeenten");

                    b.Navigation("Personen");

                    b.Navigation("Straten");
                });

            modelBuilder.Entity("Model.Entities.InteresseSoort", b =>
                {
                    b.Navigation("ProfielInteresses");
                });

            modelBuilder.Entity("Model.Entities.Provincie", b =>
                {
                    b.Navigation("Gemeenten");
                });

            modelBuilder.Entity("Model.Entities.Straat", b =>
                {
                    b.Navigation("Adressen");
                });

            modelBuilder.Entity("Model.Entities.Taal", b =>
                {
                    b.Navigation("Gemeenten");

                    b.Navigation("Personen");
                });

            modelBuilder.Entity("Model.Entities.Profiel", b =>
                {
                    b.Navigation("Berichten");

                    b.Navigation("ProfielInteresses");
                });
#pragma warning restore 612, 618
        }
    }
}
