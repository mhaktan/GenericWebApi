using System.Collections.Generic;

namespace SdCommonService.Models
{
    public class GetQueryDataRequest
    {
        public string QueryCode { get; set; }
        public List<Parameter> Parameters { get; set; }
        public OrderBy OrderBy { get; set; }
    }
}