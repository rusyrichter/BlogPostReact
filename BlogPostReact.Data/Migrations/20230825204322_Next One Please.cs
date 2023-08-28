using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogPostReact.Data.Migrations
{
    public partial class NextOnePlease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsTags_TagId",
                table: "QuestionsTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags",
                columns: new[] { "TagId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTags_QuestionId",
                table: "QuestionsTags",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsTags_QuestionId",
                table: "QuestionsTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags",
                columns: new[] { "QuestionId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTags_TagId",
                table: "QuestionsTags",
                column: "TagId");
        }
    }
}
