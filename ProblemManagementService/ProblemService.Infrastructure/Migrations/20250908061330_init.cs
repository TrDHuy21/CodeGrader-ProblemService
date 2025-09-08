using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProblemService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Promt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InOutExample",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputExample = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InOutExample", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InOutExample_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProblemTag",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemTag", x => new { x.ProblemId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProblemTag_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProblemTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Problem",
                columns: new[] { "Id", "Content", "IsDelete", "Level", "Name", "Promt" },
                values: new object[,]
                {
                    { 1, "Sum two numbers", false, 1, "Sum", "Sum prompt" },
                    { 2, "Multiply two numbers", false, 2, "Multiply", "Multiply prompt" },
                    { 3, "Check palindrome", false, 3, "Palindrome", "Palindrome prompt" },
                    { 4, "Sort a list", false, 2, "Sort", "Sort prompt" },
                    { 5, "Find max value", false, 1, "Max", "Max prompt" },
                    { 6, "Calculate factorial", false, 2, "Factorial", "Factorial prompt" },
                    { 7, "Reverse a string", false, 1, "Reverse String", "Reverse string prompt" },
                    { 8, "Generate Fibonacci sequence", false, 3, "Fibonacci", "Fibonacci prompt" },
                    { 9, "Search element in array", false, 2, "Search", "Search prompt" },
                    { 10, "Count number of vowels in a string", false, 1, "Count Vowels", "Count vowels prompt" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 1, false, "Math" },
                    { 2, false, "String" },
                    { 3, false, "Array" },
                    { 4, false, "Beginner" },
                    { 5, false, "Intermediate" },
                    { 6, false, "Recursion" },
                    { 7, false, "Loop" },
                    { 8, false, "Search" },
                    { 9, false, "String Manipulation" },
                    { 10, false, "Counting" }
                });

            migrationBuilder.InsertData(
                table: "InOutExample",
                columns: new[] { "Id", "Explanation", "InputExample", "IsDelete", "OutputExample", "ProblemId" },
                values: new object[,]
                {
                    { 1, "1+2=3", "1 2", false, "3", 1 },
                    { 2, "2*3=6", "2 3", false, "6", 2 },
                    { 3, "Palindrome", "madam", false, "Yes", 3 },
                    { 4, "Sorted", "3 1 2", false, "1 2 3", 4 },
                    { 5, "Max is 9", "7 3 9", false, "9", 5 },
                    { 6, "5! = 120", "5", false, "120", 6 },
                    { 7, "Reversed string", "hello", false, "olleh", 7 },
                    { 8, "First 5 Fibonacci numbers", "5", false, "0 1 1 2 3", 8 },
                    { 9, "Index of 7 is 2", "4 2 7 1 9, target: 7", false, "2", 9 },
                    { 10, "Vowels are e, u, a, i, o", "education", false, "5", 10 }
                });

            migrationBuilder.InsertData(
                table: "ProblemTag",
                columns: new[] { "ProblemId", "TagId", "IsDelete" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 1, 2, false },
                    { 2, 1, false },
                    { 3, 2, false },
                    { 4, 3, false },
                    { 5, 1, false },
                    { 6, 6, false },
                    { 7, 9, false },
                    { 8, 6, false },
                    { 9, 8, false },
                    { 10, 10, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InOutExample_ProblemId",
                table: "InOutExample",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemTag_TagId",
                table: "ProblemTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InOutExample");

            migrationBuilder.DropTable(
                name: "ProblemTag");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
