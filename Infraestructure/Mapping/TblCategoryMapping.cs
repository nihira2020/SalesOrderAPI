using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblCategoryMapping : IEntityTypeConfiguration<TblCategory>
    {
        public void Configure(EntityTypeBuilder<TblCategory> builder)
        {
            builder.ToTable("tbl_Category");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

            builder.HasIndex(x => x.Name, "IX_Category_Name")
            .IsUnique();
        }
    }
}