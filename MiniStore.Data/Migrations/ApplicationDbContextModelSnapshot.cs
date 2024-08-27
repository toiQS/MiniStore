﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniStore.Data;

#nullable disable

namespace MiniStore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniStore.Models.Calender", b =>
                {
                    b.Property<string>("CalenderId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Calender Id");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int")
                        .HasColumnName("Day of week");

                    b.Property<TimeOnly>("EndAt")
                        .HasColumnType("time")
                        .HasColumnName("End At");

                    b.Property<string>("ShiftId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeOnly>("StartAt")
                        .HasColumnType("time")
                        .HasColumnName("Stat At");

                    b.HasKey("CalenderId");

                    b.HasIndex("ShiftId");

                    b.ToTable("Calender");
                });

            modelBuilder.Entity("MiniStore.Models.Coupon", b =>
                {
                    b.Property<string>("CouponId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Coupon Id");

                    b.Property<string>("CouponDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Coupon Description");

                    b.Property<string>("CouponName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Coupon Name");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("End At");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Start At");

                    b.Property<string>("StyleItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Style Item Id");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("CouponId");

                    b.HasIndex("StyleItemId");

                    b.ToTable("Coupon");
                });

            modelBuilder.Entity("MiniStore.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Customer Id");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Customer Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("MiniStore.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Employee Id");

                    b.Property<int>("CCCD")
                        .HasColumnType("int");

                    b.Property<string>("EmloyeeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Employee Email");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Employee Name");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("First Name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Last Name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Middle Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShiftId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Shift Id");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ShiftId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("MiniStore.Models.Invoice", b =>
                {
                    b.Property<string>("InvoiceId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Invoice Id");

                    b.Property<string>("CouponId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Coupon Id");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Customer Id");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Employee Id");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CouponId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("MiniStore.Models.InvoiceDetail", b =>
                {
                    b.Property<string>("InvoiceDetailId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Invoice Detail Id");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Invoice Id");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Item Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("InvoiceDetailId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("Invoice Detail");
                });

            modelBuilder.Entity("MiniStore.Models.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Item Id");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Item Name");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StyleItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Style Item Id");

                    b.HasKey("ItemId");

                    b.HasIndex("StyleItemId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("MiniStore.Models.Receipt", b =>
                {
                    b.Property<string>("ReceiptId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Receipt Id");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Create At");

                    b.Property<string>("SupplierID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Supplier Id");

                    b.HasKey("ReceiptId");

                    b.HasIndex("SupplierID");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("MiniStore.Models.ReceiptDetail", b =>
                {
                    b.Property<string>("ReceiptDetailId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Receipt Detail Id");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Item Id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("ReceiptId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Receipt Id");

                    b.HasKey("ReceiptDetailId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Receipt Detail");
                });

            modelBuilder.Entity("MiniStore.Models.Shift", b =>
                {
                    b.Property<string>("ShiftId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Shift Id");

                    b.Property<string>("ShiftName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Shift Name ");

                    b.HasKey("ShiftId");

                    b.ToTable("Shift");
                });

            modelBuilder.Entity("MiniStore.Models.StyleItem", b =>
                {
                    b.Property<string>("StyleItemId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Style Item Id");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StyleItemDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Style Item Description");

                    b.Property<string>("StyleItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Style Item Name");

                    b.HasKey("StyleItemId");

                    b.ToTable("Style Item");
                });

            modelBuilder.Entity("MiniStore.Models.Supplier", b =>
                {
                    b.Property<string>("SupplierId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Supplier Id");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Supplier Status");

                    b.Property<string>("SupplierAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Suppler Address");

                    b.Property<string>("SupplierEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Supplier Email");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Supplier Name");

                    b.Property<string>("SupplierPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Supplier Phone");

                    b.HasKey("SupplierId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("MiniStore.Models.Calender", b =>
                {
                    b.HasOne("MiniStore.Models.Shift", null)
                        .WithMany("Calenders")
                        .HasForeignKey("ShiftId");
                });

            modelBuilder.Entity("MiniStore.Models.Coupon", b =>
                {
                    b.HasOne("MiniStore.Models.StyleItem", "StyleItem")
                        .WithMany()
                        .HasForeignKey("StyleItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StyleItem");
                });

            modelBuilder.Entity("MiniStore.Models.Employee", b =>
                {
                    b.HasOne("MiniStore.Models.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("MiniStore.Models.Invoice", b =>
                {
                    b.HasOne("MiniStore.Models.Coupon", "Coupon")
                        .WithMany()
                        .HasForeignKey("CouponId");

                    b.HasOne("MiniStore.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniStore.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coupon");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MiniStore.Models.InvoiceDetail", b =>
                {
                    b.HasOne("MiniStore.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniStore.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("MiniStore.Models.Item", b =>
                {
                    b.HasOne("MiniStore.Models.StyleItem", "StyleItem")
                        .WithMany()
                        .HasForeignKey("StyleItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StyleItem");
                });

            modelBuilder.Entity("MiniStore.Models.Receipt", b =>
                {
                    b.HasOne("MiniStore.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MiniStore.Models.ReceiptDetail", b =>
                {
                    b.HasOne("MiniStore.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniStore.Models.Receipt", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("MiniStore.Models.Shift", b =>
                {
                    b.Navigation("Calenders");
                });
#pragma warning restore 612, 618
        }
    }
}
