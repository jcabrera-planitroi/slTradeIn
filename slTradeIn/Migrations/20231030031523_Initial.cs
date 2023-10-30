using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slTradeIn.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Detail_ModelMaster",
                columns: table => new
                {
                    iModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vModelNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    iCategoryID = table.Column<int>(type: "int", nullable: false),
                    iCreatedBy = table.Column<int>(type: "int", nullable: false),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    bNotificationSent = table.Column<bool>(type: "bit", nullable: false),
                    yBGradeMarketplaceFMV = table.Column<decimal>(type: "money", nullable: true),
                    yBGradeRetailFMV = table.Column<decimal>(type: "money", nullable: true),
                    yBGradeWholeSaleFMV = table.Column<decimal>(type: "money", nullable: true),
                    yCGradeMarketplaceFMV = table.Column<decimal>(type: "money", nullable: true),
                    yCGradeRetailFMV = table.Column<decimal>(type: "money", nullable: true),
                    yCGradeWholeSaleFMV = table.Column<decimal>(type: "money", nullable: true),
                    yDGradeWholeSaleFMV = table.Column<decimal>(type: "money", nullable: true),
                    nWeight = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    nLength = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    nWidth = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    nHeight = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    bACAdaptorIncluded = table.Column<bool>(type: "bit", nullable: true),
                    bKeyboardIncluded = table.Column<bool>(type: "bit", nullable: true),
                    bMouseIncluded = table.Column<bool>(type: "bit", nullable: true),
                    bTouchScreen = table.Column<bool>(type: "bit", nullable: true),
                    bFingerPrintRdr = table.Column<bool>(type: "bit", nullable: true),
                    bWebCam = table.Column<bool>(type: "bit", nullable: true),
                    bStylus = table.Column<bool>(type: "bit", nullable: true),
                    bWIFI = table.Column<bool>(type: "bit", nullable: true),
                    iMfg = table.Column<int>(type: "int", nullable: true),
                    iUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    dDateUpdated = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detail_M__F2E8972C9BB7096B", x => x.iModelID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_emailTemplate",
                columns: table => new
                {
                    iEmailTemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vTemplateName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vSubject = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vHTMLBody = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    bActive = table.Column<bool>(type: "bit", nullable: false),
                    dCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    dUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    dInactiveDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_emailTemplate", x => x.iEmailTemplateID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_partnerLinkIP",
                columns: table => new
                {
                    iIPPartnerCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vIP = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    iUserIDPartner = table.Column<int>(type: "int", nullable: true),
                    bActive = table.Column<bool>(type: "bit", nullable: false),
                    dCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    dInactiveDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_IPPartnerLink", x => x.iIPPartnerCode);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_user",
                columns: table => new
                {
                    iUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vEmail = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vPassword = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vFirstName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vLastName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vTitle = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vCompanyName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vOfficePhone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vExtension = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vCellphone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    bIsAdmin = table.Column<bool>(type: "bit", nullable: true),
                    iTTUTechPartner = table.Column<int>(type: "int", nullable: true),
                    bActive = table.Column<bool>(type: "bit", nullable: false),
                    vIPCreation = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    dDateCreation = table.Column<DateTime>(type: "datetime", nullable: true),
                    iUserIDPartner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_user", x => x.iUserID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userCart",
                columns: table => new
                {
                    iCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iUserID = table.Column<int>(type: "int", nullable: false),
                    vShippingType = table.Column<int>(type: "int", nullable: false),
                    bSerializedReport = table.Column<bool>(type: "bit", nullable: false),
                    iLogisticID = table.Column<int>(type: "int", nullable: false),
                    mTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    mTotalSerializedReport = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    mTotalShipping = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    mTotalDOD = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    mTotalPayout = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    iBoxStatusID = table.Column<int>(type: "int", nullable: true),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    vImageName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    bDateSigned = table.Column<DateTime>(type: "datetime", nullable: true),
                    iQuoteNumber = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userCartPreference_1", x => x.iCartID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userCartDetail",
                columns: table => new
                {
                    iCartDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iCartID = table.Column<int>(type: "int", nullable: false),
                    iCategory = table.Column<int>(type: "int", nullable: false),
                    iManufacturer = table.Column<int>(type: "int", nullable: false),
                    iModelID = table.Column<int>(type: "int", nullable: false),
                    bIsAccessible = table.Column<bool>(type: "bit", nullable: false),
                    iProcessorType = table.Column<int>(type: "int", nullable: false),
                    iProcessorGen = table.Column<int>(type: "int", nullable: false),
                    iMemory = table.Column<int>(type: "int", nullable: false),
                    iHDD = table.Column<int>(type: "int", nullable: false),
                    vGrade = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    iQuantity = table.Column<int>(type: "int", nullable: false),
                    mPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    bDOD = table.Column<bool>(type: "bit", nullable: false),
                    mTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userCart", x => x.iCartDetailID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userEmail",
                columns: table => new
                {
                    iEmailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vEmail = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vEmailGroup = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    dCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    dUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    bStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userEmail", x => x.iEmailID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userEmailCampaign",
                columns: table => new
                {
                    iEmailCampaingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iUserID = table.Column<int>(type: "int", nullable: false),
                    iEmailTemplateID = table.Column<int>(type: "int", nullable: false),
                    vName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vSubject = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vHTMLBody = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userEmailCampaign", x => x.iEmailCampaingID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userEmailCampaignDetail",
                columns: table => new
                {
                    iEmailCampaingDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iEmailCampaingID = table.Column<int>(type: "int", nullable: false),
                    vEmailGroup = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userEmailCampaignDetail", x => x.iEmailCampaingDetailID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userManualQuote",
                columns: table => new
                {
                    iManualQuoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iUserID = table.Column<int>(type: "int", nullable: false),
                    vShippingType = table.Column<int>(type: "int", nullable: false),
                    bSerializedReport = table.Column<bool>(type: "bit", nullable: false),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: false),
                    iBoxStatusID = table.Column<int>(type: "int", nullable: false),
                    dSignDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userManualQuote", x => x.iManualQuoteID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userManualQuoteDetail",
                columns: table => new
                {
                    iManualQuoteDetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iManualQuoteID = table.Column<int>(type: "int", nullable: false),
                    vManufacturer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vModelNumber = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    vHDD = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vRam = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vProcessor = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    iQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userManualQuoteDetail", x => x.iManualQuoteDetID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userManualQuoteLocation",
                columns: table => new
                {
                    iManualQuoteLocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iManualQuoteID = table.Column<int>(type: "int", nullable: false),
                    iLogisticID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userManualQuoteLocation", x => x.iManualQuoteLocID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTUDesc",
                columns: table => new
                {
                    iJobID = table.Column<int>(type: "int", nullable: false),
                    vLogisticsTransportation = table.Column<string>(type: "text", nullable: true),
                    vSpecialPackaging = table.Column<string>(type: "text", nullable: true),
                    vAuditIncludes = table.Column<string>(type: "text", nullable: true),
                    vDataErasure = table.Column<string>(type: "text", nullable: true),
                    vReportingSettlement = table.Column<string>(type: "text", nullable: true),
                    vPayment = table.Column<string>(type: "text", nullable: true),
                    vProductEligibility = table.Column<string>(type: "text", nullable: true),
                    vAppleiCloudLock = table.Column<string>(type: "text", nullable: true),
                    vProductGradingB = table.Column<string>(type: "text", nullable: true),
                    vProductGradingC = table.Column<string>(type: "text", nullable: true),
                    vProductGradingD = table.Column<string>(type: "text", nullable: true),
                    vRecycling = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTUMath", x => x.iJobID);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Cat",
                columns: table => new
                {
                    iCatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vCatDescription = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    cCatActive = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    vProductCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    vCategoryType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    nAVGWeight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ref_Cat", x => x.iCatID);
                });

            migrationBuilder.CreateTable(
                name: "Ref_Main",
                columns: table => new
                {
                    iRefMainID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vReferenceID = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false),
                    vDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    vTagValue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    vTagHelp = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    vReferenceArea = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    iRefOrder = table.Column<int>(type: "int", nullable: true),
                    cRefMainActive = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    vReferenceIDGroup = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    bIsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    vApprovedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValueSql: "('Initial Script')"),
                    dApprovedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    vCulture = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    vCreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    vRef1 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    vRef2 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ref_Main", x => x.iRefMainID);
                });

            migrationBuilder.CreateTable(
                name: "Detail_ModelMasterPricing",
                columns: table => new
                {
                    iModelPricingID = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MPATID = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    iModelID = table.Column<int>(type: "int", nullable: false),
                    vTitle = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    vDescription = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    vDatasource = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    iOS = table.Column<int>(type: "int", nullable: true),
                    iFamily1 = table.Column<int>(type: "int", nullable: true),
                    iFamily2 = table.Column<int>(type: "int", nullable: true),
                    iYear = table.Column<int>(type: "int", nullable: true),
                    vPartNumber = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vSKU = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    iProcessor = table.Column<int>(type: "int", nullable: true),
                    vProcessorModel = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    iProcessorGen = table.Column<int>(type: "int", nullable: true),
                    iProcessorSpeed = table.Column<int>(type: "int", nullable: true),
                    vProcessorNumber = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    iRam = table.Column<int>(type: "int", nullable: true),
                    iStorageHDD = table.Column<int>(type: "int", nullable: true),
                    iStorageSSD = table.Column<int>(type: "int", nullable: true),
                    iScreenSize = table.Column<int>(type: "int", nullable: true),
                    dAUEDate = table.Column<DateTime>(type: "date", nullable: true),
                    bIsAUE = table.Column<bool>(type: "bit", nullable: true),
                    vListName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    mPrice_MarketRetail_B = table.Column<decimal>(type: "money", nullable: true),
                    mPrice_MarketRetail_C = table.Column<decimal>(type: "money", nullable: true),
                    mPrice_TradeIn_B = table.Column<decimal>(type: "money", nullable: true),
                    mPrice_TradeIn_C = table.Column<decimal>(type: "money", nullable: true),
                    mPrice_TradeIn_D = table.Column<decimal>(type: "money", nullable: true),
                    mPrice_Wholesale = table.Column<decimal>(type: "money", nullable: true),
                    dDateImported = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_ModelMasterPricing", x => x.iModelPricingID);
                    table.ForeignKey(
                        name: "FK_Detail_ModelMasterPricing_Detail_ModelMaster",
                        column: x => x.iModelID,
                        principalTable: "Detail_ModelMaster",
                        principalColumn: "iModelID");
                });

            migrationBuilder.CreateTable(
                name: "Detail_TTU_userLocation",
                columns: table => new
                {
                    iLogisticID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iUserID = table.Column<int>(type: "int", nullable: false),
                    vLocationLabel = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vLocationEmail = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vLocationContactPerson = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vLocationStreetAddress = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vSuiteAppt = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vCity = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vState = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    vPostalCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    bIsActive = table.Column<bool>(type: "bit", nullable: false),
                    vIPCreated = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    dDateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detail_TTU_userLocation", x => x.iLogisticID);
                    table.ForeignKey(
                        name: "FK_Detail_TTU_userLocation_Detail_TTU_user_iUserID",
                        column: x => x.iUserID,
                        principalTable: "Detail_TTU_user",
                        principalColumn: "iUserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detail_ModelMasterPricing_iModelID",
                table: "Detail_ModelMasterPricing",
                column: "iModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Detail_TTU_userLocation_iUserID",
                table: "Detail_TTU_userLocation",
                column: "iUserID");

            migrationBuilder.CreateIndex(
                name: "unq_vCatDescription",
                table: "Ref_Cat",
                column: "vCatDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "_dta_index_Ref_Main_7_1896393825__K1_K2_K3",
                table: "Ref_Main",
                columns: new[] { "iRefMainID", "vReferenceID", "vDescription" })
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "_dta_index_Ref_Main_7_1896393825__K2_K3_K1",
                table: "Ref_Main",
                columns: new[] { "vReferenceID", "vDescription", "iRefMainID" })
                .Annotation("SqlServer:FillFactor", 80);

            migrationBuilder.CreateIndex(
                name: "unq_RefMain",
                table: "Ref_Main",
                columns: new[] { "vReferenceID", "vDescription", "vReferenceIDGroup" },
                unique: true,
                filter: "[vReferenceID] IS NOT NULL AND [vDescription] IS NOT NULL AND [vReferenceIDGroup] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detail_ModelMasterPricing");

            migrationBuilder.DropTable(
                name: "Detail_TTU_emailTemplate");

            migrationBuilder.DropTable(
                name: "Detail_TTU_partnerLinkIP");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userCart");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userCartDetail");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userEmail");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userEmailCampaign");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userEmailCampaignDetail");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userLocation");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userManualQuote");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userManualQuoteDetail");

            migrationBuilder.DropTable(
                name: "Detail_TTU_userManualQuoteLocation");

            migrationBuilder.DropTable(
                name: "Detail_TTUDesc");

            migrationBuilder.DropTable(
                name: "Ref_Cat");

            migrationBuilder.DropTable(
                name: "Ref_Main");

            migrationBuilder.DropTable(
                name: "Detail_ModelMaster");

            migrationBuilder.DropTable(
                name: "Detail_TTU_user");
        }
    }
}
