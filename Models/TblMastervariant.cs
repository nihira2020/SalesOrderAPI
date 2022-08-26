using System;
using System.Collections.Generic;

namespace SalesOrderAPI.Models
{
    public partial class TblMastervariant
    {
        public int Id { get; set; }
        public string? VarintName { get; set; }
        public string? VarinatType { get; set; }
        public bool? IsActive { get; set; }
    }
}
