using SalesOrderAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
public class InvoiceContainer : IInvoiceContainer
{
    private readonly Sales_DBContext _DBContext;
    private readonly IMapper mapper;
     private readonly ILogger<InvoiceContainer> _logger;
    public InvoiceContainer(Sales_DBContext dBContext, IMapper mapper,ILogger<InvoiceContainer> _logger)
    {
        this._DBContext = dBContext;
        this.mapper = mapper;
        this._logger=_logger;
    }
    public async Task<List<InvoiceHeader>> GetAllInvoiceHeader()
    {
        var _data = await this._DBContext.TblSalesHeaders.ToListAsync();
        if (_data != null && _data.Count > 0)
        {
            return this.mapper.Map<List<TblSalesHeader>, List<InvoiceHeader>>(_data);
        }
        return new List<InvoiceHeader>();
    }

    public async Task<InvoiceHeader> GetAllInvoiceHeaderbyCode(string invoiceno)
    {
        var _data = await this._DBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceno);
        if (_data != null)
        {
            return this.mapper.Map<TblSalesHeader, InvoiceHeader>(_data);
        }
        return new InvoiceHeader();
    }

    public async Task<List<InvoiceDetail>> GetAllInvoiceDetailbyCode(string invoiceno)
    {
        var _data = await this._DBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceno).ToListAsync();
        if (_data != null && _data.Count > 0)
        {
            return this.mapper.Map<List<TblSalesProductInfo>, List<InvoiceDetail>>(_data);
        }
        return new List<InvoiceDetail>();
    }

    public async Task<ResponseType> Save(InvoiceInput invoiceEntity)
    {
        string Result = string.Empty;
        int processcount = 0;
        var response = new ResponseType();
        if (invoiceEntity != null)
        {
            using (var dbtransaction = await this._DBContext.Database.BeginTransactionAsync())
            {

                if (invoiceEntity != null)
                    Result = await this.SaveHeader(invoiceEntity);

                if (!string.IsNullOrEmpty(Result) && (invoiceEntity.details != null && invoiceEntity.details.Count > 0))
                {
                    invoiceEntity.details.ForEach(item =>
                    {
                        bool saveresult = this.SaveDetail(item, invoiceEntity.CreateUser,invoiceEntity.InvoiceNo).Result;
                        if (saveresult)
                        {
                            processcount++;
                        }
                    });

                    if (invoiceEntity.details.Count == processcount)
                    {
                        await this._DBContext.SaveChangesAsync();
                        await dbtransaction.CommitAsync();
                        response.Result = "pass";
                        response.KyValue = Result;
                    }
                    else
                    {
                        await dbtransaction.RollbackAsync();
                        response.Result = "faill";
                        response.Result = string.Empty;
                    }
                }
                else
                {
                    response.Result = "faill";
                    response.Result = string.Empty;
                }

                // await this._DBContext.SaveChangesAsync();
                //         await dbtransaction.CommitAsync();
                //         response.Result = "pass";
                //         response.KyValue = Result;

            };
        }
        else
        {
            return new ResponseType();
        }
        return response;

    }

    private async Task<string> SaveHeader(InvoiceInput invoiceHeader)
    {
        string Results = string.Empty;

        try
        {
            TblSalesHeader _header = this.mapper.Map<InvoiceInput, TblSalesHeader>(invoiceHeader);
            _header.InvoiceDate=DateTime.Now;
            var header = await this._DBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceHeader.InvoiceNo);

            if (header != null)
            {
                header.CustomerId = invoiceHeader.CustomerId;
                header.CustomerName = invoiceHeader.CustomerName;
                header.DeliveryAddress = invoiceHeader.DeliveryAddress;
                header.Total = invoiceHeader.Total;
                header.Remarks = invoiceHeader.Remarks;
                header.Tax = invoiceHeader.Tax;
                header.NetTotal = invoiceHeader.NetTotal;
                header.ModifyUser = invoiceHeader.CreateUser;
                header.ModifyDate = DateTime.Now;

                var _detdata = await this._DBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceHeader.InvoiceNo).ToListAsync();
                if (_detdata != null && _detdata.Count > 0)
                {
                    this._DBContext.TblSalesProductInfos.RemoveRange(_detdata);
                }
            }
            else
            {
                await this._DBContext.TblSalesHeaders.AddAsync(_header);
            }
            Results = invoiceHeader.InvoiceNo;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return Results;
    }
    private async Task<bool> SaveDetail(InvoiceDetail invoiceDetail, string User,string InvoiceNo)
    {
        try
        {
            TblSalesProductInfo _detail = this.mapper.Map<InvoiceDetail, TblSalesProductInfo>(invoiceDetail);
            _detail.CreateDate = DateTime.Now;
            _detail.CreateUser = User;
            _detail.InvoiceNo=InvoiceNo;
            await this._DBContext.TblSalesProductInfos.AddAsync(_detail);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ResponseType> Remove(string invoiceno)
    {
        try
        {
            using (var dbtransaction = await this._DBContext.Database.BeginTransactionAsync())
            {
                var _data = await this._DBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceno);
                if (_data != null)
                {
                    this._DBContext.TblSalesHeaders.Remove(_data);
                }

                var _detdata = await this._DBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceno).ToListAsync();
                if (_detdata != null && _detdata.Count > 0)
                {
                    this._DBContext.TblSalesProductInfos.RemoveRange(_detdata);
                }
                await this._DBContext.SaveChangesAsync();
                await dbtransaction.CommitAsync();
            }
            return new ResponseType() { Result = "pass", KyValue = invoiceno };
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

}