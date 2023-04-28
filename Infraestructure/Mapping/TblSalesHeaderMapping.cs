using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblSalesHeaderMapping : IEntityTypeConfiguration<TblSalesHeader>
    {
        public void Configure(EntityTypeBuilder<TblSalesHeader> builder)
        {
            builder.ToTable("tbl_SalesHeader");
            
            builder.HasKey(e => e.InvoiceNo)
            .HasName("PK_tbl_SaleHeader");

            builder.Property(x=> x.InvoiceNo)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired();

                builder.Property(e => e.CreateDate)
                .HasColumnName("CreateDate")
                .HasColumnType("smalldatetime");

                builder.Property(e => e.CreateUser)
                .HasColumnName("CreateUser")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

                builder.Property(e => e.CustomerId)
                .HasColumnName("CustumerId")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(20);

                builder.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR")
                .HasColumnName("CustomerName");


            builder.Property(e => e.InvoiceDate)
            .HasColumnName("InvoiceDate")
            .HasDefaultValueSql("GETDATE()")                
            //.HasDefaultValue(DateTime.Now.ToUniversalTime()) You choose what is best
            .HasColumnType("smalldatetime");

                builder.Property(e => e.ModifyDate)
                .HasColumnName("ModifyDate")
                .HasColumnType("smalldatetime");

                builder.Property(e => e.ModifyUser)
                .HasColumnName("ModifyUser")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

                builder.Property(e => e.NetTotal)
                .HasColumnName("NetTotal")
                .HasColumnType("numeric(18, 2)");

                builder.Property(e => e.Remarks)
                .HasColumnName("Remarks")
                .HasColumnType("VARCHAR");

                builder.Property(e => e.Tax)
                .HasColumnName("Tax")
                .HasColumnType("numeric(18, 4)");

                builder.Property(e => e.Total)
                .HasColumnName("Total")
                .HasColumnType("numeric(18, 2)");
        }
    }
}