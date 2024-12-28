﻿// <auto-generated />
using System;
using Ecommerce_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce_BE.Migrations
{
    [DbContext(typeof(DbEContext))]
    [Migration("20241226061722_Add_Product_Category_Store_UserProductRate")]
    partial class Add_Product_Category_Store_UserProductRate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce_BE.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostPrice")
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<int>("IdStore")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Quantity")
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<int>("SellingPrice")
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdStore");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isSeller")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.UserProductRate", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("UserProductRate");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Product", b =>
                {
                    b.HasOne("Ecommerce_BE.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce_BE.Models.Store", "Store")
                        .WithMany("Products")
                        .HasForeignKey("IdStore")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Store", b =>
                {
                    b.HasOne("Ecommerce_BE.Models.User", "User")
                        .WithOne("Store")
                        .HasForeignKey("Ecommerce_BE.Models.Store", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.UserProductRate", b =>
                {
                    b.HasOne("Ecommerce_BE.Models.User", "ProductUser")
                        .WithMany("ProductRates")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce_BE.Models.Product", "Product")
                        .WithMany("ProductRates")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductUser");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Product", b =>
                {
                    b.Navigation("ProductRates");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.Store", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce_BE.Models.User", b =>
                {
                    b.Navigation("ProductRates");

                    b.Navigation("Store")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
