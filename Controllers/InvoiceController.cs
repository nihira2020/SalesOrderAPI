using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;


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

    [HttpGet("generatepdf")]
    public async Task<IActionResult> GeneratePDF(string InvoiceNo)
    {
        var document = new PdfDocument();
       // string HtmlContent = "<h1>Welcome to Nihira Techiees</h1>";
       InvoiceHeader header= await this._container.GetAllInvoiceHeaderbyCode(InvoiceNo);
        List<InvoiceDetail> detail = await this._container.GetAllInvoiceDetailbyCode(InvoiceNo);
        string htmlcontent = "<div style='width:100%; text-align:center'>";
        htmlcontent += "<h2>Welcome to Nihira Techiees</h2>";

        if (header != null)
        {
            htmlcontent += "<h2> Invoice No:"+header.InvoiceNo+" & Invoice Date:"+header.InvoiceDate+"</h2>";
            htmlcontent += "<h3> Customer : "+header.CustomerName+"</h3>";
            htmlcontent += "<p>"+header.DeliveryAddress+"</p>";
            htmlcontent += "<h3> Contact : 9898989898 & Email :ts@in.com </h3>";
            htmlcontent += "<div>";
        }

       
       
        htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
        htmlcontent += "<thead style='font-weight:bold'>";
        htmlcontent += "<tr>";
        htmlcontent += "<td style='border:1px solid #000'> Product Code </td>";
        htmlcontent += "<td style='border:1px solid #000'> Description </td>";
        htmlcontent += "<td style='border:1px solid #000'>Qty</td>";
        htmlcontent += "<td style='border:1px solid #000'>Price</td >";
        htmlcontent += "<td style='border:1px solid #000'>Total</td>";
        htmlcontent += "</tr>";
        htmlcontent += "</thead >";

        htmlcontent += "<tbody>";
        if (detail != null && detail.Count > 0)
        {
            detail.ForEach(item =>
            {
                htmlcontent += "<tr>";
                htmlcontent += "<td>" + item.ProductCode + "</td>";
                htmlcontent += "<td>" + item.ProductName + "</td>";
                htmlcontent += "<td>" + item.Qty + "</td >";
                htmlcontent += "<td>" + item.SalesPrice + "</td>";
                htmlcontent += "<td> " + item.Total + "</td >";
                htmlcontent += "</tr>";
            });
        }
        htmlcontent += "</tbody>";

        htmlcontent += "</table>";
        htmlcontent += "</div>";

        htmlcontent += "<div style='text-align:right'>";
        htmlcontent += "<h1> Summary Info </h1>";
        htmlcontent += "<table style='border:1px solid #000;float:right' >";
        htmlcontent += "<tr>";
        htmlcontent += "<td style='border:1px solid #000'> Summary Total </td>";
        htmlcontent += "<td style='border:1px solid #000'> Summary Tax </td>";
        htmlcontent += "<td style='border:1px solid #000'> Summary NetTotal </td>";
        htmlcontent += "</tr>";
        if (header != null)
        {
            htmlcontent += "<tr>";
            htmlcontent += "<td style='border: 1px solid #000'> "+header.Total+" </td>";
            htmlcontent += "<td style='border: 1px solid #000'>" + header.Tax + "</td>";
            htmlcontent += "<td style='border: 1px solid #000'> " + header.NetTotal + "</td>";
            htmlcontent += "</tr>";
        }
        htmlcontent += "</table>";
        htmlcontent += "</div>";

        htmlcontent += "</div>";
        PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);
        byte[]? response = null;
        using(MemoryStream ms = new MemoryStream())
        {
            document.Save(ms);
            response = ms.ToArray();    
        }
        string Filename = "Invoice_" + InvoiceNo + ".pdf";
        return File(response, "application/pdf", Filename);
    }

   


}
