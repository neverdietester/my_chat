using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingProgram.Infrastructure.PostgresIdentity.Migrations
{
    /// <inheritdoc />
    public partial class Test_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "Id",
                keyValue: new Guid("8dbd71b4-b9d6-4518-8013-50528af62a23"),
                column: "RefreshTokenExpireTime",
                value: new DateTime(2024, 12, 10, 12, 47, 24, 663, DateTimeKind.Utc).AddTicks(9864));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserToken",
                keyColumn: "Id",
                keyValue: new Guid("8dbd71b4-b9d6-4518-8013-50528af62a23"),
                column: "RefreshTokenExpireTime",
                value: new DateTime(2024, 12, 7, 14, 20, 37, 554, DateTimeKind.Utc).AddTicks(7794));
        }
    }
}
