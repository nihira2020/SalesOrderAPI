using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblProductvarinatMapping : IEntityTypeConfiguration<TblProductvarinat>
    {
        public void Configure(EntityTypeBuilder<TblProductvarinat> builder)
        {
            builder.ToTable("tbl_productvarinat");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(e => e.ProductCode)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(x => x.ColorId)
            .HasColumnName("ColorId")
            .HasColumnType("INT");

            builder.Property(x => x.SizeId)
            .HasColumnName("SizeId")
            .HasColumnType("INT");


            builder.Property(x => x.Remarks)
            .HasColumnName("Remarks")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

            builder.Property(e => e.Price)
            .HasColumnType("decimal(18, 2)");

            builder.Property(x => x.Isactive)
            .HasColumnType("BIT");
                        
        }
    }
}