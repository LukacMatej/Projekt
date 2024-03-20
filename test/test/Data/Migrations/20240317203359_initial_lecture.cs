using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial_lecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecture",
                columns: table => new
                {
                    IdOfLecture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfLecture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lecturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enrolled = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecture", x => x.IdOfLecture);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lecture");
        }
    }
}
