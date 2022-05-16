using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradingEntities");

            migrationBuilder.DropTable(
                name: "AssignmentEntities");

            migrationBuilder.DropTable(
                name: "StudentEntities");

            migrationBuilder.DropTable(
                name: "LaboratoryEntities");

            migrationBuilder.CreateTable(
                name: "CustomerEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    NoOfAvailableSpots = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferEntities_DestinationEntities_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "DestinationEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NoOfBookedSeats = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingEntities_CustomerEntities_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingEntities_OfferEntities_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OfferEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewEntities_CustomerEntities_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewEntities_OfferEntities_OfferId",
                        column: x => x.OfferId,
                        principalTable: "OfferEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdditionalComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingEntities_BookingEntities_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BookingEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingEntities_BookingId",
                table: "BillingEntities",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntities_CustomerId",
                table: "BookingEntities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingEntities_OfferId",
                table: "BookingEntities",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEntities_DestinationId",
                table: "OfferEntities",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntities_CustomerId",
                table: "ReviewEntities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntities_OfferId",
                table: "ReviewEntities",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingEntities");

            migrationBuilder.DropTable(
                name: "ReviewEntities");

            migrationBuilder.DropTable(
                name: "BookingEntities");

            migrationBuilder.DropTable(
                name: "CustomerEntities");

            migrationBuilder.DropTable(
                name: "OfferEntities");

            migrationBuilder.DropTable(
                name: "DestinationEntities");

            migrationBuilder.CreateTable(
                name: "LaboratoryEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Objectives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Hobby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaboratoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentEntities_LaboratoryEntities_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "LaboratoryEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GradingEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Grade = table.Column<float>(type: "real", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradingEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradingEntities_AssignmentEntities_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "AssignmentEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradingEntities_StudentEntities_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentEntities_LaboratoryId",
                table: "AssignmentEntities",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GradingEntities_AssignmentId",
                table: "GradingEntities",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GradingEntities_StudentId",
                table: "GradingEntities",
                column: "StudentId");
        }
    }
}
