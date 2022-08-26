using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SalesOrderAPI.Models
{
    public partial class Sales_DBContext : DbContext
    {
        public Sales_DBContext()
        {
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.ToTable("tbl_Category");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_tbl_customer");

                entity.ToTable("tbl_Customer");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Phoneno).HasMaxLength(50);
            });

            modelBuilder.Entity<TblMastervariant>(entity =>
            {
                entity.ToTable("tbl_mastervariant");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.VarinatType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.VarintName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("tbl_product");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProductvarinat>(entity =>
            {
                entity.ToTable("tbl_productvarinat");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).HasMaxLength(100);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.Roleid);

                entity.ToTable("tbl_role");

                entity.Property(e => e.Roleid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("roleid");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSalesHeader>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("PK_tbl_SaleHeader");

                entity.ToTable("tbl_SalesHeader");

                entity.Property(e => e.InvoiceNo).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasMaxLength(20);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .HasColumnName("Customer Name");

                entity.Property(e => e.DeliveryAddress).HasColumnType("ntext");

                entity.Property(e => e.InvoiceDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.NetTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Remarks).HasColumnType("ntext");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TblSalesProductInfo>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceNo, e.ProductCode })
                    .HasName("PK_tbl_SalesInvoiceDetail");

                entity.ToTable("tbl_SalesProductInfo");

                entity.Property(e => e.InvoiceNo).HasMaxLength(20);

                entity.Property(e => e.ProductCode).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.SalesPrice).HasColumnType("numeric(18, 3)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("tbl_user");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
