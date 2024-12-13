namespace OrderService.Models;

public class OrderItem
{
    public int OrderItemId { get; set; } // Changed to int
    public int OrderId { get; set; } // Changed to int
    public int BookId { get; set; } // Changed to int
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }

}