public class ProductEntity{
     public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
         public string? productImage { get; set; }
         public string? Remarks { get; set; }
         public List<ProductVariantEntity>? Variants { get; set; }
}