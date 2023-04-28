using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblSalesProductInfoMapping : IEntityTypeConfiguration<TblSalesProductInfo>
    {
        public void Configure(EntityTypeBuilder<TblSalesProductInfo> builder)
        {

            builder.ToTable("tbl_SalesProductInfo");

            builder.HasKey(e => new { e.InvoiceNo, e.ProductCode })            
                .HasName("PK_tbl_SalesInvoiceDetail");
            
                builder.Property(e => e.InvoiceNo)
                .IsRequired()
                .HasColumnName("InvoiceNo")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

                builder.Property(e => e.ProductCode)
                .IsRequired()
                .HasColumnName("ProductCode")
                .HasColumnType("VARCHAR")
                .HasMaxLength(20);

                builder.Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("smalldatetime");

                builder.Property(e => e.CreateUser)
                .HasColumnName("CreateUser")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

                builder.Property(e => e.ModifyDate)
                .HasColumnName("ModifyDate")
                .HasColumnType("smalldatetime");

                builder.Property(e => e.ModifyUser)
                .HasColumnName("ModifyUser")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

                builder.Property(e => e.ProductName)
                .HasColumnName("ProductName")
                .HasColumnType("VARCHAR")
                .HasMaxLength(100);

                builder.Property(e => e.SalesPrice)
                .HasColumnName("SalesPrice")                
                .HasColumnType("numeric(18, 3)");

                builder.Property(e => e.Total)
                .HasColumnName("Total")
                .HasColumnType("numeric(18, 2)");
            
        }
    }
}