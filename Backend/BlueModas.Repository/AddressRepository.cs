using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class AddressRepository : IBlueModasRepository
    {
        private readonly BlueModasContext _context;

        public AddressRepository(BlueModasContext context)
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

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            IQueryable<Address> query = _context.Addresses;
            
            query = query.Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }           

        public async Task<Address[]> GetUserAddressesAsync(int userId)
        {
            IQueryable<Address> query = _context.Addresses;
            
            query = query.Where(a => a.UserId == userId);

            return await query.ToArrayAsync();
        }

        public async Task<Address> GetUserMainAddressAsync(int userId)
        {
            IQueryable<Address> query = _context.Addresses;

            query = query
                .Where(a => a.UserId == userId)
                .Where(a => a.MainAddress == true);

            return await query.FirstOrDefaultAsync();
        }
    }
}