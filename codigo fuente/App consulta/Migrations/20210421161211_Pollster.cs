using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App_consulta.Migrations
{
    public partial class Pollster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeEncuestador",
                table: "Configuracion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pollster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "longtext", nullable: true),
                    IdLocation = table.Column<int>(type: "int", nullable: false),
                    IdResponsable = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "varchar(255)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pollster", x => x.Id);
                    table.UniqueConstraint("AK_Pollster_DNI", x => x.DNI);
                    table.ForeignKey(
                        name: "FK_Pollster_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pollster_Location_IdLocation",
                        column: x => x.IdLocation,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pollster_Responsable_IdResponsable",
                        column: x => x.IdResponsable,
                        principalTable: "Responsable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pollster_IdLocation",
                table: "Pollster",
                column: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Pollster_IdResponsable",
                table: "Pollster",
                column: "IdResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Pollster_IdUser",
                table: "Pollster",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pollster");

            migrationBuilder.DropColumn(
                name: "CodeEncuestador",
                table: "Configuracion");
        }
    }
}
