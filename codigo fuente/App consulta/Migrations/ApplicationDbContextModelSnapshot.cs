﻿// <auto-generated />
using System;
using App_consulta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App_consulta.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("App_consulta.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "97f6ff5b-6816-44fc-8e6f-bbdedd1223f9",
                            Name = "Administrador",
                            NormalizedName = "ADMINISTRADOR"
                        });
                });

            modelBuilder.Entity("App_consulta.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("IDDependencia")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            Apellido = "",
                            ConcurrencyStamp = "05622443-5cfd-4389-8879-4523ac4c5aee",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            IDDependencia = 1,
                            LockoutEnabled = false,
                            Nombre = "Admin",
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAECPDxHYYnrFlyL6ghv6NFqs7g9ZlRCuHRIgzChzRa5GDZpnwsj563VfwncgzZt+OTw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "NNK44MKHKTBOV6DHXJ4BT2Q3SYO3WQC2",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        });
                });

            modelBuilder.Entity("App_consulta.Models.Configuracion", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CodeEncuestador")
                        .HasColumnType("int");

                    b.Property<string>("Entidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Favicon")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgBackgroud")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgHeader")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboApiToken")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboAssetUid")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboAttachment")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboKpiUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboParamsMap")
                        .HasColumnType("longtext");

                    b.Property<string>("KoboUsername")
                        .HasColumnType("longtext");

                    b.Property<string>("Logo")
                        .HasColumnType("longtext");

                    b.Property<string>("NombrePlan")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("activo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("colorPrincipal")
                        .HasColumnType("longtext");

                    b.Property<string>("colorTextoHeader")
                        .HasColumnType("longtext");

                    b.Property<string>("colorTextoPrincipal")
                        .HasColumnType("longtext");

                    b.Property<string>("contacto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("libre")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.ToTable("Configuracion");

                    b.HasData(
                        new
                        {
                            id = 1,
                            CodeEncuestador = 0,
                            Entidad = "Entidad",
                            Logo = "/images/SIE.png",
                            NombrePlan = "Plan",
                            activo = true,
                            colorPrincipal = "#52a3a1",
                            colorTextoHeader = "#ffffff",
                            colorTextoPrincipal = "#00000",
                            contacto = "rinconsebastian@gmail.com",
                            libre = true
                        });
                });

            modelBuilder.Entity("App_consulta.Models.Formalization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AreaPesca")
                        .HasColumnType("longtext");

                    b.Property<string>("ArtesPesca")
                        .HasColumnType("longtext");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Departamento")
                        .HasColumnType("longtext");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<string>("FechaSolicitud")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("IdKobo")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgCedulaAnverso")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgCedulaReverso")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgCertificacion")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgFirmaDigital")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgPescador")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgSolicitudCarnet")
                        .HasColumnType("longtext");

                    b.Property<string>("Municipio")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreAsociacion")
                        .HasColumnType("longtext");

                    b.Property<string>("NumeroRegistro")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Formalization");
                });

            modelBuilder.Entity("App_consulta.Models.FormalizationConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FormalizationConfig");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Field = "IdKobo",
                            Name = "Id Kobo",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 2,
                            Field = "NumeroRegistro",
                            Name = "Número registro",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 3,
                            Field = "Name",
                            Name = "Nombre y apellidos",
                            Value = ""
                        },
                        new
                        {
                            Id = 4,
                            Field = "FechaSolicitud",
                            Name = "Fecha solicitud",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 5,
                            Field = "Cedula",
                            Name = "Cédula pescador",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 6,
                            Field = "Departamento",
                            Name = "Departamento",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 7,
                            Field = "Municipio",
                            Name = "Municipio",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 8,
                            Field = "AreaPesca",
                            Name = "Área de pesca",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 9,
                            Field = "ArtesPesca",
                            Name = "Artes de pesca",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 10,
                            Field = "NombreAsociacion",
                            Name = "Nombre asociación",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 11,
                            Field = "ImgPescador",
                            Name = "Foto pescador",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 12,
                            Field = "ImgSolicitudCarnet",
                            Name = "Foto solicitud carnet",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 13,
                            Field = "ImgCertificacion",
                            Name = "Foto certificación",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 14,
                            Field = "ImgCedulaAnverso",
                            Name = "Foto cédula (anverso)",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 15,
                            Field = "ImgCedulaReverso",
                            Name = "Foto cédula (reverso)",
                            Value = "_id"
                        },
                        new
                        {
                            Id = 16,
                            Field = "ImgFirmaDigital",
                            Name = "Firma digital",
                            Value = "_id"
                        });
                });

            modelBuilder.Entity("App_consulta.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("IdLevel")
                        .HasColumnType("int");

                    b.Property<int?>("IdParent")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdLevel");

                    b.HasIndex("IdParent");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("App_consulta.Models.LocationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LocationLevel");
                });

            modelBuilder.Entity("App_consulta.Models.LogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Accion")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Modelo")
                        .HasColumnType("longtext");

                    b.Property<string>("Usuario")
                        .HasColumnType("longtext");

                    b.Property<string>("ValAnterior")
                        .HasColumnType("longtext");

                    b.Property<string>("ValNuevo")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("App_consulta.Models.Policy", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("claim")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("group")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Policy");

                    b.HasData(
                        new
                        {
                            id = 1,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.General",
                            group = 1,
                            nombre = "Ver Configuración general"
                        },
                        new
                        {
                            id = 2,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Responsable",
                            group = 1,
                            nombre = "Configuración dependencia"
                        },
                        new
                        {
                            id = 3,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Responsable.Editar",
                            group = 2,
                            nombre = "Editar dependencias"
                        },
                        new
                        {
                            id = 4,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Rol.Editar",
                            group = 2,
                            nombre = "Editar roles"
                        },
                        new
                        {
                            id = 5,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Usuario.Editar",
                            group = 2,
                            nombre = "Editar usuarios"
                        },
                        new
                        {
                            id = 6,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Logs",
                            group = 2,
                            nombre = "Ver registro actividad"
                        },
                        new
                        {
                            id = 8,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Ver",
                            group = 3,
                            nombre = "Ver encuestador"
                        },
                        new
                        {
                            id = 7,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Editar",
                            group = 3,
                            nombre = "Editar encuestador"
                        },
                        new
                        {
                            id = 9,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Administrar",
                            group = 3,
                            nombre = "Administrar encuestador"
                        },
                        new
                        {
                            id = 10,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Actualizar",
                            group = 4,
                            nombre = "Actualizar encuestas"
                        },
                        new
                        {
                            id = 11,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Listado",
                            group = 4,
                            nombre = "Informe Encuestas"
                        },
                        new
                        {
                            id = 16,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestas.Usuario",
                            group = 4,
                            nombre = "Ver Encuestas por usuario"
                        },
                        new
                        {
                            id = 12,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Ver",
                            group = 5,
                            nombre = "Ver formalización"
                        },
                        new
                        {
                            id = 13,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Editar",
                            group = 5,
                            nombre = "Editar formalización"
                        },
                        new
                        {
                            id = 14,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Validar",
                            group = 5,
                            nombre = "Validar formalización"
                        },
                        new
                        {
                            id = 15,
                            claim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Formalizacion.Listado",
                            group = 5,
                            nombre = "Informe Formalización"
                        });
                });

            modelBuilder.Entity("App_consulta.Models.Pollster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DNI")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<int>("IdLocation")
                        .HasColumnType("int");

                    b.Property<int>("IdResponsable")
                        .HasColumnType("int");

                    b.Property<string>("IdUser")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DNI")
                        .IsUnique();

                    b.HasIndex("IdLocation");

                    b.HasIndex("IdResponsable");

                    b.HasIndex("IdUser");

                    b.ToTable("Pollster");
                });

            modelBuilder.Entity("App_consulta.Models.Responsable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Editar")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("IdJefe")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdJefe");

                    b.ToTable("Responsable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Editar = true,
                            Nombre = "Entidad"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.General",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Responsable",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Responsable.Editar",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Rol.Editar",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 5,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Usuario.Editar",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 6,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Configuracion.Logs",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 7,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Editar",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 8,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Ver",
                            ClaimValue = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            Id = 9,
                            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Encuestador.Administrar",
                            ClaimValue = "1",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("App_consulta.Models.Location", b =>
                {
                    b.HasOne("App_consulta.Models.LocationLevel", "LocationLevel")
                        .WithMany()
                        .HasForeignKey("IdLevel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_consulta.Models.Location", "LocationParent")
                        .WithMany()
                        .HasForeignKey("IdParent");

                    b.Navigation("LocationLevel");

                    b.Navigation("LocationParent");
                });

            modelBuilder.Entity("App_consulta.Models.Pollster", b =>
                {
                    b.HasOne("App_consulta.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("IdLocation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_consulta.Models.Responsable", "Responsable")
                        .WithMany()
                        .HasForeignKey("IdResponsable")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_consulta.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("Location");

                    b.Navigation("Responsable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App_consulta.Models.Responsable", b =>
                {
                    b.HasOne("App_consulta.Models.Responsable", "ResponsableJefe")
                        .WithMany("Hijos")
                        .HasForeignKey("IdJefe");

                    b.Navigation("ResponsableJefe");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("App_consulta.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("App_consulta.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("App_consulta.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("App_consulta.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App_consulta.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("App_consulta.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("App_consulta.Models.Responsable", b =>
                {
                    b.Navigation("Hijos");
                });
#pragma warning restore 612, 618
        }
    }
}
