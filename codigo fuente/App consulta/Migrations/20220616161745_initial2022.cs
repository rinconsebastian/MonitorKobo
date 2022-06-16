using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App_consulta.Migrations
{
    public partial class initial2022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: true),
                    Apellido = table.Column<string>(type: "longtext", nullable: true),
                    IDDependencia = table.Column<int>(type: "int", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuracion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Logo = table.Column<string>(type: "longtext", nullable: true),
                    Favicon = table.Column<string>(type: "longtext", nullable: true),
                    ImgHeader = table.Column<string>(type: "longtext", nullable: true),
                    ColorTextoHeader = table.Column<string>(type: "longtext", nullable: true),
                    ColorPrincipal = table.Column<string>(type: "longtext", nullable: true),
                    ColorTextoPrincipal = table.Column<string>(type: "longtext", nullable: true),
                    Contacto = table.Column<string>(type: "longtext", nullable: false),
                    Entidad = table.Column<string>(type: "longtext", nullable: false),
                    NombrePlan = table.Column<string>(type: "longtext", nullable: false),
                    NombreApp = table.Column<string>(type: "longtext", nullable: false),
                    DescripcionApp = table.Column<string>(type: "longtext", nullable: false),
                    CodeEncuestador = table.Column<int>(type: "int", nullable: false),
                    KoboKpiUrl = table.Column<string>(type: "longtext", nullable: true),
                    KoboApiToken = table.Column<string>(type: "longtext", nullable: true),
                    KoboAssetUid = table.Column<string>(type: "longtext", nullable: true),
                    KoboAssetUidAssociation = table.Column<string>(type: "longtext", nullable: true),
                    KoboAttachment = table.Column<string>(type: "longtext", nullable: true),
                    KoboUsername = table.Column<string>(type: "longtext", nullable: true),
                    KoboParamsMap = table.Column<string>(type: "longtext", nullable: true),
                    KoboParamsMapAssociation = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormalizationConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Field = table.Column<string>(type: "longtext", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormalizationConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormalizationVariable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Group = table.Column<string>(type: "longtext", nullable: false),
                    Key = table.Column<string>(type: "longtext", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormalizationVariable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Usuario = table.Column<string>(type: "longtext", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Accion = table.Column<string>(type: "longtext", nullable: true),
                    Modelo = table.Column<string>(type: "longtext", nullable: true),
                    ValAnterior = table.Column<string>(type: "longtext", nullable: true),
                    ValNuevo = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false),
                    claim = table.Column<string>(type: "longtext", nullable: false),
                    group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RequestUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Request = table.Column<string>(type: "longtext", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "longtext", nullable: false),
                    FormalizationId = table.Column<int>(type: "int", nullable: false),
                    File = table.Column<string>(type: "longtext", nullable: true),
                    Response = table.Column<string>(type: "longtext", nullable: true),
                    AlertUser = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AlertAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdUser = table.Column<string>(type: "longtext", nullable: true),
                    AdminName = table.Column<string>(type: "longtext", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValidationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdJefe = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Editar = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsable_Responsable_IdJefe",
                        column: x => x.IdJefe,
                        principalTable: "Responsable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
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
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
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
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdParent = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Code2 = table.Column<string>(type: "longtext", nullable: false),
                    IdLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Location_IdParent",
                        column: x => x.IdParent,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_LocationLevel_IdLevel",
                        column: x => x.IdLevel,
                        principalTable: "LocationLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Formalization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdKobo = table.Column<string>(type: "longtext", nullable: true),
                    NumeroRegistro = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    FechaSolicitud = table.Column<string>(type: "longtext", nullable: false),
                    Cedula = table.Column<string>(type: "longtext", nullable: false),
                    Departamento = table.Column<string>(type: "longtext", nullable: true),
                    Municipio = table.Column<string>(type: "longtext", nullable: true),
                    Especies = table.Column<string>(type: "longtext", nullable: true),
                    AreaPesca = table.Column<string>(type: "longtext", nullable: true),
                    ArtesPesca = table.Column<string>(type: "longtext", nullable: true),
                    NombreAsociacion = table.Column<string>(type: "longtext", nullable: true),
                    ImgPescador = table.Column<string>(type: "longtext", nullable: true),
                    ImgSolicitudCarnet = table.Column<string>(type: "longtext", nullable: true),
                    ImgCertificacion = table.Column<string>(type: "longtext", nullable: true),
                    ImgCedulaAnverso = table.Column<string>(type: "longtext", nullable: true),
                    ImgCedulaReverso = table.Column<string>(type: "longtext", nullable: true),
                    ImgFirmaDigital = table.Column<string>(type: "longtext", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    IdResponsable = table.Column<int>(type: "int", nullable: false),
                    IdCreateByUser = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdLastEditByUser = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastEditDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Encuestador = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formalization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formalization_AspNetUsers_IdCreateByUser",
                        column: x => x.IdCreateByUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Formalization_AspNetUsers_IdLastEditByUser",
                        column: x => x.IdLastEditByUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Formalization_Responsable_IdResponsable",
                        column: x => x.IdResponsable,
                        principalTable: "Responsable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "97f6ff5b-6816-44fc-8e6f-bbdedd1223f9", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellido", "ConcurrencyStamp", "Email", "EmailConfirmed", "IDDependencia", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "", "05622443-5cfd-4389-8879-4523ac4c5aee", "admin@admin.com", true, 1, false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAECPDxHYYnrFlyL6ghv6NFqs7g9ZlRCuHRIgzChzRa5GDZpnwsj563VfwncgzZt+OTw==", null, false, "NNK44MKHKTBOV6DHXJ4BT2Q3SYO3WQC2", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "Configuracion",
                columns: new[] { "Id", "CodeEncuestador", "ColorPrincipal", "ColorTextoHeader", "ColorTextoPrincipal", "Contacto", "DescripcionApp", "Entidad", "Favicon", "ImgHeader", "KoboApiToken", "KoboAssetUid", "KoboAssetUidAssociation", "KoboAttachment", "KoboKpiUrl", "KoboParamsMap", "KoboParamsMapAssociation", "KoboUsername", "Logo", "NombreApp", "NombrePlan" },
                values: new object[] { 1, 0, "#4287F5", "#FFFFFF", "#0448B5", "rinconsebastian@gmail.com", "Acuerdo AUNAP - PNUD 2022", "PNUD-AUNAP", "/images/favicon.png", null, null, null, null, null, null, null, null, null, "/images/favicon1.png", "App Consulta", "Coordinación" });

            migrationBuilder.InsertData(
                table: "FormalizationConfig",
                columns: new[] { "Id", "Field", "Group", "Name", "Value" },
                values: new object[,]
                {
                    { 20, "zonaOtro", 2, "Zona pesca (Otro)", "_id" },
                    { 19, "zonaNombre", 2, "Zona pesca (Nombre)", "_id" },
                    { 18, "zonaTipo", 2, "Zona pesca (Tipo)", "_id" },
                    { 17, "Encuestador", 1, "Cedula Encuestador", "_id" },
                    { 16, "ImgFirmaDigital", 1, "Firma digital", "_id" },
                    { 14, "ImgCedulaAnverso", 1, "Foto cédula (anverso)", "_id" },
                    { 13, "ImgCertificacion", 1, "Foto certificación", "_id" },
                    { 12, "ImgSolicitudCarnet", 1, "Foto solicitud carnet", "_id" },
                    { 11, "ImgPescador", 1, "Foto pescador", "_id" },
                    { 15, "ImgCedulaReverso", 1, "Foto cédula (reverso)", "_id" },
                    { 9, "ArtesPesca", 0, "Artes de pesca", "_id" },
                    { 8, "AreaPesca", 1, "Área de pesca", "_id" },
                    { 7, "Municipio", 1, "Municipio", "_id" },
                    { 6, "Departamento", 1, "Departamento", "_id" },
                    { 5, "Cedula", 1, "Cédula pescador", "_id" },
                    { 4, "FechaSolicitud", 1, "Fecha solicitud", "_id" },
                    { 3, "Name", 1, "Nombre y apellidos", "" },
                    { 2, "NumeroRegistro", 1, "Número registro", "_id" },
                    { 1, "IdKobo", 1, "Id Kobo", "_id" },
                    { 10, "NombreAsociacion", 1, "Nombre asociación", "_id" }
                });

            migrationBuilder.InsertData(
                table: "FormalizationVariable",
                columns: new[] { "Id", "Group", "Key", "Value" },
                values: new object[,]
                {
                    { 15, "Arte", "3", "Boliche" },
                    { 16, "Arte", "4", "Chinchorra" },
                    { 17, "Arte", "5", "Chinchorro" },
                    { 18, "Arte", "6", "Cóngolo / canasta" },
                    { 19, "Arte", "7", "Línea de mano" },
                    { 24, "Arte", "12", "Otro" },
                    { 21, "Arte", "9", "Redes de enmalle" },
                    { 22, "Arte", "10", "Trampas/nasas" },
                    { 23, "Arte", "11", "Trasmallo" },
                    { 14, "Arte", "2", "Atarraya" },
                    { 20, "Arte", "8", "Palangre" },
                    { 13, "Arte", "1", "Arpón" },
                    { 12, "Zona", "14", "Embalse" },
                    { 11, "Zona", "11", "Sector de río" },
                    { 2, "Zona", "2", "Canal" },
                    { 3, "Zona", "3", "Ciénaga" },
                    { 4, "Zona", "4", "Estanque" },
                    { 5, "Zona", "5", "Laguna" },
                    { 1, "Zona", "1", "Arroyo" },
                    { 7, "Zona", "7", "Presa" },
                    { 8, "Zona", "8", "Quebrada" },
                    { 9, "Zona", "9", "Riachuelo" },
                    { 10, "Zona", "10", "Río" },
                    { 6, "Zona", "6", "Lago" }
                });

            migrationBuilder.InsertData(
                table: "Policy",
                columns: new[] { "id", "claim", "group", "nombre" },
                values: new object[,]
                {
                    { 12, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver", 5, "Ver formalización" },
                    { 21, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Solicitud.Crear", 7, "Crear solicitudes" },
                    { 20, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imagen.Restablecer", 5, "Restablecer imagenes formalización" },
                    { 19, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imagen.Cambiar", 5, "Cambiar imagenes formalización" },
                    { 18, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Exportar.Listado", 6, "Exportar listados" },
                    { 17, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imprimir", 5, "Imprimir formalización" },
                    { 15, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Listado", 5, "Informe formalización" },
                    { 14, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Validar", 5, "Validar formalización" },
                    { 13, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Editar", 5, "Editar formalización" },
                    { 16, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Usuario", 4, "Ver encuestas por usuario" },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Logs", 2, "Ver registro actividad" },
                    { 10, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Actualizar", 4, "Actualizar encuestas" },
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Administrar", 3, "Administrar encuestador" },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Editar", 3, "Editar encuestador" },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Ver", 3, "Ver encuestador" },
                    { 22, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Solicitud.Administrar", 7, "Administrar solicitudes" },
                    { 5, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Usuario.Editar", 2, "Editar usuarios" },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Rol.Editar", 2, "Editar roles" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Responsable.Editar", 2, "Editar dependencias" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Responsable", 1, "Configuración dependencia" },
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.General", 1, "Ver Configuración general" },
                    { 11, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Listado", 4, "Informe encuestas" }
                });

            migrationBuilder.InsertData(
                table: "Responsable",
                columns: new[] { "Id", "Editar", "IdJefe", "Nombre" },
                values: new object[] { 1, false, null, "Entidad" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.General", "1", "1" },
                    { 22, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Solicitud.Administrar", "1", "1" },
                    { 21, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Solicitud.Crear", "1", "1" },
                    { 20, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imagen.Restablecer", "1", "1" },
                    { 19, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imagen.Cambiar", "1", "1" },
                    { 18, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Exportar.Listado", "1", "1" },
                    { 17, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Imprimir", "1", "1" },
                    { 16, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Listado", "1", "1" },
                    { 15, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Validar", "1", "1" },
                    { 14, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Editar", "1", "1" },
                    { 13, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver", "1", "1" },
                    { 12, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Usuario", "1", "1" },
                    { 11, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Listado", "1", "1" },
                    { 10, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Actualizar", "1", "1" },
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Administrar", "1", "1" },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Editar", "1", "1" },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Ver", "1", "1" },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Logs", "1", "1" },
                    { 5, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Usuario.Editar", "1", "1" },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Rol.Editar", "1", "1" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Responsable.Editar", "1", "1" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Responsable", "1", "1" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });

            migrationBuilder.InsertData(
                table: "Responsable",
                columns: new[] { "Id", "Editar", "IdJefe", "Nombre" },
                values: new object[] { 2, false, 1, "[CDR] Coordinación" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Formalization_IdCreateByUser",
                table: "Formalization",
                column: "IdCreateByUser");

            migrationBuilder.CreateIndex(
                name: "IX_Formalization_IdLastEditByUser",
                table: "Formalization",
                column: "IdLastEditByUser");

            migrationBuilder.CreateIndex(
                name: "IX_Formalization_IdResponsable",
                table: "Formalization",
                column: "IdResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdLevel",
                table: "Location",
                column: "IdLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdParent",
                table: "Location",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_Pollster_DNI",
                table: "Pollster",
                column: "DNI",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Responsable_IdJefe",
                table: "Responsable",
                column: "IdJefe");
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
                name: "Configuracion");

            migrationBuilder.DropTable(
                name: "Formalization");

            migrationBuilder.DropTable(
                name: "FormalizationConfig");

            migrationBuilder.DropTable(
                name: "FormalizationVariable");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "Pollster");

            migrationBuilder.DropTable(
                name: "RequestUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Responsable");

            migrationBuilder.DropTable(
                name: "LocationLevel");
        }
    }
}
