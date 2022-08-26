
using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Models;
public interface IProductContainer
{
    Task<List<ProductEntity>> Getall();
    Task<ProductEntity> Getbycode(string code);
    Task<List<ProductEntity>> Getbycategory(int Category);

     Task<ResponseType> SaveProduct(ProductEntity product);
}