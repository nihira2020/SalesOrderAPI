using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using SalesOrderAPI.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderAPI.Extensions
{
    public static class ConnectionExtension
    {
        public static void Connection(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<Sales_DBContext>(options =>
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
            });            
            
        }
    }
}