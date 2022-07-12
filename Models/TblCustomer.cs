using System;
using System.Collections.Generic;

namespace SalesOrderAPI.Models
{
    public partial class TblCustomer
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phoneno { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public string? CreateUser { get; set; }
        public string? ModifyUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
