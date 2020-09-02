using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VPlati.Migrations
{
    public partial class ZadnjaMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Ime = table.Column<string>(maxLength: 15, nullable: false),
                    Priimek = table.Column<string>(maxLength: 15, nullable: false),
                    PlezalecInfo = table.Column<string>(maxLength: 300, nullable: true),
                    SlikaPlezalca = table.Column<string>(nullable: true),
                    DatumRojstva = table.Column<DateTime>(nullable: false),
                    DatumRegistracije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NacinPreplezanjaSmeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nacin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPreplezanjaSmeri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OcenaSmeri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocena", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plezalisca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImePlezalisca = table.Column<string>(nullable: false),
                    Visina = table.Column<int>(nullable: false),
                    CasDostopa = table.Column<int>(nullable: false),
                    OpisDostopa = table.Column<string>(nullable: true),
                    SlikaPlezalisca = table.Column<string>(nullable: true),
                    UsmerjenostStene = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plezalisca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opozorila",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpozoriloText = table.Column<string>(nullable: false),
                    OpozoriloDatum = table.Column<DateTime>(nullable: false),
                    PlezalisceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opozorila", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opozorila_Plezalisca_PlezalisceId",
                        column: x => x.PlezalisceId,
                        principalTable: "Plezalisca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sektorji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeSektorja = table.Column<string>(nullable: false),
                    PlezalisceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sektorji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sektorji_Plezalisca_PlezalisceId",
                        column: x => x.PlezalisceId,
                        principalTable: "Plezalisca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Smeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeSmeri = table.Column<string>(nullable: false),
                    Dolzina = table.Column<int>(nullable: false),
                    PrviVzpon = table.Column<DateTime>(nullable: false),
                    Opremjevalec = table.Column<string>(nullable: true),
                    SektorId = table.Column<int>(nullable: false),
                    SteviloVzponov = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Smeri_Sektorji_SektorId",
                        column: x => x.SektorId,
                        principalTable: "Sektorji",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentarji",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KomentarText = table.Column<string>(maxLength: 300, nullable: false),
                    KomentarUser = table.Column<string>(nullable: true),
                    KomentarDatum = table.Column<DateTime>(nullable: false),
                    SmerId = table.Column<int>(nullable: false),
                    PlezalecId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentarji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentarji_AspNetUsers_PlezalecId",
                        column: x => x.PlezalecId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentarji_Smeri_SmerId",
                        column: x => x.SmerId,
                        principalTable: "Smeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcenaSmeri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OcenaSmeriDatum = table.Column<DateTime>(nullable: false),
                    NacinPreplezanjaSmeriId = table.Column<int>(nullable: false),
                    SmerId = table.Column<int>(nullable: false),
                    OcenaId = table.Column<int>(nullable: false),
                    PlezalecId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcenaSmeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OcenaSmeri_NacinPreplezanjaSmeri_NacinPreplezanjaSmeriId",
                        column: x => x.NacinPreplezanjaSmeriId,
                        principalTable: "NacinPreplezanjaSmeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcenaSmeri_Ocena_OcenaId",
                        column: x => x.OcenaId,
                        principalTable: "Ocena",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcenaSmeri_AspNetUsers_PlezalecId",
                        column: x => x.PlezalecId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OcenaSmeri_Smeri_SmerId",
                        column: x => x.SmerId,
                        principalTable: "Smeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "0e943048-19cc-42b4-99bb-a5965bfe1171", "admin", "ADMIN" },
                    { 2, "49fc6dad-6c16-4478-a574-aff9121f911b", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "NacinPreplezanjaSmeri",
                columns: new[] { "Id", "Nacin" },
                values: new object[,]
                {
                    { 1, "Flash" },
                    { 2, "Na Pogled" },
                    { 3, "Rdeča Pika" }
                });

            migrationBuilder.InsertData(
                table: "Ocena",
                columns: new[] { "Id", "OcenaSmeri" },
                values: new object[,]
                {
                    { 17, "7c" },
                    { 18, "7c+" },
                    { 19, "8a" },
                    { 20, "8a+" },
                    { 21, "8b" },
                    { 24, "8c+" },
                    { 23, "8c" },
                    { 16, "7b+" },
                    { 25, "9a" },
                    { 26, "9a+" },
                    { 27, "9b" },
                    { 22, "8b+" },
                    { 15, "7b" },
                    { 12, "6c+" },
                    { 13, "7a" },
                    { 28, "9b+" },
                    { 11, "6c" },
                    { 10, "6b+" },
                    { 9, "6b" },
                    { 8, "6a+" },
                    { 7, "6a" },
                    { 6, "5c" },
                    { 5, "5b" },
                    { 4, "5a" },
                    { 3, "4c" },
                    { 2, "4b" },
                    { 1, "4a" },
                    { 14, "7a+" },
                    { 29, "9c" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Komentarji_PlezalecId",
                table: "Komentarji",
                column: "PlezalecId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentarji_SmerId",
                table: "Komentarji",
                column: "SmerId");

            migrationBuilder.CreateIndex(
                name: "IX_OcenaSmeri_NacinPreplezanjaSmeriId",
                table: "OcenaSmeri",
                column: "NacinPreplezanjaSmeriId");

            migrationBuilder.CreateIndex(
                name: "IX_OcenaSmeri_OcenaId",
                table: "OcenaSmeri",
                column: "OcenaId");

            migrationBuilder.CreateIndex(
                name: "IX_OcenaSmeri_PlezalecId",
                table: "OcenaSmeri",
                column: "PlezalecId");

            migrationBuilder.CreateIndex(
                name: "IX_OcenaSmeri_SmerId",
                table: "OcenaSmeri",
                column: "SmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Opozorila_PlezalisceId",
                table: "Opozorila",
                column: "PlezalisceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sektorji_PlezalisceId",
                table: "Sektorji",
                column: "PlezalisceId");

            migrationBuilder.CreateIndex(
                name: "IX_Smeri_SektorId",
                table: "Smeri",
                column: "SektorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Komentarji");

            migrationBuilder.DropTable(
                name: "OcenaSmeri");

            migrationBuilder.DropTable(
                name: "Opozorila");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "NacinPreplezanjaSmeri");

            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Smeri");

            migrationBuilder.DropTable(
                name: "Sektorji");

            migrationBuilder.DropTable(
                name: "Plezalisca");
        }
    }
}
