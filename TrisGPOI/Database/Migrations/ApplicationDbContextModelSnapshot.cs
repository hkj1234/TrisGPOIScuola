﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TrisGPOI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TrisGPOI.Database.Game.Entities.DBGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CurrentPlayer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GameType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastMoveTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Player1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Player2")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("TrisGPOI.Database.OTP.Entities.DBOtpEntity", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(95)");

                    b.Property<DateTime>("ExpiryTime")
                        .HasColumnType("datetime");

                    b.Property<string>("OtpCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Email");

                    b.ToTable("OTP");
                });

            modelBuilder.Entity("TrisGPOI.Database.User.Entities.DBUser", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FotoProfilo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrisGPOI.Database.User.Entities.DBUserVittoriePVP", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(95)");

                    b.Property<int>("GameInfinity")
                        .HasColumnType("int");

                    b.Property<int>("GameNormal")
                        .HasColumnType("int");

                    b.Property<int>("GameUltimate")
                        .HasColumnType("int");

                    b.Property<int>("LossesInfinity")
                        .HasColumnType("int");

                    b.Property<int>("LossesNormal")
                        .HasColumnType("int");

                    b.Property<int>("LossesUltimate")
                        .HasColumnType("int");

                    b.Property<int>("VictoryInfinity")
                        .HasColumnType("int");

                    b.Property<int>("VictoryNormal")
                        .HasColumnType("int");

                    b.Property<int>("VictoryUltimate")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.ToTable("UserVittoriePVP");
                });
#pragma warning restore 612, 618
        }
    }
}
