using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ISavis<T> 
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(Guid Id);
    }
}
