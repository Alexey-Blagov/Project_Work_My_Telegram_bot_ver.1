using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Project_Work_My_Telegram_bot.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdTg = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TgUserName = table.Column<string>(type: "text", nullable: false),
                    UserRol = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    JobTitlel = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdTg);
                });

            migrationBuilder.CreateTable(
                name: "CarDrives",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarName = table.Column<string>(type: "text", nullable: true),
                    isPersonalCar = table.Column<bool>(type: "boolean", nullable: false),
                    CarNumber = table.Column<string>(type: "text", nullable: false),
                    GasСonsum = table.Column<double>(type: "double precision", nullable: true),
                    TypeFuel = table.Column<int>(type: "integer", nullable: false),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDrives", x => x.CarId);
                    table.UniqueConstraint("AK_CarDrives_CarNumber", x => x.CarNumber);
                    table.ForeignKey(
                        name: "FK_CarDrives_Users_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Users",
                        principalColumn: "IdTg");
                });

            migrationBuilder.CreateTable(
                name: "OtherExpenses",
                columns: table => new
                {
                    ExpId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameExpense = table.Column<string>(type: "text", nullable: true),
                    Coast = table.Column<decimal>(type: "numeric", nullable: true),
                    DateTimeExp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherExpenses", x => x.ExpId);
                    table.ForeignKey(
                        name: "FK_OtherExpenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "IdTg");
                });

            migrationBuilder.CreateTable(
                name: "ObjectPaths",
                columns: table => new
                {
                    IdPath = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectName = table.Column<string>(type: "text", nullable: true),
                    PathLengh = table.Column<double>(type: "double precision", nullable: true),
                    DatePath = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectPaths", x => x.IdPath);
                    table.ForeignKey(
                        name: "FK_ObjectPaths_CarDrives_CarId",
                        column: x => x.CarId,
                        principalTable: "CarDrives",
                        principalColumn: "CarId");
                    table.ForeignKey(
                        name: "FK_ObjectPaths_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "IdTg");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDrives_PersonalId",
                table: "CarDrives",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPaths_CarId",
                table: "ObjectPaths",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectPaths_UserId",
                table: "ObjectPaths",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherExpenses_UserId",
                table: "OtherExpenses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectPaths");

            migrationBuilder.DropTable(
                name: "OtherExpenses");

            migrationBuilder.DropTable(
                name: "CarDrives");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
