using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniStore.Services.repository
{
    public interface IRepository<T> where T : class
    {
        public string GetPath(string folderName, string fileName); 
        public Task<IEnumerable<T>> GetAll();         
        
        public Task<bool> Add(T entity);               
        public Task<bool> Update(T entity);            
        public Task<bool> Delete(T entity);            
        
    }

}
