namespace MiniStore.API.Models.styleItem
{
    public class StyleItemModelRequest
    {
        public string StyleItemName { get; set; } = string.Empty;
        public string StyleItemDescription { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
