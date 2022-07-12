using Microsoft.AspNetCore.Mvc;
using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerContainer _container;
    public CustomerController(ICustomerContainer container)
    {
        this._container = container;
    }

    [HttpGet("GetAll")]
    public async Task<List<CustomerEntity>> GetAll()
    {
        return await this._container.Getall();

    }
      [HttpGet("GetByCode")]
    public async Task<CustomerEntity> GetByCode(string Code)
    {
        return await this._container.Getbycode(Code);

    }

}
