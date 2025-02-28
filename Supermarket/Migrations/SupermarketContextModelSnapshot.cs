﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Supermarket.Model;

#nullable disable

namespace Supermarket.Migrations
{
    [DbContext(typeof(SupermarketContext))]
    partial class SupermarketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Supermarket.Model.Bon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CasierId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEliberare")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SumaIncasata")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CasierId");

                    b.ToTable("Bonuri");
                });

            modelBuilder.Entity("Supermarket.Model.BonProdus", b =>
                {
                    b.Property<int>("BonId")
                        .HasColumnType("int");

                    b.Property<int>("ProdusId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cantitate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BonId", "ProdusId");

                    b.HasIndex("ProdusId");

                    b.ToTable("BonProduse");
                });

            modelBuilder.Entity("Supermarket.Model.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nume")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorii");
                });

            modelBuilder.Entity("Supermarket.Model.Producator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaraOrigine")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Producatori");
                });

            modelBuilder.Entity("Supermarket.Model.Produs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("CodBare")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Nume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProducatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("ProducatorId");

                    b.ToTable("Produse");
                });

            modelBuilder.Entity("Supermarket.Model.Stoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cantitate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataAprovizionare")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataExpirare")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("PretAchizitie")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PretVanzare")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProdusId")
                        .HasColumnType("int");

                    b.Property<string>("UnitateMasura")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProdusId");

                    b.ToTable("Stocuri");
                });

            modelBuilder.Entity("Supermarket.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NumeUtilizator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parola")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipUtilizator")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Utilizatori");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NumeUtilizator = "admin",
                            Parola = "admin",
                            TipUtilizator = "admin"
                        },
                        new
                        {
                            Id = 2,
                            NumeUtilizator = "casier",
                            Parola = "casier",
                            TipUtilizator = "casier"
                        });
                });

            modelBuilder.Entity("Supermarket.Model.Bon", b =>
                {
                    b.HasOne("Supermarket.Model.User", "Casier")
                        .WithMany()
                        .HasForeignKey("CasierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Casier");
                });

            modelBuilder.Entity("Supermarket.Model.BonProdus", b =>
                {
                    b.HasOne("Supermarket.Model.Bon", "Bon")
                        .WithMany("BonProduse")
                        .HasForeignKey("BonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Model.Produs", "Produs")
                        .WithMany()
                        .HasForeignKey("ProdusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bon");

                    b.Navigation("Produs");
                });

            modelBuilder.Entity("Supermarket.Model.Produs", b =>
                {
                    b.HasOne("Supermarket.Model.Categorie", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supermarket.Model.Producator", "Producator")
                        .WithMany()
                        .HasForeignKey("ProducatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Producator");
                });

            modelBuilder.Entity("Supermarket.Model.Stoc", b =>
                {
                    b.HasOne("Supermarket.Model.Produs", "Produs")
                        .WithMany()
                        .HasForeignKey("ProdusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produs");
                });

            modelBuilder.Entity("Supermarket.Model.Bon", b =>
                {
                    b.Navigation("BonProduse");
                });
#pragma warning restore 612, 618
        }
    }
}
