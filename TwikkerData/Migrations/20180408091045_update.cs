using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Twikker.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_UserTexts_UserTextId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTexts_UserTexts_PostUserTextId",
                table: "UserTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTexts_Users_CreatorUserId",
                table: "UserTexts");

            migrationBuilder.DropIndex(
                name: "IX_UserTexts_CreatorUserId",
                table: "UserTexts");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "UserTexts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "UserTexts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserTextId",
                table: "UserTexts",
                newName: "TextId");

            migrationBuilder.RenameColumn(
                name: "PostUserTextId",
                table: "UserTexts",
                newName: "PostTextId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTexts_PostUserTextId",
                table: "UserTexts",
                newName: "IX_UserTexts_PostTextId");

            migrationBuilder.RenameColumn(
                name: "UserTextId",
                table: "Reactions",
                newName: "TextId");

            migrationBuilder.RenameIndex(
                name: "IX_Reactions_UserTextId",
                table: "Reactions",
                newName: "IX_Reactions_TextId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_UserTexts_TextId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTexts_UserTexts_PostTextId",
                table: "UserTexts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserTexts",
                newName: "CreatorUserId");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "UserTexts",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "TextId",
                table: "UserTexts",
                newName: "UserTextId");

            migrationBuilder.RenameColumn(
                name: "PostTextId",
                table: "UserTexts",
                newName: "PostUserTextId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTexts_PostTextId",
                table: "UserTexts",
                newName: "IX_UserTexts_PostUserTextId");

            migrationBuilder.RenameColumn(
                name: "TextId",
                table: "Reactions",
                newName: "UserTextId");

            migrationBuilder.RenameIndex(
                name: "IX_Reactions_TextId",
                table: "Reactions",
                newName: "IX_Reactions_UserTextId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTexts_CreatorUserId",
                table: "UserTexts",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_UserTexts_UserTextId",
                table: "Reactions",
                column: "UserTextId",
                principalTable: "UserTexts",
                principalColumn: "UserTextId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTexts_UserTexts_PostUserTextId",
                table: "UserTexts",
                column: "PostUserTextId",
                principalTable: "UserTexts",
                principalColumn: "UserTextId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTexts_Users_CreatorUserId",
                table: "UserTexts",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
