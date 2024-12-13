namespace OrderService.Models;

public class Order
{
    public int OrderId { get; set; } // Changed to int
    public int UserId { get; set; } // Changed to int
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public double TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();

}