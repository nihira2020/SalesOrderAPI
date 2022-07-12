
using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Models;
public interface ICustomerContainer{

    Task<List<CustomerEntity>> Getall();
    Task<CustomerEntity> Getbycode(string code);

}