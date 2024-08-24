using Microsoft.EntityFrameworkCore;
using MiniStore.Models;

namespace MiniStore.Data
{
    public class SeedingData : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=AKAI;Database=Noname;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;User Id = akai;Password=Akai123");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Shift
            builder.Entity<Shift>().HasData(
                new Shift
                {
                    ShiftId = "S1",
                    ShiftName = "Morning",
                    //Calenders = new List<Calender>()
                    //{
                    //    new Calender { CalenderId = "C1", DayOfWeek = DayOfWeek.Monday, StartAt = new TimeOnly(8, 0), EndAt = new TimeOnly(16, 0)}
                    //}
                },
                new Shift
                {
                    ShiftId = "S2",
                    ShiftName = "Afternoon",
                    //Calenders = new List<Calender>()
                    //{
                    //    new Calender { CalenderId = "C2", DayOfWeek = DayOfWeek.Tuesday, StartAt = new TimeOnly(14, 0), EndAt = new TimeOnly(22, 0) }
                    //}
                },
                new Shift
                {
                    ShiftId = "S3",
                    ShiftName = "Evening",
                    
                    //Calenders = new List<Calender>()
                    //{
                    //    new Calender { CalenderId = "C3", DayOfWeek = DayOfWeek.Wednesday, StartAt = new TimeOnly(8, 0), EndAt = new TimeOnly(16, 0) }
                    //}
                }
            );

            // Seeding Calendar
            builder.Entity<Calender>().HasData(
                new Calender { CalenderId = "C1", DayOfWeek = DayOfWeek.Monday, StartAt = new TimeOnly(8, 0), EndAt = new TimeOnly(16, 0)},
                new Calender { CalenderId = "C2", DayOfWeek = DayOfWeek.Tuesday, StartAt = new TimeOnly(14, 0), EndAt = new TimeOnly(22, 0)},
                new Calender { CalenderId = "C3", DayOfWeek = DayOfWeek.Wednesday, StartAt = new TimeOnly(8, 0), EndAt = new TimeOnly(16, 0) }
            );

            // Seeding StyleItem
            builder.Entity<StyleItem>().HasData(
                new StyleItem { StyleItemId = "ST1", StyleItemName = "Clothes", StyleItemDescription = "Various clothes", Status = true },
                new StyleItem { StyleItemId = "ST2", StyleItemName = "Electronics", StyleItemDescription = "Electronic items", Status = true }
            );

            // Seeding Item
            builder.Entity<Item>().HasData(
                new Item { ItemId = "I1", ItemName = "T-Shirt", Quantity = 50, Status = true, StyleItemId = "ST1" },
                new Item { ItemId = "I2", ItemName = "Laptop", Quantity = 10, Status = true, StyleItemId = "ST2" }
            );

            // Seeding Customer
            builder.Entity<Customer>().HasData(
                new Customer { CustomerId = "C1", CustomerName = "John Doe", Phone = "0123456789", Status = true },
                new Customer { CustomerId = "C2", CustomerName = "Jane Doe", Phone = "0987654321", Status = true }
            );

            // Seeding Supplier
            builder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = "SP1", SupplierName = "Supplier A", SupplierPhone = "0123456789", SupplierEmail = "a@supplier.com", SupplierAddress = "Address A", Status = true },
                new Supplier { SupplierId = "SP2", SupplierName = "Supplier B", SupplierPhone = "0987654321", SupplierEmail = "b@supplier.com", SupplierAddress = "Address B", Status = true }
            );

            // Seeding Coupon
            builder.Entity<Coupon>().HasData(
                new Coupon { CouponId = "CP1", CouponName = "Discount 10%", CouponDescription = "10% off on all items", StyleItemId = "ST1", Value = 10, Unit = "%", StartAt = DateTime.Now.AddDays(-10), EndAt = DateTime.Now.AddDays(10) },
                new Coupon { CouponId = "CP2", CouponName = "Discount 20%", CouponDescription = "20% off on electronics", StyleItemId = "ST2", Value = 20, Unit = "%", StartAt = DateTime.Now.AddDays(-10), EndAt = DateTime.Now.AddDays(10) }
            );

            // Seeding Employee
            builder.Entity<Employee>().HasData(
                new Employee { EmployeeId = "E1", FirstName = "Alice", LastName = "Smith", MiddleName = "B", EmployeeName = "Alice B Smith", CCCD = 123456789, EmloyeeEmail = "alice@example.com", Phone = "1234567890", ShiftId = "S1", Status = true },
                new Employee { EmployeeId = "E2", FirstName = "Bob", LastName = "Johnson", MiddleName = "C", EmployeeName = "Bob C Johnson", CCCD = 987654321, EmloyeeEmail = "bob@example.com", Phone = "0987654321", ShiftId = "S2", Status = true }
            );

            // Seeding Invoice
            builder.Entity<Invoice>().HasData(
                new Invoice { InvoiceId = "INV1", EmployeeId = "E1", CustomerId = "C1", CouponId = "CP1" },
                new Invoice { InvoiceId = "INV2", EmployeeId = "E2", CustomerId = "C2", CouponId = "CP2" }
            );

            // Seeding InvoiceDetail
            builder.Entity<InvoiceDetail>().HasData(
                new InvoiceDetail { InvoiceDetailId = "ID1", ItemId = "I1", InvoiceId = "INV1", Quantity = 2 },
                new InvoiceDetail { InvoiceDetailId = "ID2", ItemId = "I2", InvoiceId = "INV2", Quantity = 1 }
            );

            // Seeding Receipt
            builder.Entity<Receipt>().HasData(
                new Receipt { ReceiptId = "R1", SupplierID = "SP1", CreateAt = DateTime.Now.AddDays(-5) },
                new Receipt { ReceiptId = "R2", SupplierID = "SP2", CreateAt = DateTime.Now.AddDays(-3) }
            );

            // Seeding ReceiptDetail
            builder.Entity<ReceiptDetail>().HasData(
                new ReceiptDetail { ReceiptDetailId = "RD1", ReceiptId = "R1", ItemId = "I1" },
                new ReceiptDetail { ReceiptDetailId = "RD2", ReceiptId = "R2", ItemId = "I2" }
            );
        }
    }
}
