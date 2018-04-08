using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Twikker.Data.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_UserTexts_TextId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTexts_UserTexts_PostTextId",
                table: "UserTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTexts",
                table: "UserTexts");

            migrationBuilder.RenameTable(
                name: "UserTexts",
                newName: "Texts");

            migrationBuilder.RenameIndex(
                name: "IX_UserTexts_PostTextId",
                table: "Texts",
                newName: "IX_Texts_PostTextId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Texts",
                table: "Texts",
                column: "TextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Texts_TextId",
                table: "Reactions",
                column: "TextId",
                principalTable: "Texts",
                principalColumn: "TextId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Texts_Texts_PostTextId",
                table: "Texts",
                column: "PostTextId",
                principalTable: "Texts",
                principalColumn: "TextId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Texts_TextId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Texts_Texts_PostTextId",
                table: "Texts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Texts",
                table: "Texts");

            migrationBuilder.RenameTable(
                name: "Texts",
                newName: "UserTexts");

            migrationBuilder.RenameIndex(
                name: "IX_Texts_PostTextId",
                table: "UserTexts",
                newName: "IX_UserTexts_PostTextId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTexts",
                table: "UserTexts",
                column: "TextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_UserTexts_TextId",
                table: "Reactions",
                column: "TextId",
                principalTable: "UserTexts",
                principalColumn: "TextId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTexts_UserTexts_PostTextId",
                table: "UserTexts",
                column: "PostTextId",
                principalTable: "UserTexts",
                principalColumn: "TextId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
