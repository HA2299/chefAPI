using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddItemAsync(T item);
        Task UpdateItemAsync(int id,T item);
        Task DeleteItemAsync(int id);
        Task<T> GetByNameAsync(string name)=>default;
    }
}
