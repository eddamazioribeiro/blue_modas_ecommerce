using System.Threading.Tasks;
using BlueModas.Domain;

namespace BlueModas.Repository
{
    public interface IBlueModasRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();
         Task<object[]> GetByIdAsync(int id, bool includeChildren);
         Task<object[]> GetByNameAsync(string name, bool includeChildren);
         Task<object[]> GetAllAsync(bool includeChildren);
    }
}