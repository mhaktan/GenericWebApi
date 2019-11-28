using SdCommonService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SdCommonService.Models
{
    public class ResultMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string QueryCode { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
        

    }
}