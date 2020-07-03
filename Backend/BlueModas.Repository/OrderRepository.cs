using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class OrderRepository : IBlueModasRepository
    {
        private readonly BlueModasContext _context;

        public OrderRepository(BlueModasContext context)
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

        public async Task<Order> GetByIdAsync(int id, bool includeChildren = false)
        {
            IQueryable<Order> query = _context.Orders;

            if(includeChildren)
            {
                query = query.Include(i => i.Items);
            }
            
            query = query.Where(o => o.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Order[]> GetUseOrdersAsync(int userId, bool includeItems = true)
        {
            IQueryable<Order> query = _context.Orders;

            if(includeItems)
            {
                query = query.Include(i => i.Items);
            }

            query = query.OrderByDescending(o => o.UpdatedAt)
                .Where(o => o.UserId == userId);

            return await query.ToArrayAsync();
        }
    }
}