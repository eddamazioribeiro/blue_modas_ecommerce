using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class OrderItemRepository : IBlueModasRepository
    {
        private readonly BlueModasContext _context;

        public OrderItemRepository(BlueModasContext context)
        {
            _context = context;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return(await _context.SaveChangesAsync()) > 0;
        }        

        public async Task<OrderItem[]> GetOrderItems(int orderId)
        {
            IQueryable<OrderItem> query = _context.OrderItems;
           
            query.Where(o => o.OrderId == orderId);

            return await query.ToArrayAsync();
        }
    }
}