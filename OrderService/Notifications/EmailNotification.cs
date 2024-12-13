using OrderService.Models;

namespace OrderService.Notifications;

public class EmailNotification : INotificationObserver
{
    public async Task Add(Order order)
    {
        try
        {
            // Simulate email notification logic with an async delay
            await Task.Delay(500); // Simulates async email sending
            Console.WriteLine($"Email Notification: New Order added - Order ID: {order.OrderId}, Total Amount: {order.TotalAmount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email Notification Failed: {ex.Message}");
            throw;
        }
    }

}