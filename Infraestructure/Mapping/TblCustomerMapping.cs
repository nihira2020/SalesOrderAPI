using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblCustomerMapping : IEntityTypeConfiguration<TblCustomer>
    {
        public void Configure(EntityTypeBuilder<TblCustomer> builder)
        {
            builder.ToTable("tbl_Customer");

            builder.HasKey(x => x.Code)
            .HasName("PK_tbl_customer");

            builder.Property(x => x.Code)
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(20);

            builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(x => x.Address)
            .HasColumnName("Address")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(200);

            builder.Property(x => x.Phoneno)
            .HasColumnName("Phoneno")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);


            builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);


            builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("BIT");

            builder.Property(x => x.CreateUser)
            .HasColumnName("CreateUser")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

            builder.Property(x => x.ModifyUser)
            .HasColumnName("ModifyUser")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

            builder.Property(x => x.ModifyDate)
            .HasColumnName("ModifyDate")
            .HasColumnType("DATETIME");

            builder.Property(x => x.CreateDate)
            .HasColumnName("CreateDate")
            .HasColumnType("DATETIME")
            .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(x => x.Name, "IX_Customer_Name")
            .IsUnique();
        }
    }
}