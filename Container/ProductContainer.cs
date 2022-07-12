using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
public class ProductContainer:IProductContainer{

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
            return this.mapper.Map<TblProduct, ProductEntity>(customerdata);
        }
        return new ProductEntity();

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
}