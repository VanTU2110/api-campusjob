﻿using System.ComponentModel;

namespace apicampusjob.Models.BaseRequest
{
    public class DpsPagingParamBase : DpsParamBase
    {
        [DefaultValue(20)]
        public int PageSize { get; set; } = 20;
        [DefaultValue(1)]
        public int Page { get; set; } = 1;
    }
}
