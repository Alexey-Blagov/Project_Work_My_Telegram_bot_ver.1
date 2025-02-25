﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project_Work_My_Telegram_bot;

#nullable disable

namespace Project_Work_My_Telegram_bot.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.CarDrive", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarId"));

                    b.Property<string>("CarName")
                        .HasColumnType("text");

                    b.Property<string>("CarNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("GasСonsum")
                        .HasColumnType("double precision");

                    b.Property<long?>("PersonalId")
                        .HasColumnType("bigint");

                    b.Property<int>("TypeFuel")
                        .HasColumnType("integer");

                    b.Property<bool>("isPersonalCar")
                        .HasColumnType("boolean");

                    b.HasKey("CarId");

                    b.HasAlternateKey("CarNumber");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.ToTable("CarDrives");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.ObjectPath", b =>
                {
                    b.Property<int>("IdPath")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPath"));

                    b.Property<int?>("CarId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DatePath")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ObjectName")
                        .HasColumnType("text");

                    b.Property<double?>("PathLengh")
                        .HasColumnType("double precision");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("IdPath");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("ObjectPaths");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.OtherExpenses", b =>
                {
                    b.Property<int>("ExpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ExpId"));

                    b.Property<decimal?>("Coast")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("DateTimeExp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NameExpense")
                        .HasColumnType("text");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ExpId");

                    b.HasIndex("UserId");

                    b.ToTable("OtherExpenses");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.User", b =>
                {
                    b.Property<long>("IdTg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdTg"));

                    b.Property<string>("JobTitlel")
                        .HasColumnType("text");

                    b.Property<string>("TgUserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<int>("UserRol")
                        .HasColumnType("integer");

                    b.HasKey("IdTg");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.CarDrive", b =>
                {
                    b.HasOne("Project_Work_My_Telegram_bot.ClassDB.User", "UserPersonal")
                        .WithOne("PersonalCar")
                        .HasForeignKey("Project_Work_My_Telegram_bot.ClassDB.CarDrive", "PersonalId");

                    b.Navigation("UserPersonal");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.ObjectPath", b =>
                {
                    b.HasOne("Project_Work_My_Telegram_bot.ClassDB.CarDrive", "CarDrive")
                        .WithMany("ObjectPaths")
                        .HasForeignKey("CarId");

                    b.HasOne("Project_Work_My_Telegram_bot.ClassDB.User", "UserPath")
                        .WithMany("ObjectPaths")
                        .HasForeignKey("UserId");

                    b.Navigation("CarDrive");

                    b.Navigation("UserPath");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.OtherExpenses", b =>
                {
                    b.HasOne("Project_Work_My_Telegram_bot.ClassDB.User", "UserExp")
                        .WithMany("OtherExpenses")
                        .HasForeignKey("UserId");

                    b.Navigation("UserExp");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.CarDrive", b =>
                {
                    b.Navigation("ObjectPaths");
                });

            modelBuilder.Entity("Project_Work_My_Telegram_bot.ClassDB.User", b =>
                {
                    b.Navigation("ObjectPaths");

                    b.Navigation("OtherExpenses");

                    b.Navigation("PersonalCar");
                });
#pragma warning restore 612, 618
        }
    }
}
