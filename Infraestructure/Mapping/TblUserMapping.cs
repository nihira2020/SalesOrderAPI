using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesOrderAPI.Models;

namespace SalesOrderAPI.Infraestructure.Mapping
{
    public class TblUserMapping : IEntityTypeConfiguration<TblUser>
    {
        public void Configure(EntityTypeBuilder<TblUser> builder)
        {
            builder.ToTable("tbl_user");

            builder.HasKey(e => e.Userid)
            .HasName("PK_tbl_User");                

            builder.Property(e => e.Userid)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasColumnName("userid");

            builder.Property(e => e.Email)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsUnicode(false);

            builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsUnicode(false);

            builder.Property(e => e.Password)
            .HasMaxLength(50)
            .HasColumnName("Password")
            .HasColumnType("Varchar")
            .IsUnicode(false);

            builder.Property(e => e.Role)
            .HasColumnName("Role")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsUnicode(false);
            
        }
    }
}