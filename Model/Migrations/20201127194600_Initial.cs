using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afdelingen",
                columns: table => new
                {
                    AfdelingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AfdelingCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    AfdelingNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AfdelingTekst = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afdelingen", x => x.AfdelingId);
                });

            migrationBuilder.CreateTable(
                name: "BerichtTypes",
                columns: table => new
                {
                    BerichtTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BerichtTypeCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BerichtTypeNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BerichtTypeTekst = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerichtTypes", x => x.BerichtTypeId);
                });

            migrationBuilder.CreateTable(
                name: "InteresseSoort",
                columns: table => new
                {
                    InteresseSoortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InteresseSoortNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteresseSoort", x => x.InteresseSoortId);
                });

            migrationBuilder.CreateTable(
                name: "Provincies",
                columns: table => new
                {
                    ProvincieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvincieCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Provincienaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincies", x => x.ProvincieId);
                });

            migrationBuilder.CreateTable(
                name: "Talen",
                columns: table => new
                {
                    TaalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaalCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TaalNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talen", x => x.TaalId);
                });

            migrationBuilder.CreateTable(
                name: "Gemeenten",
                columns: table => new
                {
                    GemeenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GemeenteNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postcode = table.Column<int>(type: "int", nullable: false),
                    ProvincieId = table.Column<int>(type: "int", nullable: false),
                    HoofdGemeenteId = table.Column<int>(type: "int", nullable: true),
                    TaalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gemeenten", x => x.GemeenteId);
                    table.ForeignKey(
                        name: "FK_Gemeenten_Gemeenten_HoofdGemeenteId",
                        column: x => x.HoofdGemeenteId,
                        principalTable: "Gemeenten",
                        principalColumn: "GemeenteId");
                    table.ForeignKey(
                        name: "FK_Gemeenten_Provincies_ProvincieId",
                        column: x => x.ProvincieId,
                        principalTable: "Provincies",
                        principalColumn: "ProvincieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gemeenten_Talen_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Talen",
                        principalColumn: "TaalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Straten",
                columns: table => new
                {
                    StraatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StraatNaam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GemeenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Straten", x => x.StraatId);
                    table.ForeignKey(
                        name: "FK_Straten_Gemeenten_GemeenteId",
                        column: x => x.GemeenteId,
                        principalTable: "Gemeenten",
                        principalColumn: "GemeenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    AdresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StraatId = table.Column<int>(type: "int", nullable: false),
                    HuisNr = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    BusNr = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Aangepast = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.AdresId);
                    table.ForeignKey(
                        name: "FK_Adressen_Straten_StraatId",
                        column: x => x.StraatId,
                        principalTable: "Straten",
                        principalColumn: "StraatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personen",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", maxLength: 2, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoorNaam = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FamilieNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    GeboorteplaatsId = table.Column<int>(type: "int", nullable: false),
                    TelefoonNr = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LoginNaam = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LoginPaswoord = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TaalId = table.Column<int>(type: "int", nullable: false),
                    Aangepast = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    Geblokkeerd = table.Column<bool>(type: "bit", maxLength: 50, nullable: false),
                    LoginAantal = table.Column<int>(type: "int", nullable: false),
                    VerkeerdeLoginsAantal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personen", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Personen_Adressen_AdresId",
                        column: x => x.AdresId,
                        principalTable: "Adressen",
                        principalColumn: "AdresId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personen_Gemeenten_GeboorteplaatsId",
                        column: x => x.GeboorteplaatsId,
                        principalTable: "Gemeenten",
                        principalColumn: "GemeenteId");
                    table.ForeignKey(
                        name: "FK_Personen_Talen_TaalId",
                        column: x => x.TaalId,
                        principalTable: "Talen",
                        principalColumn: "TaalId");
                });

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    AfdelingId = table.Column<int>(type: "int", nullable: false),
                    PersoonId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Afdelingen_AfdelingId",
                        column: x => x.AfdelingId,
                        principalTable: "Afdelingen",
                        principalColumn: "AfdelingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Personen_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medewerkers_Personen_PersoonId1",
                        column: x => x.PersoonId1,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profielen",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    KennismakingsTekst = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    WoontHierSindsDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeroepTekst = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FirmaNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WebsiteAdres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailAdres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FacebookNaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GoedgekeurdTijdstip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatieTijdstip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaatsteUpdateTijdstip = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profielen", x => x.PersoonId);
                    table.ForeignKey(
                        name: "FK_Profielen_Personen_PersoonId",
                        column: x => x.PersoonId,
                        principalTable: "Personen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Berichten",
                columns: table => new
                {
                    BerichtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoofdBerichtId = table.Column<int>(type: "int", nullable: false),
                    GemeenteId = table.Column<int>(type: "int", nullable: false),
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    BerichtTypeId = table.Column<int>(type: "int", nullable: false),
                    BerichtTijdstip = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BerichtTitel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BerichtTekst = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Berichten", x => x.BerichtId);
                    table.ForeignKey(
                        name: "FK_Berichten_Berichten_HoofdBerichtId",
                        column: x => x.HoofdBerichtId,
                        principalTable: "Berichten",
                        principalColumn: "BerichtId");
                    table.ForeignKey(
                        name: "FK_Berichten_BerichtTypes_BerichtTypeId",
                        column: x => x.BerichtTypeId,
                        principalTable: "BerichtTypes",
                        principalColumn: "BerichtTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Berichten_Profielen_GemeenteId",
                        column: x => x.GemeenteId,
                        principalTable: "Profielen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfielInteresses",
                columns: table => new
                {
                    PersoonId = table.Column<int>(type: "int", nullable: false),
                    InteresseSoortId = table.Column<int>(type: "int", nullable: false),
                    ProfielInteresseTekst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aangepast = table.Column<byte[]>(type: "timestamp", rowVersion: true, nullable: true),
                    ProfielPersoonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfielInteresses", x => new { x.PersoonId, x.InteresseSoortId });
                    table.ForeignKey(
                        name: "FK_ProfielInteresses_InteresseSoort_InteresseSoortId",
                        column: x => x.InteresseSoortId,
                        principalTable: "InteresseSoort",
                        principalColumn: "InteresseSoortId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfielInteresses_Profielen_ProfielPersoonId",
                        column: x => x.ProfielPersoonId,
                        principalTable: "Profielen",
                        principalColumn: "PersoonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Afdelingen",
                columns: new[] { "AfdelingId", "AfdelingCode", "AfdelingNaam", "AfdelingTekst" },
                values: new object[,]
                {
                    { 1, "VERK", "Verkoop", null },
                    { 2, "BOEK", "Boekhouding", null },
                    { 3, "AANK", "Aankoop", null }
                });

            migrationBuilder.InsertData(
                table: "BerichtTypes",
                columns: new[] { "BerichtTypeId", "BerichtTypeCode", "BerichtTypeNaam", "BerichtTypeTekst" },
                values: new object[,]
                {
                    { 11, "GH", "Gezondheid", "" },
                    { 10, "HD", "Huisdieren", "" },
                    { 9, "BS", "Babysit", "" },
                    { 8, "MD", "Melding", "" },
                    { 6, "WG", "Weggeven", "" },
                    { 7, "AC", "Activiteit", "" },
                    { 4, "ID", "Idee", "" },
                    { 3, "IZ", "Ik zoek", "" },
                    { 2, "TK", "Te koop", "" },
                    { 1, "AL", "Algemeen", "" },
                    { 5, "LN", "Lenen", "" }
                });

            migrationBuilder.InsertData(
                table: "InteresseSoort",
                columns: new[] { "InteresseSoortId", "InteresseSoortNaam" },
                values: new object[,]
                {
                    { 7, "Muziek Beluisteren" },
                    { 10, "Zwemmen" },
                    { 9, "Fietsen" },
                    { 8, "Natuur" },
                    { 6, "Muziek Spelen" },
                    { 5, "ICT" },
                    { 4, "Tv Kijken" },
                    { 3, "Klussen" },
                    { 2, "Wandelen" },
                    { 1, "Vrijwilligerswerk" }
                });

            migrationBuilder.InsertData(
                table: "Provincies",
                columns: new[] { "ProvincieId", "ProvincieCode", "Provincienaam" },
                values: new object[,]
                {
                    { 10, "NAM", "Namen" },
                    { 9, "LUX", "Luxemburg" },
                    { 8, "LUI", "Luik" },
                    { 7, "HEN", "Henegouwen" },
                    { 6, "WBR", "Waals-Brabant" },
                    { 5, "WVL", "West-Vlaanderen" },
                    { 4, "VBR", "Vlaams-Brabant" },
                    { 3, "OVL", "Oost-Vlaanderen" },
                    { 2, "LIM", "Limburg" },
                    { 1, "ANT", "Antwerpen" }
                });

            migrationBuilder.InsertData(
                table: "Talen",
                columns: new[] { "TaalId", "TaalCode", "TaalNaam" },
                values: new object[,]
                {
                    { 1, "nl", "Nederlands" },
                    { 2, "fr", "Frans" },
                    { 3, "en", "Engels" }
                });

            migrationBuilder.InsertData(
                table: "Gemeenten",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "Postcode", "ProvincieId", "TaalId" },
                values: new object[] { 1730, "Beernem", null, 8730, 5, 1 });

            migrationBuilder.InsertData(
                table: "Gemeenten",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "Postcode", "ProvincieId", "TaalId" },
                values: new object[] { 1790, "Oostkamp", null, 8020, 5, 1 });

            migrationBuilder.InsertData(
                table: "Gemeenten",
                columns: new[] { "GemeenteId", "GemeenteNaam", "HoofdGemeenteId", "Postcode", "ProvincieId", "TaalId" },
                values: new object[,]
                {
                    { 1731, "Oedelem", 1730, 8730, 5, 1 },
                    { 1732, "Sint-Joris", 1730, 8730, 5, 1 },
                    { 1791, "Hertsberge", 1790, 8020, 5, 1 },
                    { 1792, "Ruddervoorde", 1790, 8020, 5, 1 },
                    { 1793, "Waardamme", 1790, 8020, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Straten",
                columns: new[] { "StraatId", "GemeenteId", "StraatNaam" },
                values: new object[,]
                {
                    { 1, 1730, "Lange baan" },
                    { 2, 1730, "Achterom" },
                    { 3, 1730, "Bolwerk" },
                    { 4, 1730, "Dijkstraat" },
                    { 5, 1730, "Winkelstraat" },
                    { 6, 1790, "Kerkstraat" },
                    { 7, 1790, "Eikenlaan" },
                    { 8, 1790, "Kastanjedreef" },
                    { 9, 1790, "Bosweg" },
                    { 10, 1790, "Meibloemstraat" }
                });

            migrationBuilder.InsertData(
                table: "Adressen",
                columns: new[] { "AdresId", "BusNr", "HuisNr", "StraatId" },
                values: new object[] { 1, "b", "69", 1 });

            migrationBuilder.InsertData(
                table: "Adressen",
                columns: new[] { "AdresId", "BusNr", "HuisNr", "StraatId" },
                values: new object[] { 2, "7", "88", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Adressen_StraatId_HuisNr_BusNr",
                table: "Adressen",
                columns: new[] { "StraatId", "HuisNr", "BusNr" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Afdelingen_AfdelingCode",
                table: "Afdelingen",
                column: "AfdelingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Afdelingen_AfdelingNaam",
                table: "Afdelingen",
                column: "AfdelingNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_BerichtTypeId",
                table: "Berichten",
                column: "BerichtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_GemeenteId",
                table: "Berichten",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Berichten_HoofdBerichtId",
                table: "Berichten",
                column: "HoofdBerichtId");

            migrationBuilder.CreateIndex(
                name: "IX_BerichtTypes_BerichtTypeCode",
                table: "BerichtTypes",
                column: "BerichtTypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gemeenten_GemeenteNaam",
                table: "Gemeenten",
                column: "GemeenteNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gemeenten_HoofdGemeenteId",
                table: "Gemeenten",
                column: "HoofdGemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Gemeenten_ProvincieId",
                table: "Gemeenten",
                column: "ProvincieId");

            migrationBuilder.CreateIndex(
                name: "IX_Gemeenten_TaalId",
                table: "Gemeenten",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_AfdelingId",
                table: "Medewerkers",
                column: "AfdelingId");

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_PersoonId1",
                table: "Medewerkers",
                column: "PersoonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_AdresId",
                table: "Personen",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_GeboorteplaatsId",
                table: "Personen",
                column: "GeboorteplaatsId");

            migrationBuilder.CreateIndex(
                name: "IX_Personen_LoginNaam",
                table: "Personen",
                column: "LoginNaam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personen_TaalId",
                table: "Personen",
                column: "TaalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfielInteresses_InteresseSoortId",
                table: "ProfielInteresses",
                column: "InteresseSoortId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfielInteresses_ProfielPersoonId",
                table: "ProfielInteresses",
                column: "ProfielPersoonId");

            migrationBuilder.CreateIndex(
                name: "IX_Straten_GemeenteId",
                table: "Straten",
                column: "GemeenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Talen_TaalId",
                table: "Talen",
                column: "TaalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talen_TaalNaam",
                table: "Talen",
                column: "TaalNaam",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Berichten");

            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropTable(
                name: "ProfielInteresses");

            migrationBuilder.DropTable(
                name: "BerichtTypes");

            migrationBuilder.DropTable(
                name: "Afdelingen");

            migrationBuilder.DropTable(
                name: "InteresseSoort");

            migrationBuilder.DropTable(
                name: "Profielen");

            migrationBuilder.DropTable(
                name: "Personen");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Straten");

            migrationBuilder.DropTable(
                name: "Gemeenten");

            migrationBuilder.DropTable(
                name: "Provincies");

            migrationBuilder.DropTable(
                name: "Talen");
        }
    }
}
