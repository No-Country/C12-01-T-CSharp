using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using API.Models;

namespace API.models
{
    public partial class BookCartContext : DbContext
    {
        public BookCartContext()
        {
        }

        public BookCartContext(DbContextOptions<BookCartContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Cart> Cart { get; set; } = null!;
        public virtual DbSet<CartItems> CartItems { get; set; } = null!;
        public virtual DbSet<Categories> Categories { get; set; } = null!;
        public virtual DbSet<CustomerOrders> CustomerOrders { get; set; } = null!;
        public virtual DbSet<CustomerOrderDetails> CustomerOrderDetails { get; set; } = null!;
        public virtual DbSet<UserMaster> UserMaster { get; set; } = null!;
        public virtual DbSet<Usertype> UserType { get; set; } = null!;
        public virtual DbSet<Wishlist> Wishlist { get; set; } = null!;
        public virtual DbSet<WishlistItems> WishlistItems { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookId).HasColumnName("bookid");

                entity.Property(e => e.Author)
                    .HasMaxLength(100)
                    .HasColumnName("author");

                entity.Property(e => e.Category)
                    .HasMaxLength(20)
                    .HasColumnName("category");

                entity.Property(e => e.CoverFileName)
                    .HasMaxLength(100)
                    .HasColumnName("coverfilename");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.CartId)
                    .HasMaxLength(36)
                    .HasColumnName("cartid");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("datecreated");

                entity.Property(e => e.UserId).HasColumnName("userid");
            });

            modelBuilder.Entity<CartItems>(entity =>
            {
                entity.ToTable("cartitems");

                entity.Property(e => e.CartItemId).HasColumnName("cartitemid");

                entity.Property(e => e.CartId)
                    .HasMaxLength(36)
                    .HasColumnName("cartid");

                entity.Property(e => e.ProductId).HasColumnName("productid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId).HasColumnName("categoryid");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("categoryname");
            });

            modelBuilder.Entity<CustomerOrders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("customerorders_pkey");

                entity.ToTable("customerorders");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("orderid");

                entity.Property(e => e.CartTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("carttotal");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("datecreated");

                entity.Property(e => e.UserId).HasColumnName("userid");
            });

            modelBuilder.Entity<CustomerOrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId)
                    .HasName("customerorderdetails_pkey");

                entity.ToTable("customerorderdetails");

                entity.Property(e => e.OrderDetailsId).HasColumnName("orderdetailsid");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(20)
                    .HasColumnName("orderid");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("productid");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("usermaster_pkey");

                entity.ToTable("usermaster");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .HasMaxLength(40)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .HasColumnName("username");

                entity.Property(e => e.UserTypeId).HasColumnName("usertypeid");
            });

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.ToTable("usertype");

                entity.Property(e => e.UserTypeId).HasColumnName("usertypeid");

                entity.Property(e => e.UserTypeName)
                    .HasMaxLength(20)
                    .HasColumnName("usertypename");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("wishlist");

                entity.Property(e => e.WishlistId)
                    .HasMaxLength(36)
                    .HasColumnName("wishlistid");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("datecreated");

                entity.Property(e => e.UserId).HasColumnName("userid");
            });

            modelBuilder.Entity<WishlistItems>(entity =>
            {
                entity.ToTable("wishlistitems");

                entity.Property(e => e.WishlistItemId).HasColumnName("wishlistitemid");

                entity.Property(e => e.ProductId).HasColumnName("productid");

                entity.Property(e => e.WishlistId)
                    .HasMaxLength(36)
                    .HasColumnName("wishlistid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
