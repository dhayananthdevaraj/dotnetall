using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        // Navigation Property: Product can be part of multiple orders
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
        //public List<Booking>? Bookings { get; set; }

    }
}
