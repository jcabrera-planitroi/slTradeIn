﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace slTradeIn.Data;

public partial class Detail_ModelMasterPricing
{
    public decimal iModelPricingID { get; set; }

    public decimal? MPATID { get; set; }

    public int iModelID { get; set; }

    public string vTitle { get; set; }

    public string vDescription { get; set; }

    public string vDatasource { get; set; }

    public int? iOS { get; set; }

    public int? iFamily1 { get; set; }

    public int? iFamily2 { get; set; }

    public int? iYear { get; set; }

    public string vPartNumber { get; set; }

    public string vSKU { get; set; }

    public int? iProcessor { get; set; }

    public string vProcessorModel { get; set; }

    public int? iProcessorGen { get; set; }

    public int? iProcessorSpeed { get; set; }

    public string vProcessorNumber { get; set; }

    public int? iRam { get; set; }

    public int? iStorageHDD { get; set; }

    public int? iStorageSSD { get; set; }

    public int? iScreenSize { get; set; }

    public DateTime? dAUEDate { get; set; }

    public bool? bIsAUE { get; set; }

    public string vListName { get; set; }

    public decimal? mPrice_MarketRetail_B { get; set; }

    public decimal? mPrice_MarketRetail_C { get; set; }

    public decimal? mPrice_TradeIn_B { get; set; }

    public decimal? mPrice_TradeIn_C { get; set; }

    public decimal? mPrice_TradeIn_D { get; set; }

    public decimal? mPrice_Wholesale { get; set; }

    public DateTime? dDateImported { get; set; }

    public virtual Detail_ModelMaster iModel { get; set; }
}