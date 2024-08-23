using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Data
{
    public class SeedingData : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=AKAI;Database=Noname;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;User Id = akai;Password=Akai123");


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //    modelBuilder.Entity<Shift>().HasData(
            //        new Shift
            //        {
            //            ShiftId = "full-time",
            //            ShiftName = "Full Time",
            //            Calenders = new List<Calender>
            //            {
            //                new Calender()
            //                {
            //                    CalenderId = "full-time-1",
            //                    DayOfWeek = DayOfWeek.Monday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //                , new Calender()
            //                {
            //                    CalenderId = "full-time-2",
            //                    DayOfWeek = DayOfWeek.Tuesday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //                , new Calender()
            //                {
            //                    CalenderId = "full-time-3",
            //                    DayOfWeek = DayOfWeek.Wednesday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //                , new Calender()
            //                {
            //                    CalenderId = "full-time-4",
            //                    DayOfWeek = DayOfWeek.Thursday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //                , new Calender()
            //                {
            //                    CalenderId = "full-time-17",
            //                    DayOfWeek = DayOfWeek.Friday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //                , new Calender()
            //                {
            //                    CalenderId = "full-time-6",
            //                    DayOfWeek = DayOfWeek.Saturday,
            //                    EndAt = new TimeOnly(17, 30),
            //                    StartAt = new TimeOnly(8, 00)
            //                }
            //            }
            //        },
            //        new Shift
            //        {
            //            ShiftId = "shift-case-1",
            //            ShiftName = "Full Morning Shift",
            //            Calenders = new List<Calender>
            //            {
            //                new Calender()
            //                {
            //                    CalenderId = "full-morning-case-1",
            //                    DayOfWeek = DayOfWeek.Monday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-morning-case-2",
            //                    DayOfWeek = DayOfWeek.Tuesday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-morning-case-3",
            //                    DayOfWeek = DayOfWeek.Wednesday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-morning-case-4",
            //                    DayOfWeek = DayOfWeek.Thursday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-morning-case-5",
            //                    DayOfWeek = DayOfWeek.Friday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-morning-case-6",
            //                    DayOfWeek = DayOfWeek.Saturday,
            //                    StartAt = new TimeOnly(7,00),
            //                    EndAt = new TimeOnly(12,00)
            //                }
            //            }
            //        },
            //        new Shift
            //        {
            //            ShiftId = "shift-case-2",
            //            ShiftName = "Full afternoon shift",
            //            Calenders = new List<Calender>
            //            {
            //                new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-1",
            //                    DayOfWeek = DayOfWeek.Monday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-2",
            //                    DayOfWeek = DayOfWeek.Tuesday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-3",
            //                    DayOfWeek = DayOfWeek.Wednesday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-4",
            //                    DayOfWeek = DayOfWeek.Thursday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-5",
            //                    DayOfWeek = DayOfWeek.Friday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //                ,new Calender()
            //                {
            //                    CalenderId = "full-afternoon-case-6",
            //                    DayOfWeek = DayOfWeek.Saturday,
            //                    StartAt = new TimeOnly(12,00),
            //                    EndAt = new TimeOnly(17,00)
            //                }
            //            }
            //        },
            //        new Shift
            //        {
            //            ShiftId = "shift-case-3",
            //            ShiftName = "Full Evening Shift",
            //            Calenders = new List<Calender>
            //            {
            //                new Calender()
            //                {
            //                    CalenderId = "full-evening-case-1",
            //                    DayOfWeek = DayOfWeek.Monday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },new Calender()
            //                {
            //                    CalenderId = "full-evening-case-2",
            //                    DayOfWeek = DayOfWeek.Tuesday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },new Calender()
            //                {
            //                    CalenderId = "full-evening-case-3",
            //                    DayOfWeek = DayOfWeek.Wednesday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },new Calender()
            //                {
            //                    CalenderId = "full-evening-case-4",
            //                    DayOfWeek = DayOfWeek.Thursday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },new Calender()
            //                {
            //                    CalenderId = "full-evening-case-5",
            //                    DayOfWeek = DayOfWeek.Friday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },new Calender()
            //                {
            //                    CalenderId = "full-evening-case-5",
            //                    DayOfWeek = DayOfWeek.Saturday,
            //                    StartAt = new TimeOnly(17,00),
            //                    EndAt = new TimeOnly(22,00)
            //                },
            //            }
            //        });
            //    modelBuilder.Entity<Supplier>().HasData(
            //        new Supplier()
            //        {
            //            SupplierId = "DA",
            //            SupplierName = "Đông Á",
            //            Status = true,
            //            SupplierAddress = "Ho Chi Minh City",
            //            SupplierEmail = "donga@donga.comunity.com",
            //            SupplierPhone = "1234567890"
            //        },
            //        new Supplier()
            //        {
            //            SupplierId = "VNG",
            //            SupplierName = "Đông Lào",
            //            Status = true,
            //            SupplierPhone = "0123456789",
            //            SupplierAddress = "Ho Chi Minh City",
            //            SupplierEmail = "donglao@vng.com"
            //        }
            //        );
            //    modelBuilder.Entity<Receipt>().HasData(
            //        new Receipt()
            //        {
            //            CreateAt = DateTime.Now,
            //            ReceiptId = $"NH-{DateTime.Now}",
            //            SupplierID = "DA"
            //        },
            //        new Receipt()
            //        {
            //            SupplierID = "VNG",
            //            CreateAt = DateTime.Now,
            //            ReceiptId = $"NH-{DateTime.Now}"
            //        },
            //        new Receipt()
            //        {
            //            ReceiptId = $"XH-{DateTime.Now}",
            //            SupplierID = "VNG",
            //            CreateAt= DateTime.Now.AddDays(12)
            //        });
            //    modelBuilder.Entity<StyleItem>().HasData(
            //        new StyleItem()
            //        {
            //            StyleItemId = "style-item-1",
            //            StyleItemName ="F&D",
            //            Status = true,
            //            StyleItemDescription = "Nothing"
            //        },
            //        new StyleItem()
            //        {
            //            StyleItemId = "style-item-2",
            //            StyleItemName = "Necessities",
            //            Status = true,
            //            StyleItemDescription= "Nothing"
            //        },
            //        new StyleItem()
            //        {
            //            StyleItemId = "style-item-3",
            //            StyleItemName = "Personal Items",
            //            Status = true,
            //            StyleItemDescription = "Nothing"
            //        },
            //        new StyleItem()
            //        {
            //            StyleItemId ="style-item-4",
            //            StyleItemName = "newspager",
            //            Status= true,
            //            StyleItemDescription = "Nothing"
            //        });
            //    modelBuilder.Entity<Item>().HasData(
            //        new Item()
            //        {
            //            ItemId = "item-1",
            //            ItemName = "Banana",
            //            Quantity = 100,
            //            Status = true,
            //            StyleItemId = ""
            //        }); 

            //}

            //Seeding data for StyleItems

           modelBuilder.Entity<StyleItem>().ToTable("Style Item").HasData(
               new StyleItem { StyleItemId = "SI001", StyleItemName = "Fruit", StyleItemDescription = "Fresh Fruits", Status = true },
               new StyleItem { StyleItemId = "SI002", StyleItemName = "Vegetable", StyleItemDescription = "Organic Vegetables", Status = true }
           );

           // Seeding data for Customers
           modelBuilder.Entity<Customer>().ToTable("Customer").HasData(
                new Customer { CustomerId = "C001", CustomerName = "John Doe", Phone = "0909123456", Status = true },
                new Customer { CustomerId = "C002", CustomerName = "Jane Smith", Phone = "0909876543", Status = true },
                new Customer { CustomerId = "C003", CustomerName = "Tom Brown", Phone = "0912345678", Status = false }
            );

            // Seeding data for Employees
            modelBuilder.Entity<Employee>().ToTable("Employee").HasData(
                new Employee { EmployeeId = "E001", FirstName = "Alice", LastName = "Johnson", MiddleName = "M", EmployeeName = "Alice M Johnson", CCCD = 123456789, EmloyeeEmail = "alice@example.com", Phone = "0911222333", CalenderId = "CL001", Status = true },
                new Employee { EmployeeId = "E002", FirstName = "Bob", LastName = "Williams", MiddleName = "J", EmployeeName = "Bob J Williams", CCCD = 987654321, EmloyeeEmail = "bob@example.com", Phone = "0911333444", CalenderId = "CL002", Status = true }
            );

            // Seeding data for Items
            modelBuilder.Entity<Item>().ToTable("Item").HasData(
                new Item { ItemId = "I001", ItemName = "Apple", Quantity = 100, Status = true, StyleItemId = "SI001" },
                new Item { ItemId = "I002", ItemName = "Banana", Quantity = 150, Status = true, StyleItemId = "SI002" },
                new Item { ItemId = "I003", ItemName = "Orange", Quantity = 200, Status = false, StyleItemId = "SI003" }
            );

            // Seeding data for Coupons
            modelBuilder.Entity<Coupon>().ToTable("Coupon").HasData(
                new Coupon { CouponId = "CP001", CouponName = "Discount10", CouponDescription = "10% off on all items", ApplyToItem = "I001", Value = 10.0f, Unit = "%", StartAt = DateTime.Now, EndAt = DateTime.Now.AddMonths(1) },
                new Coupon { CouponId = "CP002", CouponName = "Discount20", CouponDescription = "20% off on selected items", ApplyToItem = "I002", Value = 20.0f, Unit = "%", StartAt = DateTime.Now, EndAt = DateTime.Now.AddMonths(2) }
            );

            // Seeding data for Suppliers
            modelBuilder.Entity<Supplier>().ToTable("Supplier").HasData(
                new Supplier { SupplierId = "S001", SupplierName = "Fresh Produce Inc.", SupplierPhone = "0909888777", SupplierEmail = "freshproduce@example.com", SupplierAddress = "123 Fruit St.", Status = true },
                new Supplier { SupplierId = "S002", SupplierName = "Organic Farms", SupplierPhone = "0909777666", SupplierEmail = "organicfarms@example.com", SupplierAddress = "456 Vegetable Ave.", Status = true }
            );

            // Seeding data for Shifts
            modelBuilder.Entity<Shift>().ToTable("Shift").HasData(
                new Shift { ShiftId = "SH001", ShiftName = "Morning Shift" },
                new Shift { ShiftId = "SH002", ShiftName = "Evening Shift" }
            );

            // Seeding data for Calendars
            modelBuilder.Entity<Calender>().ToTable("Calender").HasData(
                new Calender { CalenderId = "CL001", DayOfWeek = DayOfWeek.Monday, StartAt = new TimeOnly(9, 0), EndAt = new TimeOnly(17, 0) },
                new Calender { CalenderId = "CL002", DayOfWeek = DayOfWeek.Tuesday, StartAt = new TimeOnly(10, 0), EndAt = new TimeOnly(18, 0) }
            );

            // Seeding data for Invoices
            modelBuilder.Entity<Invoice>().ToTable("Invoice").HasData(
                new Invoice { InvoiceId = "INV001", EmployeeId = "E001", CouponId = "CP001" },
                new Invoice { InvoiceId = "INV002", EmployeeId = "E002", CouponId = "CP002" }
            );

            // Seeding data for InvoiceDetails
            modelBuilder.Entity<InvoiceDetail>().ToTable("Invoice Detail").HasData(
                new InvoiceDetail { InvoiceDetailId = "ID001", ItemId = "I001", InvoiceId = "INV001", Quantity = 10 },
                new InvoiceDetail { InvoiceDetailId = "ID002", ItemId = "I002", InvoiceId = "INV002", Quantity = 20 }
            );

            // Seeding data for Receipts
            modelBuilder.Entity<Receipt>().ToTable("Receipt").HasData(
                new Receipt { ReceiptId = "R001", SupplierID = "S001", CreateAt = DateTime.Now },
                new Receipt { ReceiptId = "R002", SupplierID = "S002", CreateAt = DateTime.Now }
            );

            // Seeding data for ReceiptDetails
            modelBuilder.Entity<ReceiptDetail>().ToTable("Receipt Detail").HasData(
                new ReceiptDetail { ReceiptDetailId = "RD001", ReceiptId = "R001", ItemId = "I001" },
                new ReceiptDetail { ReceiptDetailId = "RD002", ReceiptId = "R002", ItemId = "I002" }
            );

            
        }
    }
}
