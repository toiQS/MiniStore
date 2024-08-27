namespace MiniStore.API.Models.item
{
    public class ItemModelResponse
    {
        
        public string ItemId { get; set; } = string.Empty;
        
        public string ItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public bool Status { get; set; }
        
        public string StyleItemId { get; set; } = string.Empty;
        
    }
}
