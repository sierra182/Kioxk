﻿// <auto-generated />
using System;
using Kioxk.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kioxk.Server.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Kioxk.Shared.Models.Commande", b =>
                {
                    b.Property<int>("CommandeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("Phone")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Ref")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RgtsCompl")
                        .HasColumnType("TEXT");

                    b.Property<float>("Total")
                        .HasColumnType("REAL");

                    b.Property<bool>("Valide")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommandeId");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Datetime", b =>
                {
                    b.Property<int>("DatetimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Dt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("HashsetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LivretId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DatetimeId");

                    b.HasIndex("CommandeId");

                    b.HasIndex("HashsetId");

                    b.HasIndex("LivretId");

                    b.ToTable("Datetime");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Hashset", b =>
                {
                    b.Property<int>("HashsetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LivretId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HashsetId");

                    b.HasIndex("CommandeId");

                    b.HasIndex("LivretId");

                    b.ToTable("Hashset");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Int", b =>
                {
                    b.Property<int>("IntId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CommandeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<int>("It")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LivretId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IntId");

                    b.HasIndex("CommandeId");

                    b.HasIndex("LivretId");

                    b.ToTable("Int");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Livret", b =>
                {
                    b.Property<int>("LivretId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("LivretId");

                    b.ToTable("Livret");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Datetime", b =>
                {
                    b.HasOne("Kioxk.Shared.Models.Commande", "Commande")
                        .WithMany("Selected")
                        .HasForeignKey("CommandeId");

                    b.HasOne("Kioxk.Shared.Models.Hashset", "Hashset")
                        .WithMany("Hs")
                        .HasForeignKey("HashsetId");

                    b.HasOne("Kioxk.Shared.Models.Livret", "Livret")
                        .WithMany("UnSelectable")
                        .HasForeignKey("LivretId");

                    b.Navigation("Commande");

                    b.Navigation("Hashset");

                    b.Navigation("Livret");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Hashset", b =>
                {
                    b.HasOne("Kioxk.Shared.Models.Commande", "Commande")
                        .WithMany("Seasons")
                        .HasForeignKey("CommandeId");

                    b.HasOne("Kioxk.Shared.Models.Livret", "Livret")
                        .WithMany("Seasons")
                        .HasForeignKey("LivretId");

                    b.Navigation("Commande");

                    b.Navigation("Livret");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Int", b =>
                {
                    b.HasOne("Kioxk.Shared.Models.Commande", "Commande")
                        .WithMany("Prices")
                        .HasForeignKey("CommandeId");

                    b.HasOne("Kioxk.Shared.Models.Livret", "Livret")
                        .WithMany("Prices")
                        .HasForeignKey("LivretId");

                    b.Navigation("Commande");

                    b.Navigation("Livret");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Commande", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Seasons");

                    b.Navigation("Selected");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Hashset", b =>
                {
                    b.Navigation("Hs");
                });

            modelBuilder.Entity("Kioxk.Shared.Models.Livret", b =>
                {
                    b.Navigation("Prices");

                    b.Navigation("Seasons");

                    b.Navigation("UnSelectable");
                });
#pragma warning restore 612, 618
        }
    }
}
