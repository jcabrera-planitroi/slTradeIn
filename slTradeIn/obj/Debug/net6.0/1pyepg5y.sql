IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Detail_ModelMaster] (
    [iModelID] int NOT NULL IDENTITY,
    [vModelNumber] varchar(50) NOT NULL,
    [iCategoryID] int NOT NULL,
    [iCreatedBy] int NOT NULL,
    [dDateCreated] datetime NOT NULL,
    [bNotificationSent] bit NOT NULL,
    [yBGradeMarketplaceFMV] money NULL,
    [yBGradeRetailFMV] money NULL,
    [yBGradeWholeSaleFMV] money NULL,
    [yCGradeMarketplaceFMV] money NULL,
    [yCGradeRetailFMV] money NULL,
    [yCGradeWholeSaleFMV] money NULL,
    [yDGradeWholeSaleFMV] money NULL,
    [nWeight] decimal(9,2) NULL,
    [nLength] decimal(9,2) NULL,
    [nWidth] decimal(9,2) NULL,
    [nHeight] decimal(9,2) NULL,
    [bACAdaptorIncluded] bit NULL,
    [bKeyboardIncluded] bit NULL,
    [bMouseIncluded] bit NULL,
    [bTouchScreen] bit NULL,
    [bFingerPrintRdr] bit NULL,
    [bWebCam] bit NULL,
    [bStylus] bit NULL,
    [bWIFI] bit NULL,
    [iMfg] int NULL,
    [iUpdatedBy] int NULL,
    [dDateUpdated] datetime NULL,
    CONSTRAINT [PK__Detail_M__F2E8972C9BB7096B] PRIMARY KEY ([iModelID])
);
GO

CREATE TABLE [Detail_TTU_emailTemplate] (
    [iEmailTemplateID] int NOT NULL IDENTITY,
    [vTemplateName] varchar(500) NOT NULL,
    [vSubject] varchar(500) NOT NULL,
    [vHTMLBody] varchar(max) NOT NULL,
    [bActive] bit NOT NULL,
    [dCreatedDate] datetime NOT NULL,
    [dUpdatedDate] datetime NULL,
    [dInactiveDate] datetime NULL,
    CONSTRAINT [PK_Detail_TTU_emailTemplate] PRIMARY KEY ([iEmailTemplateID])
);
GO

CREATE TABLE [Detail_TTU_partnerLinkIP] (
    [iIPPartnerCode] int NOT NULL IDENTITY,
    [vIP] varchar(500) NULL,
    [iUserIDPartner] int NULL,
    [bActive] bit NOT NULL,
    [dCreatedDate] datetime NOT NULL,
    [dInactiveDate] datetime NULL,
    CONSTRAINT [PK_Detail_TTU_IPPartnerLink] PRIMARY KEY ([iIPPartnerCode])
);
GO

CREATE TABLE [Detail_TTU_user] (
    [iUserID] int NOT NULL IDENTITY,
    [vEmail] varchar(500) NOT NULL,
    [vPassword] varchar(500) NOT NULL,
    [vFirstName] varchar(500) NOT NULL,
    [vLastName] varchar(500) NOT NULL,
    [vTitle] varchar(500) NULL,
    [vCompanyName] varchar(500) NULL,
    [vOfficePhone] varchar(100) NULL,
    [vExtension] varchar(100) NULL,
    [vCellphone] varchar(100) NOT NULL,
    [bIsAdmin] bit NULL,
    [iTTUTechPartner] int NULL,
    [bActive] bit NOT NULL,
    [vIPCreation] varchar(500) NULL,
    [dDateCreation] datetime NULL,
    [iUserIDPartner] int NULL,
    CONSTRAINT [PK_Detail_TTU_user] PRIMARY KEY ([iUserID])
);
GO

CREATE TABLE [Detail_TTU_userCart] (
    [iCartID] int NOT NULL IDENTITY,
    [iUserID] int NOT NULL,
    [vShippingType] int NOT NULL,
    [bSerializedReport] bit NOT NULL,
    [iLogisticID] int NOT NULL,
    [mTotal] numeric(18,2) NULL,
    [mTotalSerializedReport] numeric(18,2) NULL,
    [mTotalShipping] numeric(18,2) NULL,
    [mTotalDOD] numeric(18,2) NULL,
    [mTotalPayout] numeric(18,2) NULL,
    [iBoxStatusID] int NULL,
    [dDateCreated] datetime NULL,
    [vImageName] varchar(200) NULL,
    [bDateSigned] datetime NULL,
    [iQuoteNumber] int NOT NULL DEFAULT (((1))),
    CONSTRAINT [PK_Detail_TTU_userCartPreference_1] PRIMARY KEY ([iCartID])
);
GO

CREATE TABLE [Detail_TTU_userCartDetail] (
    [iCartDetailID] int NOT NULL IDENTITY,
    [iCartID] int NOT NULL,
    [iCategory] int NOT NULL,
    [iManufacturer] int NOT NULL,
    [iModelID] int NOT NULL,
    [bIsAccessible] bit NOT NULL,
    [iProcessorType] int NOT NULL,
    [iProcessorGen] int NOT NULL,
    [iMemory] int NOT NULL,
    [iHDD] int NOT NULL,
    [vGrade] varchar(1) NOT NULL,
    [iQuantity] int NOT NULL,
    [mPrice] numeric(18,2) NOT NULL,
    [bDOD] bit NOT NULL,
    [mTotal] numeric(18,2) NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userCart] PRIMARY KEY ([iCartDetailID])
);
GO

CREATE TABLE [Detail_TTU_userEmail] (
    [iEmailID] int NOT NULL IDENTITY,
    [vEmail] varchar(500) NOT NULL,
    [vName] varchar(500) NULL,
    [vEmailGroup] varchar(500) NULL,
    [dCreatedDate] datetime NOT NULL,
    [dUpdatedDate] datetime NULL,
    [bStatus] bit NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userEmail] PRIMARY KEY ([iEmailID])
);
GO

CREATE TABLE [Detail_TTU_userEmailCampaign] (
    [iEmailCampaingID] int NOT NULL IDENTITY,
    [iUserID] int NOT NULL,
    [iEmailTemplateID] int NOT NULL,
    [vName] varchar(500) NULL,
    [vSubject] varchar(500) NOT NULL,
    [vHTMLBody] varchar(max) NOT NULL,
    [dDateCreated] datetime NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userEmailCampaign] PRIMARY KEY ([iEmailCampaingID])
);
GO

CREATE TABLE [Detail_TTU_userEmailCampaignDetail] (
    [iEmailCampaingDetailID] int NOT NULL IDENTITY,
    [iEmailCampaingID] int NOT NULL,
    [vEmailGroup] varchar(500) NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userEmailCampaignDetail] PRIMARY KEY ([iEmailCampaingDetailID])
);
GO

CREATE TABLE [Detail_TTU_userManualQuote] (
    [iManualQuoteID] int NOT NULL IDENTITY,
    [iUserID] int NOT NULL,
    [vShippingType] int NOT NULL,
    [bSerializedReport] bit NOT NULL,
    [dDateCreated] datetime NOT NULL,
    [iBoxStatusID] int NOT NULL,
    [dSignDate] datetime NULL,
    CONSTRAINT [PK_Detail_TTU_userManualQuote] PRIMARY KEY ([iManualQuoteID])
);
GO

CREATE TABLE [Detail_TTU_userManualQuoteDetail] (
    [iManualQuoteDetID] int NOT NULL IDENTITY,
    [iManualQuoteID] int NOT NULL,
    [vManufacturer] varchar(100) NULL,
    [vModelNumber] varchar(1000) NOT NULL,
    [vHDD] varchar(100) NULL,
    [vRam] varchar(100) NULL,
    [vProcessor] varchar(1000) NULL,
    [iQuantity] int NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userManualQuoteDetail] PRIMARY KEY ([iManualQuoteDetID])
);
GO

CREATE TABLE [Detail_TTU_userManualQuoteLocation] (
    [iManualQuoteLocID] int NOT NULL IDENTITY,
    [iManualQuoteID] int NOT NULL,
    [iLogisticID] int NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userManualQuoteLocation] PRIMARY KEY ([iManualQuoteLocID])
);
GO

CREATE TABLE [Detail_TTUDesc] (
    [iJobID] int NOT NULL,
    [vLogisticsTransportation] text NULL,
    [vSpecialPackaging] text NULL,
    [vAuditIncludes] text NULL,
    [vDataErasure] text NULL,
    [vReportingSettlement] text NULL,
    [vPayment] text NULL,
    [vProductEligibility] text NULL,
    [vAppleiCloudLock] text NULL,
    [vProductGradingB] text NULL,
    [vProductGradingC] text NULL,
    [vProductGradingD] text NULL,
    [vRecycling] text NULL,
    CONSTRAINT [PK_Detail_TTUMath] PRIMARY KEY ([iJobID])
);
GO

CREATE TABLE [Ref_Cat] (
    [iCatID] int NOT NULL IDENTITY,
    [vCatDescription] varchar(75) NOT NULL,
    [cCatActive] char(1) NULL,
    [vProductCode] varchar(3) NULL,
    [vCategoryType] varchar(10) NULL,
    [nAVGWeight] int NULL,
    CONSTRAINT [PK_ref_Cat] PRIMARY KEY ([iCatID])
);
GO

CREATE TABLE [Ref_Main] (
    [iRefMainID] int NOT NULL IDENTITY,
    [vReferenceID] varchar(75) NOT NULL,
    [vDescription] varchar(100) NOT NULL,
    [vTagValue] varchar(50) NULL,
    [vTagHelp] varchar(100) NULL,
    [vReferenceArea] varchar(50) NULL,
    [iRefOrder] int NULL,
    [cRefMainActive] char(1) NULL,
    [vReferenceIDGroup] varchar(75) NULL,
    [bIsApproved] bit NOT NULL DEFAULT (((1))),
    [vApprovedBy] varchar(100) NULL DEFAULT (('Initial Script')),
    [dApprovedOn] datetime NULL,
    [vCulture] varchar(50) NULL,
    [vCreatedBy] varchar(50) NULL,
    [dDateCreated] datetime NULL,
    [vRef1] varchar(20) NULL,
    [vRef2] varchar(20) NULL,
    CONSTRAINT [PK_Ref_Main] PRIMARY KEY ([iRefMainID])
);
GO

CREATE TABLE [Detail_ModelMasterPricing] (
    [iModelPricingID] numeric(18,0) NOT NULL IDENTITY,
    [MPATID] numeric(18,0) NULL,
    [iModelID] int NOT NULL,
    [vTitle] varchar(1000) NULL,
    [vDescription] varchar(1000) NOT NULL,
    [vDatasource] varchar(1000) NULL,
    [iOS] int NULL,
    [iFamily1] int NULL,
    [iFamily2] int NULL,
    [iYear] int NULL,
    [vPartNumber] varchar(500) NULL,
    [vSKU] varchar(500) NULL,
    [iProcessor] int NULL,
    [vProcessorModel] varchar(500) NULL,
    [iProcessorGen] int NULL,
    [iProcessorSpeed] int NULL,
    [vProcessorNumber] varchar(500) NULL,
    [iRam] int NULL,
    [iStorageHDD] int NULL,
    [iStorageSSD] int NULL,
    [iScreenSize] int NULL,
    [dAUEDate] date NULL,
    [bIsAUE] bit NULL,
    [vListName] varchar(500) NULL,
    [mPrice_MarketRetail_B] money NULL,
    [mPrice_MarketRetail_C] money NULL,
    [mPrice_TradeIn_B] money NULL,
    [mPrice_TradeIn_C] money NULL,
    [mPrice_TradeIn_D] money NULL,
    [mPrice_Wholesale] money NULL,
    [dDateImported] datetime NULL,
    CONSTRAINT [PK_Detail_ModelMasterPricing] PRIMARY KEY ([iModelPricingID]),
    CONSTRAINT [FK_Detail_ModelMasterPricing_Detail_ModelMaster] FOREIGN KEY ([iModelID]) REFERENCES [Detail_ModelMaster] ([iModelID])
);
GO

CREATE TABLE [Detail_TTU_userLocation] (
    [iLogisticID] int NOT NULL IDENTITY,
    [iUserID] int NOT NULL,
    [vLocationLabel] varchar(500) NOT NULL,
    [vLocationEmail] varchar(500) NOT NULL,
    [vLocationContactPerson] varchar(500) NOT NULL,
    [vLocationStreetAddress] varchar(500) NOT NULL,
    [vSuiteAppt] varchar(500) NOT NULL,
    [vCity] varchar(500) NOT NULL,
    [vState] varchar(50) NOT NULL,
    [vPostalCode] varchar(50) NOT NULL,
    [bIsActive] bit NOT NULL,
    [vIPCreated] varchar(50) NOT NULL,
    [dDateCreated] datetime NOT NULL,
    CONSTRAINT [PK_Detail_TTU_userLocation] PRIMARY KEY ([iLogisticID]),
    CONSTRAINT [FK_Detail_TTU_userLocation_Detail_TTU_user_iUserID] FOREIGN KEY ([iUserID]) REFERENCES [Detail_TTU_user] ([iUserID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Detail_ModelMasterPricing_iModelID] ON [Detail_ModelMasterPricing] ([iModelID]);
GO

CREATE INDEX [IX_Detail_TTU_userLocation_iUserID] ON [Detail_TTU_userLocation] ([iUserID]);
GO

CREATE UNIQUE INDEX [unq_vCatDescription] ON [Ref_Cat] ([vCatDescription]);
GO

CREATE INDEX [_dta_index_Ref_Main_7_1896393825__K1_K2_K3] ON [Ref_Main] ([iRefMainID], [vReferenceID], [vDescription]) WITH (FILLFACTOR = 80);
GO

CREATE INDEX [_dta_index_Ref_Main_7_1896393825__K2_K3_K1] ON [Ref_Main] ([vReferenceID], [vDescription], [iRefMainID]) WITH (FILLFACTOR = 80);
GO

CREATE UNIQUE INDEX [unq_RefMain] ON [Ref_Main] ([vReferenceID], [vDescription], [vReferenceIDGroup]) WHERE [vReferenceID] IS NOT NULL AND [vDescription] IS NOT NULL AND [vReferenceIDGroup] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231030031523_Initial', N'7.0.13');
GO

COMMIT;
GO

