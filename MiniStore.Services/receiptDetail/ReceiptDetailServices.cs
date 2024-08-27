using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MiniStore.Data;
using MiniStore.Models;
using MiniStore.Services.repository;
using MiniStore.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.receiptDetail
{
    class ReceiptDetailServices: IReceiptDetailServices
    {
        private readonly ApplicationDbContext _context;
        private IRepository<ReceiptDetail> _receiptDetailRepository;
        private IRepository<Item> _itemRepository;
        private string _path;
        // constructor for api controller
        public ReceiptDetailServices(ApplicationDbContext context)
        {
            _context = context;
            _receiptDetailRepository = new Repository<ReceiptDetail>(context);
            _itemRepository = new Repository<Item>(context);
            _path = _receiptDetailRepository.GetPath("receipt detail", "LogReceiptDetailFile.txt");
        }
        // constructor for Unit Test
        public ReceiptDetailServices(ApplicationDbContext context, IRepository<ReceiptDetail> receiptDetailRepository, IRepository<Item> itemRepository)
        {
            _context = context;
            _receiptDetailRepository = receiptDetailRepository;
            _itemRepository = itemRepository;
            _path = _receiptDetailRepository.GetPath("receipt detail", "LogReceiptDetailFile.txt");
        }
        // fetch all data of receipt detail in database
        public async Task<IEnumerable<ReceiptDetail>> GetReceiptDetailsAsync()
        {
            return await _receiptDetailRepository.GetAll();
        }
        // fetch a data of receipt data specific by receipt detail id
        public async Task<ReceiptDetail> GetReceiptDetailAsync(string id)
        {
            try
            {
                var result = await _context.ReceiptDetail
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ReceiptDetailId == id);
                return result;
            }
            catch (Exception ex)
            {
                await LogErrorAsync(ex);
                throw;
            }
        }
        // add new a receipt detail
        public async Task<bool> AddReceipt(ReceiptDetail receiptDetail)
        {
            // add quantity of item when it was exist
            var IsItemExist = await _context.Item.AnyAsync(x => x.ItemId == receiptDetail.ItemId && x.Status == true);
            if (IsItemExist)
            {
                var result = await _receiptDetailRepository.Add(receiptDetail);
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == receiptDetail.ItemId);
                if (dataItem == null)
                {
                    return false;
                }
                dataItem.Quantity += receiptDetail.Quantity;
                await _itemRepository.Update(dataItem);
                return result;
            }
            return false;

           
        }
        // remove a receipt detail was existed in database
        public async Task<bool> RemoveReceipt(string id)
        {
            var data = await _context.ReceiptDetail
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.ReceiptDetailId == id);
            if (data == null) return false;
            var IsItemExist = await _context.Item.AnyAsync(x => x.ItemId == data.ItemId && x.Status == true);
            if (IsItemExist)
            {
                var result = await _receiptDetailRepository.Delete(data);
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == data.ItemId);
                if (dataItem == null)
                {
                    return false;
                }
                dataItem.Quantity -= data.Quantity;
                await _itemRepository.Update(dataItem);
                return result;
            }
            return false;
        }
        // update a receipt detail was existed in database

        public async Task<bool> UpdateReceipt(string id, int quantity)
        {
            // is data of old receipt detail
            // update item id and quanity if data of receipt is existed
            var data = await _context.ReceiptDetail
                   .AsNoTracking()
                   .FirstOrDefaultAsync(x => x.ReceiptDetailId == id);
            if (data == null) return false;


            // only update quantity of item
            
            var IsItemExist = await _context.Item.AnyAsync(x => x.ItemId == data.ItemId && x.Status == true);
            if (IsItemExist)
            {
                var result = await _receiptDetailRepository.Update(data);
                var dataItem = await _context.Item.FirstOrDefaultAsync(x => x.ItemId == data.ItemId);
                if (dataItem == null)
                {
                    return false;
                }
                // change based on the difference between new and old data
                dataItem.Quantity += (data.Quantity - quantity);
                await _itemRepository.Update(dataItem);
                return result;
            }
            return false;
        }
        private async Task LogErrorAsync(Exception ex)
        {
            var errorDetails = new StringBuilder();
            errorDetails.AppendLine($"Message: {ex.Message}\n");
            errorDetails.AppendLine($"Stack Trace: {ex.StackTrace}\n");
            errorDetails.AppendLine($"Source: {ex.Source}\n");
            errorDetails.AppendLine($"Time: {DateTime.Now}\n");

            if (!string.IsNullOrEmpty(_path))
            {
                await File.AppendAllTextAsync(_path, errorDetails.ToString());
            }
        }
    }
}
