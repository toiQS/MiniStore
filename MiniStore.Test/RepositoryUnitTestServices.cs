using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.receipt;
using MiniStore.Services.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Test
{
    public class RepositoryUnitTestServices
    {
        private readonly ApplicationDbContext _context;
        private IReceiptServices _receiptServices;
        private IRepository<Receipt> _repository;

        [SetUp]
        public void Setup() { 
            _receiptServices = new ReceiptServices(_context,_repository);
        }
        [TearDown]
        public void Teardown()
        {
            
        }
        [Test]
        public void Test()
        {
            var result = _receiptServices.GetReceiptsAsync();
            Console.WriteLine(result);
        }
    }
}
