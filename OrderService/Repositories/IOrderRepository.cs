using OrderService.Models;
using OrderService.Notifications;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task AddOrderAsync(Order order);
    void RegisterObserver(INotificationObserver observer);
}