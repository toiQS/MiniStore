using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.repository
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsync(int id);
        public Task<bool> Add(T entity);
        public Task<bool> Delete(string id);
        public Task<bool> Update(string id, T entity); 
    }
}
