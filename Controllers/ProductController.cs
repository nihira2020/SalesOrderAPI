using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductContainer _container;
    public ProductController(IProductContainer container)
    {
        this._container = container;
    }

    [HttpGet("GetAll")]
    public async Task<List<ProductEntity>> GetAll()
    {
        return await this._container.Getall();

    }
    [HttpGet("GetByCode")]
    public async Task<ProductEntity> GetByCode(string Code)
    {
        return await this._container.Getbycode(Code);

    }

    [HttpGet("Getbycategory")]
    public async Task<List<ProductEntity>> Getbycategory(int Code)
    {
        return await this._container.Getbycategory(Code);

    }

}
