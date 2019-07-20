using System.Linq;
using System.Threading.Tasks;
using FootballClubs.Models;

namespace FootballClubs.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        Task Update(int id, T entity);

        Task Delete(int id);

        void Save();
    }
}
