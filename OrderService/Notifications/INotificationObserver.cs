using OrderService.Models;

namespace OrderService.Notifications;

public interface INotificationObserver
{
    Task Add(Order order); // Async method to notify when a new order is added
}