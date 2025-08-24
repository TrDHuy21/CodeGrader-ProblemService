using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProblemService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Problem",
                columns: new[] { "Id", "Content", "IsDelete", "Level", "Name", "Promt" },
                values: new object[,]
                {
                    { 1, "Sum two numbers", false, 1, "Sum", "Sum prompt" },
                    { 2, "Multiply two numbers", false, 2, "Multiply", "Multiply prompt" },
                    { 3, "Check palindrome", false, 3, "Palindrome", "Palindrome prompt" },
                    { 4, "Sort a list", false, 2, "Sort", "Sort prompt" },
                    { 5, "Find max value", false, 1, "Max", "Max prompt" }
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
                    { 5, false, "Intermediate" }
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
                    { 5, "Max is 9", "7 3 9", false, "9", 5 }
                });

            migrationBuilder.InsertData(
                table: "ProblemTag",
                columns: new[] { "ProblemId", "IsDelete", "TagId" },
                values: new object[,]
                {
                    { 1, false, 1 },
                    { 2, false, 1 },
                    { 3, false, 2 },
                    { 4, false, 3 },
                    { 5, false, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InOutExample",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InOutExample",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InOutExample",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InOutExample",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InOutExample",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProblemTag",
                keyColumn: "ProblemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProblemTag",
                keyColumn: "ProblemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProblemTag",
                keyColumn: "ProblemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProblemTag",
                keyColumn: "ProblemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProblemTag",
                keyColumn: "ProblemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Problem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
