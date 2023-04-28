using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblProductMapping : IEntityTypeConfiguration<TblProduct>
    {
        public void Configure(EntityTypeBuilder<TblProduct> builder)
        {
            builder.ToTable("tbl_product");

            builder.HasKey(e => e.Code);

            // builder.Property(x => x.Code)
            // .ValueGeneratedOnAdd()
            // .UseIdentityColumn();

            builder.Property(e => e.Code)
            .HasColumnType("VARCHAR")
           .HasMaxLength(20);

            builder.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(250);

            builder.Property(e => e.Price)
            .HasColumnType("decimal(18, 3)");

            builder.Property(x => x.Category)
            .HasColumnType("INT");

            builder.Property(e => e.Remarks)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);


        }
    }
}