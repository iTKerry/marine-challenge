using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Vessel",
                schema: "dbo",
                columns: table => new
                {
                    IMO = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessel", x => x.IMO);
                });

            migrationBuilder.CreateTable(
                name: "Trace",
                schema: "dbo",
                columns: table => new
                {
                    TraceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IMO = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()"),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GetDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trace", x => x.TraceId);
                    table.ForeignKey(
                        name: "FK_Trace_Vessel_IMO",
                        column: x => x.IMO,
                        principalSchema: "dbo",
                        principalTable: "Vessel",
                        principalColumn: "IMO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trace_IMO",
                schema: "dbo",
                table: "Trace",
                column: "IMO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trace",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vessel",
                schema: "dbo");
        }
    }
}
