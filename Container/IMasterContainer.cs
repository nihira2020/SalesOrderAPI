
using Microsoft.EntityFrameworkCore;
using SalesOrderAPI.Models;
public interface IMasterContainer{

    Task<List<VariantEntity>> GetAllVariant(string variantType);

}