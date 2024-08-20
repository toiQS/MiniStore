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
        }
    }
}
