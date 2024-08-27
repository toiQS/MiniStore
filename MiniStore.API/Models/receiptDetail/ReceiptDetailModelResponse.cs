namespace MiniStore.API.Models.receiptDetail
{
    public class ReceiptDetailModelResponse
    {
        public string ReceiptDetailId { get; set; } = string.Empty;
        public string ReceiptId { get; set; } = string.Empty;
        public string ItemId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
