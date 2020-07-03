using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class UserRepository : IBlueModasRepository
    {
        private readonly BlueModasContext _context;

        public UserRepository(BlueModasContext context)
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

        public async Task<User> GetUserByIdAsync(int id)
        {
            IQueryable<User> query = _context.Users;
            
            query = query.Where(a => a.Id == id);
            query = query.Include(a => a.Addresses);

            return await query.FirstOrDefaultAsync();
        }
    }
}