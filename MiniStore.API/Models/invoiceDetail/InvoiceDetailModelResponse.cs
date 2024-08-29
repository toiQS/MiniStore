namespace MiniStore.API.Models.invoiceDetail
{
    public class InvoiceDetailModelResponse
    {
        public string InvoiceDetailId { get; set; } = string.Empty; 
        public string ItemId { get; set; } = string.Empty;
        public string InvoiceId { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
