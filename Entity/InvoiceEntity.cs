public class InvoiceEntity
{
    public InvoiceHeader? header { get; set; }
    public List<InvoiceDetail>? details { get; set; }
}