public class InvoiceDetail{
      public string InvoiceNo { get; set; } = null!;
        public string ProductCode { get; set; } = null!;
        public string? ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? Total { get; set; }

}