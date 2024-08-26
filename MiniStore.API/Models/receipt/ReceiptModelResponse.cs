namespace MiniStore.API.Models.receipt
{
    public class ReceiptModelResponse
    {
        public string ReceiptId { set; get; } = string.Empty;
        public string SupplierID { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}
