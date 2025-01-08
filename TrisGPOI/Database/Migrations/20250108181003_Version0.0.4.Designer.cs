﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TrisGPOI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250108181003_Version0.0.4")]
    partial class Version004
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TrisGPOI.Core.ReceiveBox.Entities.DBReceiveBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ReceiveBox");
                });

            modelBuilder.Entity("TrisGPOI.Database.Friend.Entities.DBFriend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("User1Email")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<string>("User2Email")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.HasKey("Id");

                    b.HasIndex("User1Email");

                    b.HasIndex("User2Email");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("TrisGPOI.Database.Friend.Entities.DBFriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ReceiverEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FriendRequest");
                });

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

                    b.Property<string>("Winning")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

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

            modelBuilder.Entity("TrisGPOI.Database.Report.Entities.DBReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ReportMessage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReportTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("TrisGPOI.Database.User.Entities.DBUser", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("FotoProfilo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MoneyXO")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RewardRemain")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StatusNumber")
                        .HasColumnType("int");

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

            modelBuilder.Entity("TrisGPOI.Database.Friend.Entities.DBFriend", b =>
                {
                    b.HasOne("TrisGPOI.Database.User.Entities.DBUser", "User1")
                        .WithMany()
                        .HasForeignKey("User1Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrisGPOI.Database.User.Entities.DBUser", "User2")
                        .WithMany()
                        .HasForeignKey("User2Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });
#pragma warning restore 612, 618
        }
    }
}
