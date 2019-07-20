using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FootballClubs.Models;
using FootballClubs.Repositories.Interfaces;

namespace FootballClubs.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        IQueryable<T> IRepository<T>.GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
