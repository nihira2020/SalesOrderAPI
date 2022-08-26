using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
public class ProductContainer : IProductContainer
{

    private readonly Sales_DBContext _DBContext;
    private readonly IMapper mapper;
    public ProductContainer(Sales_DBContext dBContext, IMapper mapper)
    {

        this._DBContext = dBContext;
        this.mapper = mapper;
    }

    public async Task<List<ProductEntity>> Getall()
    {
        var customerdata = await this._DBContext.TblProducts.ToListAsync();
        if (customerdata != null && customerdata.Count > 0)
        {
            // we need use automapper

            return this.mapper.Map<List<TblProduct>, List<ProductEntity>>(customerdata);
        }
        return new List<ProductEntity>();

    }

    public async Task<ProductEntity> Getbycode(string code)
    {
        var customerdata = await this._DBContext.TblProducts.FirstOrDefaultAsync(item => item.Code == code);
        if (customerdata != null)
        {
            var _proddata = this.mapper.Map<TblProduct, ProductEntity>(customerdata);
            if (_proddata != null)
            {
                _proddata.Variants = GetVarintbyProduct(code).Result;
            }
            return _proddata;
        }
        return new ProductEntity();

    }

    public async Task<List<ProductVariantEntity>> GetVarintbyProduct(string productcode)
    {
        var customerdata = await this._DBContext.TblProductvarinats.Where(item => item.ProductCode == productcode).ToListAsync();
        if (customerdata != null && customerdata.Count > 0)
        {
            return this.mapper.Map<List<TblProductvarinat>, List<ProductVariantEntity>>(customerdata);
        }
        return new List<ProductVariantEntity>();

    }

    public async Task<List<ProductEntity>> Getbycategory(int Category)
    {
        var customerdata = await this._DBContext.TblProducts.Where(item => item.Category == Category).ToListAsync();
        if (customerdata != null)
        {
            return this.mapper.Map<List<TblProduct>, List<ProductEntity>>(customerdata);
        }
        return new List<ProductEntity>();

    }

    public async Task<ResponseType> SaveProduct(ProductEntity product)
    {
        try
        {
            string Result = string.Empty;
            int processcount = 0;

            if (product != null)
            {
                using (var dbtransaction = await this._DBContext.Database.BeginTransactionAsync())
                {
                    // check exist product
                    var _product = await this._DBContext.TblProducts.FirstOrDefaultAsync(item => item.Code == product.Code);
                    if (_product != null)
                    {
                        //update here
                        _product.Name = product.Name;
                        _product.Category = product.Category;
                        _product.Price = product.Price;
                        _product.Remarks = product.Remarks;
                        // await this._DBContext.SaveChangesAsync();

                    }
                    else
                    {
                        // create new record
                        var _newproduct = new TblProduct()
                        {
                            Code = product.Code,
                            Name = product.Name,
                            Price = product.Price,
                            Category = product.Category,
                            Remarks = product.Remarks
                        };
                        await this._DBContext.TblProducts.AddAsync(_newproduct);
                        // await this._DBContext.SaveChangesAsync();
                    }
                    if (product.Variants != null && product.Variants.Count > 0)
                    {
                        product.Variants.ForEach(item =>
                        {
                            var _resp = SaveProductVariant(item, product.Code);
                            if (_resp.Result)
                            {
                                processcount++;
                            }
                        });
                        if (processcount == product.Variants.Count)
                        {
                            await this._DBContext.SaveChangesAsync();
                            await dbtransaction.CommitAsync();
                            return new ResponseType() { KyValue = product.Code, Result = "pass" };
                        }
                        else
                        {
                            await dbtransaction.RollbackAsync();
                        }
                    }
                }


            }
            else
            {

            }
        }


        catch (Exception ex)
        {
            throw ex;
        }

        return new ResponseType() { KyValue = string.Empty, Result = "fail" };
    }

    private async Task<bool> SaveProductVariant(ProductVariantEntity _variant, string ProductCode)
    {
        bool Result = false;

        try
        {
            var _existdata = await this._DBContext.TblProductvarinats.FirstOrDefaultAsync(item => item.Id == _variant.Id);
            if (_existdata != null)
            {

                _existdata.ColorId = _variant.ColorId;
                _existdata.SizeId = _variant.SizeId;
                _existdata.ProductCode = _variant.ProductCode;
                _existdata.Price = _variant.Price;
                 _existdata.Remarks = _variant.Remarks;
            }
            else
            {
                var _newrecord = new TblProductvarinat()
                {
                    ColorId = _variant.ColorId,
                    SizeId = _variant.SizeId,
                    Price = _variant.Price,
                    ProductCode = ProductCode,
                    Remarks = _variant.Remarks,
                    Isactive = true
                };
                await this._DBContext.TblProductvarinats.AddAsync(_newrecord);
            }
            Result = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return Result;
    }


}