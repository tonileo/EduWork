using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduWork.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Newmodelvalidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeaves_Users_UserId",
                table: "AnnualLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeavesRecords_Users_UserId",
                table: "AnnualLeavesRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SickLeaveRecords_Users_UserId",
                table: "SickLeaveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectRoles_Projects_ProjectId",
                table: "UserProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectRoles_Users_UserId",
                table: "UserProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AppRoles_AppRoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkDays_UserId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_AnnualLeaves_UserId",
                table: "AnnualLeaves");

            migrationBuilder.AlterColumn<string>(
                name: "EntraObjectId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProjectTimes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_UserId_WorkDate",
                table: "WorkDays",
                columns: new[] { "UserId", "WorkDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EntraObjectId",
                table: "Users",
                column: "EntraObjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Title",
                table: "Projects",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_Id",
                table: "ProjectRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonWorkingDays_NonWorkingDate",
                table: "NonWorkingDays",
                column: "NonWorkingDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_Title",
                table: "AppRoles",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnualLeaves_UserId_Year",
                table: "AnnualLeaves",
                columns: new[] { "UserId", "Year" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeaves_Users_UserId",
                table: "AnnualLeaves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeavesRecords_Users_UserId",
                table: "AnnualLeavesRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SickLeaveRecords_Users_UserId",
                table: "SickLeaveRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectRoles_Projects_ProjectId",
                table: "UserProjectRoles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectRoles_Users_UserId",
                table: "UserProjectRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AppRoles_AppRoleId",
                table: "Users",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeaves_Users_UserId",
                table: "AnnualLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnualLeavesRecords_Users_UserId",
                table: "AnnualLeavesRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SickLeaveRecords_Users_UserId",
                table: "SickLeaveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectRoles_Projects_ProjectId",
                table: "UserProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectRoles_Users_UserId",
                table: "UserProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AppRoles_AppRoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkDays_UserId_WorkDate",
                table: "WorkDays");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EntraObjectId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Title",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_Id",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_NonWorkingDays_NonWorkingDate",
                table: "NonWorkingDays");

            migrationBuilder.DropIndex(
                name: "IX_AppRoles_Title",
                table: "AppRoles");

            migrationBuilder.DropIndex(
                name: "IX_AnnualLeaves_UserId_Year",
                table: "AnnualLeaves");

            migrationBuilder.AlterColumn<string>(
                name: "EntraObjectId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ProjectTimes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProjectRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppRoles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkDays_UserId",
                table: "WorkDays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualLeaves_UserId",
                table: "AnnualLeaves",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeaves_Users_UserId",
                table: "AnnualLeaves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualLeavesRecords_Users_UserId",
                table: "AnnualLeavesRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SickLeaveRecords_Users_UserId",
                table: "SickLeaveRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectRoles_Projects_ProjectId",
                table: "UserProjectRoles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectRoles_Users_UserId",
                table: "UserProjectRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AppRoles_AppRoleId",
                table: "Users",
                column: "AppRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkDays_Users_UserId",
                table: "WorkDays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
