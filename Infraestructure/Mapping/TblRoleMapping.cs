using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblRoleMapping : IEntityTypeConfiguration<TblRole>
    {
        public void Configure(EntityTypeBuilder<TblRole> builder)
        {
            builder.ToTable("tbl_role");

            builder.HasKey(e => e.Roleid);        

            builder.Property(e => e.Roleid)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)                    
            .HasColumnName("Roleid");

            builder.Property(e => e.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
                                    
        }
    }
}