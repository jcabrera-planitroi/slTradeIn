﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace slTradeIn.Data;

public partial class Detail_TTU_emailTemplate
{
    public int iEmailTemplateID { get; set; }

    public string vTemplateName { get; set; }

    public string vSubject { get; set; }

    public string vHTMLBody { get; set; }

    public bool bActive { get; set; }

    public DateTime dCreatedDate { get; set; }

    public DateTime? dUpdatedDate { get; set; }

    public DateTime? dInactiveDate { get; set; }
}