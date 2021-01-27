using LoadLink.LoadMatching.Application.Persistence;
using System;
using System.Threading.Tasks;

namespace LoadLink.LoadMatching.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
