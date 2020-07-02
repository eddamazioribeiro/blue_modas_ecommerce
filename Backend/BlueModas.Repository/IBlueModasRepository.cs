using System.Threading.Tasks;
using BlueModas.Domain;

namespace BlueModas.Repository
{
    public interface IBlueModasRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();
    }
}