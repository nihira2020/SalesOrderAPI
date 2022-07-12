public interface IInvoiceContainer
{
    Task<List<InvoiceHeader>> GetAllInvoiceHeader();
    Task<InvoiceHeader> GetAllInvoiceHeaderbyCode(string invoiceno);
    Task<List<InvoiceDetail>> GetAllInvoiceDetailbyCode(string invoiceno);
    Task<ResponseType> Save(InvoiceEntity invoiceEntity);
    Task<ResponseType> Remove(string invoiceno);
}
