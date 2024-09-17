using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    DomainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain", x => x.DomainID);
                });

            migrationBuilder.CreateTable(
                name: "Framework",
                columns: table => new
                {
                    FrameworkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrameworkName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Framework", x => x.FrameworkID);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.QuestionnaireID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tier",
                columns: table => new
                {
                    TierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tier", x => x.TierId);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                columns: table => new
                {
                    UOMID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UOMType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.UOMID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentQuestionID = table.Column<int>(type: "int", nullable: true),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    DomainID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Domain_DomainID",
                        column: x => x.DomainID,
                        principalTable: "Domain",
                        principalColumn: "DomainID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkDetails",
                columns: table => new
                {
                    FrameworkID = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkDetails", x => x.FrameworkID);
                    table.ForeignKey(
                        name: "FK_FrameworkDetails_Framework_FrameworkID",
                        column: x => x.FrameworkID,
                        principalTable: "Framework",
                        principalColumn: "FrameworkID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contact_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    FileUploadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.FileUploadID);
                    table.ForeignKey(
                        name: "FK_FileUploads_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionFramework",
                columns: table => new
                {
                    QuestionFrameworkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrameworkID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionFramework", x => x.QuestionFrameworkID);
                    table.ForeignKey(
                        name: "FK_QuestionFramework_Framework_FrameworkID",
                        column: x => x.FrameworkID,
                        principalTable: "Framework",
                        principalColumn: "FrameworkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionFramework_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionQuestionnaire",
                columns: table => new
                {
                    QuestionQuestionnaireID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuestionnaire", x => x.QuestionQuestionnaireID);
                    table.ForeignKey(
                        name: "FK_QuestionQuestionnaire_Questionnaires_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaires",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuestionnaire_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
        name: "Textboxes",
        columns: table => new
        {
            TextBoxID = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            QuestionID = table.Column<int>(type: "int", nullable: false),
            Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
            OrderIndex = table.Column<int>(type: "int", nullable: false),
            UOMID = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Textboxes", x => x.TextBoxID);
            table.ForeignKey(
                name: "FK_Textboxes_Questions_QuestionID",
                column: x => x.QuestionID,
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_Textboxes_UnitOfMeasurement_UOMID",
                column: x => x.UOMID,
                principalTable: "UnitOfMeasurement",
                principalColumn: "UOMID",
                onDelete: ReferentialAction.Restrict);
        });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TierID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorID);
                    table.ForeignKey(
                        name: "FK_Vendor_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_Tier_TierID",
                        column: x => x.TierID,
                        principalTable: "Tier",
                        principalColumn: "TierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireAssignments",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireAssignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAssignments_Questionnaires_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaires",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAssignments_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAssignments_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vendorHierarchy",
                columns: table => new
                {
                    HierarchyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentVendorID = table.Column<int>(type: "int", nullable: false),
                    ChildVendorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendorHierarchy", x => x.HierarchyID);
                    table.ForeignKey(
                        name: "FK_vendorHierarchy_Vendor_ChildVendorID",
                        column: x => x.ChildVendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vendorHierarchy_Vendor_ParentVendorID",
                        column: x => x.ParentVendorID,
                        principalTable: "Vendor",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
        name: "Responses",
        columns: table => new
        {
            ResponseID = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            AssignmentID = table.Column<int>(type: "int", nullable: false),
            QuestionID = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Responses", x => x.ResponseID);
            table.ForeignKey(
                name: "FK_Responses_QuestionnaireAssignments_AssignmentID",
                column: x => x.AssignmentID,
                principalTable: "QuestionnaireAssignments",
                principalColumn: "AssignmentID",
                onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                name: "FK_Responses_Questions_QuestionID",
                column: x => x.QuestionID,
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Restrict); // Changed from Cascade to Restrict
        });

            migrationBuilder.CreateTable(
       name: "FileUploadResponses",
       columns: table => new
       {
           FileUploadResponseID = table.Column<int>(type: "int", nullable: false)
               .Annotation("SqlServer:Identity", "1, 1"),
           ResponseID = table.Column<int>(type: "int", nullable: false),
           FileUploadID = table.Column<int>(type: "int", nullable: false),
           FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
           FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
       },
       constraints: table =>
       {
           table.PrimaryKey("PK_FileUploadResponses", x => x.FileUploadResponseID);
           table.ForeignKey(
               name: "FK_FileUploadResponses_FileUploads_FileUploadID",
               column: x => x.FileUploadID,
               principalTable: "FileUploads",
               principalColumn: "FileUploadID",
               onDelete: ReferentialAction.Restrict); // Changed from Cascade to Restrict
           table.ForeignKey(
               name: "FK_FileUploadResponses_Responses_ResponseID",
               column: x => x.ResponseID,
               principalTable: "Responses",
               principalColumn: "ResponseID",
               onDelete: ReferentialAction.Restrict); // Changed from Cascade to Restrict
       });

            migrationBuilder.CreateTable(
                name: "OptionResponses",
                columns: table => new
                {
                    OptionResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseID = table.Column<int>(type: "int", nullable: false),
                    OptionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionResponses", x => x.OptionResponseID);
                    table.ForeignKey(
                        name: "FK_OptionResponses_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionResponses_Responses_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "Responses",
                        principalColumn: "ResponseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TextBoxResponses",
                columns: table => new
                {
                    TextBoxResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseID = table.Column<int>(type: "int", nullable: false),
                    TextBoxID = table.Column<int>(type: "int", nullable: false),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextBoxResponses", x => x.TextBoxResponseID);
                    table.ForeignKey(
                        name: "FK_TextBoxResponses_Responses_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "Responses",
                        principalColumn: "ResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TextBoxResponses_Textboxes_TextBoxID",
                        column: x => x.TextBoxID,
                        principalTable: "Textboxes",
                        principalColumn: "TextBoxID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploadResponses_FileUploadID",
                table: "FileUploadResponses",
                column: "FileUploadID");

            migrationBuilder.CreateIndex(
                name: "IX_FileUploadResponses_ResponseID",
                table: "FileUploadResponses",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_QuestionID",
                table: "FileUploads",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OptionResponses_OptionID",
                table: "OptionResponses",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_OptionResponses_ResponseID",
                table: "OptionResponses",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFramework_FrameworkID",
                table: "QuestionFramework",
                column: "FrameworkID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionFramework_QuestionID",
                table: "QuestionFramework",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAssignments_QuestionnaireID",
                table: "QuestionnaireAssignments",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAssignments_StatusID",
                table: "QuestionnaireAssignments",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAssignments_VendorID",
                table: "QuestionnaireAssignments",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuestionnaire_QuestionID",
                table: "QuestionQuestionnaire",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuestionnaire_QuestionnaireID",
                table: "QuestionQuestionnaire",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryID",
                table: "Questions",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DomainID",
                table: "Questions",
                column: "DomainID");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_AssignmentID",
                table: "Responses",
                column: "AssignmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_QuestionID",
                table: "Responses",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Textboxes_QuestionID",
                table: "Textboxes",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Textboxes_UOMID",
                table: "Textboxes",
                column: "UOMID");

            migrationBuilder.CreateIndex(
                name: "IX_TextBoxResponses_ResponseID",
                table: "TextBoxResponses",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_TextBoxResponses_TextBoxID",
                table: "TextBoxResponses",
                column: "TextBoxID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CategoryID",
                table: "Vendor",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_TierID",
                table: "Vendor",
                column: "TierID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UserID",
                table: "Vendor",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_vendorHierarchy_ChildVendorID",
                table: "vendorHierarchy",
                column: "ChildVendorID");

            migrationBuilder.CreateIndex(
                name: "IX_vendorHierarchy_ParentVendorID",
                table: "vendorHierarchy",
                column: "ParentVendorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUploadResponses");

            migrationBuilder.DropTable(
                name: "FrameworkDetails");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OptionResponses");

            migrationBuilder.DropTable(
                name: "QuestionFramework");

            migrationBuilder.DropTable(
                name: "QuestionQuestionnaire");

            migrationBuilder.DropTable(
                name: "TextBoxResponses");

            migrationBuilder.DropTable(
                name: "vendorHierarchy");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Framework");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "Textboxes");

            migrationBuilder.DropTable(
                name: "QuestionnaireAssignments");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tier");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
