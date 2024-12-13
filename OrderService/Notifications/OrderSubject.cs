using OrderService.Models;

namespace OrderService.Notifications;

public class OrderSubject
{
    private readonly List<INotificationObserver> _observers = new();

    // Attach an observer
    public void Attach(INotificationObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            Console.WriteLine($"Observer attached: {observer.GetType().Name}");
        }
        else
        {
            Console.WriteLine($"Observer already attached: {observer.GetType().Name}");
        }
    }

    // Notify all observers
    public async Task NotifyAsync(Order order)
    {
        foreach (var observer in _observers)
        {
            try
            {
                await observer.Add(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error notifying observer: {ex.Message}");
            }
        }
    }
}