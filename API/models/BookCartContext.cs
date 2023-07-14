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

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=BookCart;User Id=postgres;Password=Pa55w0rd!");
//            }
//        }

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

                entity.Property(e => e.coverFileName)
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
