namespace MiniStore.API.Models.receiptDetail
{
    public class ReceiptDetailModelRequest
    {
        public string ReceiptId { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
