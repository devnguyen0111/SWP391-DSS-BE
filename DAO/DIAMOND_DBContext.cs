﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace DAO;

public partial class DIAMOND_DBContext : DbContext
{
    public DIAMOND_DBContext(DbContextOptions<DIAMOND_DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Cover> Covers { get; set; }

    public virtual DbSet<CoverMetaltype> CoverMetaltypes { get; set; }

    public virtual DbSet<CoverSize> CoverSizes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DeliveryStaff> DeliveryStaffs { get; set; }

    public virtual DbSet<Diamond> Diamonds { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Metaltype> Metaltypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SaleStaff> SaleStaffs { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__26A111AD0232EBEA");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedOnAdd()
                .HasColumnName("addressId");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("zipCode");

            entity.HasOne(d => d.AddressNavigation).WithOne(p => p.Address)
                .HasForeignKey<Address>(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Address__address__6FE99F9F");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__415B03B8953B264B");

            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("cartId");
            entity.Property(e => e.CartQuantity).HasColumnName("cartQuantity");

            entity.HasOne(d => d.CartNavigation).WithOne(p => p.Cart)
                .HasForeignKey<Cart>(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cart__cartId__628FA481");
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("PK__CartProd__F38A0EAE905ACC9E");

            entity.ToTable("CartProduct");

            entity.Property(e => e.CartId).HasColumnName("cartId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartProdu__cartI__656C112C");

            entity.HasOne(d => d.Product).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartProdu__produ__66603565");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__23CAF1D8EB1B4631");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<Cover>(entity =>
        {
            entity.HasKey(e => e.CoverId).HasName("PK__Cover__B192A2E0B8414299");

            entity.ToTable("Cover");

            entity.Property(e => e.CoverId).HasColumnName("coverId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CoverName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("coverName");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubCategoryId).HasColumnName("subCategoryId");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Category).WithMany(p => p.Covers)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cover__categoryI__412EB0B6");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Covers)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cover__subCatego__403A8C7D");
        });

        modelBuilder.Entity<CoverMetaltype>(entity =>
        {
            entity.HasKey(e => new { e.MetaltypeId, e.CoverId }).HasName("PK__CoverMet__F1356AB699639745");

            entity.ToTable("CoverMetaltype");

            entity.Property(e => e.MetaltypeId).HasColumnName("metaltypeId");
            entity.Property(e => e.CoverId).HasColumnName("coverId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Cover).WithMany(p => p.CoverMetaltypes)
                .HasForeignKey(d => d.CoverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoverMeta__cover__48CFD27E");

            entity.HasOne(d => d.Metaltype).WithMany(p => p.CoverMetaltypes)
                .HasForeignKey(d => d.MetaltypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoverMeta__metal__47DBAE45");
        });

        modelBuilder.Entity<CoverSize>(entity =>
        {
            entity.HasKey(e => new { e.SizeId, e.CoverId }).HasName("PK__CoverSiz__4EA8CF79837F9095");

            entity.ToTable("CoverSize");

            entity.Property(e => e.SizeId).HasColumnName("sizeId");
            entity.Property(e => e.CoverId).HasColumnName("coverId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Cover).WithMany(p => p.CoverSizes)
                .HasForeignKey(d => d.CoverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoverSize__cover__44FF419A");

            entity.HasOne(d => d.Size).WithMany(p => p.CoverSizes)
                .HasForeignKey(d => d.SizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CoverSize__sizeI__440B1D61");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CusId).HasName("PK__Customer__BA9897F3F2093C5A");

            entity.ToTable("Customer");

            entity.Property(e => e.CusId)
                .ValueGeneratedNever()
                .HasColumnName("cusId");
            entity.Property(e => e.CusFirstName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cusFirstName");
            entity.Property(e => e.CusLastName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cusLastName");
            entity.Property(e => e.CusPhoneNum)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cusPhoneNum");

            entity.HasOne(d => d.Cus).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__cusId__5535A963");

            entity.HasMany(d => d.Vouchers).WithMany(p => p.Cus)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerVoucher",
                    r => r.HasOne<Voucher>().WithMany()
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CustomerV__vouch__07C12930"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CusId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CustomerV__cusId__06CD04F7"),
                    j =>
                    {
                        j.HasKey("CusId", "VoucherId").HasName("PK__Customer__35CBAF6DAF62384E");
                        j.ToTable("CustomerVoucher");
                        j.IndexerProperty<int>("CusId").HasColumnName("cusId");
                        j.IndexerProperty<int>("VoucherId").HasColumnName("voucherId");
                    });
        });

        modelBuilder.Entity<DeliveryStaff>(entity =>
        {
            entity.HasKey(e => e.DStaffId).HasName("PK__Delivery__C4E2975C720BC802");

            entity.ToTable("DeliveryStaff");

            entity.Property(e => e.DStaffId)
                .ValueGeneratedNever()
                .HasColumnName("dStaffId");
            entity.Property(e => e.ManagerId).HasColumnName("managerId");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.DStaff).WithOne(p => p.DeliveryStaff)
                .HasForeignKey<DeliveryStaff>(d => d.DStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeliveryS__dStaf__5EBF139D");

            entity.HasOne(d => d.Manager).WithMany(p => p.DeliveryStaffs)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__DeliveryS__manag__5FB337D6");
        });

        modelBuilder.Entity<Diamond>(entity =>
        {
            entity.HasKey(e => e.DiamondId).HasName("PK__Diamond__F848E6C096EE6D07");

            entity.ToTable("Diamond");

            entity.Property(e => e.DiamondId).HasColumnName("diamondId");
            entity.Property(e => e.CaratWeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("caratWeight");
            entity.Property(e => e.Clarity)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clarity");
            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Cut)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cut");
            entity.Property(e => e.DiamondName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diamondName");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Shape)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shape");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__favorite__876A64D54DFA7996");

            entity.ToTable("favorite");

            entity.Property(e => e.FavoriteId)
                .ValueGeneratedNever()
                .HasColumnName("favoriteId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.FavoriteNavigation).WithOne(p => p.Favorite)
                .HasForeignKey<Favorite>(d => d.FavoriteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorite__favori__693CA210");
        });

        modelBuilder.Entity<FavoriteProduct>(entity =>
        {
            entity.HasKey(e => new { e.FavoriteId, e.ProductId }).HasName("PK__favorite__35BB69C3206DBB24");

            entity.ToTable("favoriteProduct");

            entity.Property(e => e.FavoriteId).HasColumnName("favoriteId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Favorite).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.FavoriteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favoriteP__favor__6C190EBB");

            entity.HasOne(d => d.Product).WithMany(p => p.FavoriteProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favoriteP__produ__6D0D32F4");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.ManId).HasName("PK__Manager__0A7A95EEAC0A04E2");

            entity.ToTable("Manager");

            entity.Property(e => e.ManId)
                .ValueGeneratedNever()
                .HasColumnName("manId");
            entity.Property(e => e.ManName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("manName");
            entity.Property(e => e.ManPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("manPhone");

            entity.HasOne(d => d.Man).WithOne(p => p.Manager)
                .HasForeignKey<Manager>(d => d.ManId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Manager__manId__5812160E");
        });

        modelBuilder.Entity<Metaltype>(entity =>
        {
            entity.HasKey(e => e.MetaltypeId).HasName("PK__Metaltyp__EA2C4098E45D6D2E");

            entity.ToTable("Metaltype");

            entity.Property(e => e.MetaltypeId).HasColumnName("metaltypeId");
            entity.Property(e => e.MetaltypeName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metaltypeName");
            entity.Property(e => e.MetaltypePrice)
                .HasColumnType("money")
                .HasColumnName("metaltypePrice");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__0809335D1E72F54E");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.CusId).HasColumnName("cusId");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.ShippingMethodId).HasColumnName("shippingMethodId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmount");

            entity.HasOne(d => d.Cus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Order__cusId__74AE54BC");

            entity.HasOne(d => d.ShippingMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShippingMethodId)
                .HasConstraintName("FK__Order__shippingM__75A278F5");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__2D10D16AFF938D4A");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.CoverId).HasColumnName("coverId");
            entity.Property(e => e.DiamondId).HasColumnName("diamondId");
            entity.Property(e => e.MetaltypeId).HasColumnName("metaltypeId");
            entity.Property(e => e.Pp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PP");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("productName");
            entity.Property(e => e.SizeId).HasColumnName("sizeId");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.Cover).WithMany(p => p.Products)
                .HasForeignKey(d => d.CoverId)
                .HasConstraintName("FK__Product__coverId__4E88ABD4");

            entity.HasOne(d => d.Diamond).WithMany(p => p.Products)
                .HasForeignKey(d => d.DiamondId)
                .HasConstraintName("FK__Product__diamond__4D94879B");

            entity.HasOne(d => d.Metaltype).WithMany(p => p.Products)
                .HasForeignKey(d => d.MetaltypeId)
                .HasConstraintName("FK__Product__metalty__4F7CD00D");

            entity.HasOne(d => d.Size).WithMany(p => p.Products)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__Product__sizeId__5070F446");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.OrderId }).HasName("PK__ProductO__ED90425FC87FA0BC");

            entity.ToTable("ProductOrder");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductOr__order__797309D9");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductOr__produ__787EE5A0");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__2ECD6E0499756987");

            entity.ToTable("Review");

            entity.Property(e => e.ReviewId).HasColumnName("reviewId");
            entity.Property(e => e.CusId).HasColumnName("cusId");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Review1)
                .HasColumnType("text")
                .HasColumnName("review");
            entity.Property(e => e.ReviewDate).HasColumnName("reviewDate");

            entity.HasOne(d => d.Cus).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Review__cusId__01142BA1");
        });

        modelBuilder.Entity<SaleStaff>(entity =>
        {
            entity.HasKey(e => e.SStaffId).HasName("PK__SaleStaf__7D4E9B956D5A2A6E");

            entity.ToTable("SaleStaff");

            entity.Property(e => e.SStaffId)
                .ValueGeneratedNever()
                .HasColumnName("sStaffId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.ManagerId).HasColumnName("managerId");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            entity.HasOne(d => d.Manager).WithMany(p => p.SaleStaffs)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__SaleStaff__manag__5BE2A6F2");

            entity.HasOne(d => d.SStaff).WithOne(p => p.SaleStaff)
                .HasForeignKey<SaleStaff>(d => d.SStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleStaff__sStaf__5AEE82B9");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__EDF80BCA76FABAD2");

            entity.ToTable("Shipping");

            entity.Property(e => e.ShippingId).HasColumnName("shippingId");
            entity.Property(e => e.DeliveryStaffId).HasColumnName("deliveryStaffId");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.SaleStaffId).HasColumnName("saleStaffId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.DeliveryStaff).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.DeliveryStaffId)
                .HasConstraintName("FK__Shipping__delive__7E37BEF6");

            entity.HasOne(d => d.Order).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Shipping__orderI__7C4F7684");

            entity.HasOne(d => d.SaleStaff).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.SaleStaffId)
                .HasConstraintName("FK__Shipping__saleSt__7D439ABD");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.ShippingMethodId).HasName("PK__Shipping__E405E856BF3B5182");

            entity.ToTable("ShippingMethod");

            entity.Property(e => e.ShippingMethodId).HasColumnName("shippingMethodId");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MethodName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("methodName");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__55B1E557085F14F4");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("sizeId");
            entity.Property(e => e.SizeName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sizeName");
            entity.Property(e => e.SizePrice)
                .HasColumnType("money")
                .HasColumnName("sizePrice");
            entity.Property(e => e.SizeValue)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sizeValue");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId).HasName("PK__SubCateg__F82064697BE54A85");

            entity.ToTable("SubCategory");

            entity.Property(e => e.SubCategoryId).HasColumnName("subCategoryId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.SubCategoryName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subCategoryName");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__SubCatego__categ__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFF38E1A418");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__F53389E954CFDA98");

            entity.ToTable("Voucher");

            entity.Property(e => e.VoucherId).HasColumnName("voucherId");
            entity.Property(e => e.CusId).HasColumnName("cusId");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ExpDate).HasColumnName("expDate");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("rate");

            entity.HasOne(d => d.CusNavigation).WithMany(p => p.VouchersNavigation)
                .HasForeignKey(d => d.CusId)
                .HasConstraintName("FK__Voucher__cusId__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}