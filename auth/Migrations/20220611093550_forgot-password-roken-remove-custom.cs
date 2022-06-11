﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace auth.Migrations
{
    public partial class forgotpasswordrokenremovecustom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgotPasswordToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordTokenExpiryTime",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForgotPasswordToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForgotPasswordTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
