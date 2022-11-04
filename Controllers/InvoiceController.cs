using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceContainer _container;
    public InvoiceController(IInvoiceContainer container)
    {
        this._container = container;
    }

    [HttpGet("GetAllHeader")]
    public async Task<List<InvoiceHeader>> GetAllHeader()
    {
        return await this._container.GetAllInvoiceHeader();

    }
    [HttpGet("GetAllHeaderbyCode")]
    public async Task<InvoiceHeader> GetAllHeaderbyCode(string invoiceno)
    {
        return await this._container.GetAllInvoiceHeaderbyCode(invoiceno);

    }

    [HttpGet("GetAllDetailbyCode")]
    public async Task<List<InvoiceDetail>> GetAllDetailbyCode(string invoiceno)
    {
        return await this._container.GetAllInvoiceDetailbyCode(invoiceno);

    }

    [HttpPost("Save")]
    public async Task<ResponseType> Save([FromBody] InvoiceInput invoiceEntity)
    {
        return await this._container.Save(invoiceEntity);

    }

    [HttpDelete("Remove")]
    public async Task<ResponseType> Remove(string InvoiceNo)
    {
        return await this._container.Remove(InvoiceNo);

    }



}
