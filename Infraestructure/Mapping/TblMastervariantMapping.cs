using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblMastervariantMapping : IEntityTypeConfiguration<TblMastervariant>
    {
        public void Configure(EntityTypeBuilder<TblMastervariant> builder)
        {
            builder.ToTable("tbl_mastervariant");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(e => e.VarintName)
            .HasColumnName("VarintName")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

            builder.Property(x => x.VarinatType)
            .HasColumnName("VarinatType")
            .HasColumnType("VARCHAR")
            .HasMaxLength(1);

            builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("BIT");
        }
    }
}