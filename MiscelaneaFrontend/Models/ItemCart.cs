namespace MiscelaneaFrontend.Models
{
    public class ItemCart
    {
        public long IdItemCart { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal PriceUnit { get; set; }
        public decimal Subtotal { get; set; }
    }
}
