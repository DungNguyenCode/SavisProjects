using Data.Interface;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class SavisServices<T> : ISavis<T>
    {
        public Task<bool> Add(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
