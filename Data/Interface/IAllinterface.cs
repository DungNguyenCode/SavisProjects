using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IAllinterface<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();       
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(Guid  Id);
    }
}
