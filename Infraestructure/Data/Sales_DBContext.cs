using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SalesOrderAPI.Infraestructure.Mapping;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Data
{
    public partial class Sales_DBContext : DbContext
    {        

        public Sales_DBContext(DbContextOptions<Sales_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblMastervariant> TblMastervariants { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblProductvarinat> TblProductvarinats { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblSalesHeader> TblSalesHeaders { get; set; } = null!;
        public virtual DbSet<TblSalesProductInfo> TblSalesProductInfos { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TblUserMapping).Assembly);
        }

        
    }
}
