using SdCommonService.Enums;
using System.Collections.Generic;

namespace SdCommonService.Models
{
    public class OrderBy
    {
        public OrderTypeEnum OrderType { get; set; }
        public List<string> ColumnName { get; set; }
    }
}