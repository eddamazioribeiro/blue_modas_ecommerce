using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class ProductRepository : IBlueModasRepository
    {
        private readonly BlueModasContext _context;

        public ProductRepository(BlueModasContext context)
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

        public async Task<object> GetByIdAsync(int id, bool includeChildren = false)
        {
            IQueryable<Product> query = _context.Products;
            
            query = query.Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product[]> GetAllProducts()
        {
            IQueryable<Product> query = _context.Products;

            query = query.OrderByDescending(p => p.IncludedAt);

            return await query.ToArrayAsync();
        }
    }
}