using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using OrderService.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly OrderSubject _subject;

        public OrderRepository(OrderDbContext context, OrderSubject subject)
        {
            _context = context;
            _subject = subject;
        }

        public void RegisterObserver(INotificationObserver observer)
        {
            _subject.Attach(observer);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }


        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await _subject.NotifyAsync(order);
        }

    }
}