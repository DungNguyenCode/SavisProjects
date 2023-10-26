using Data.ContextDbSavis;
using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class RoleService : IAllinterface<Role>
    {
        private readonly ContextDb _dbContext;

        public RoleService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Role item)
        {
            if (item != null)
            {
                await _dbContext.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var acc = _dbContext.Roles.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role> GetById(Guid Id)
        {
            var temp = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Role item)
        {
            var temp = _dbContext.Roles.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.Code = item.Code;
                temp.Name = item.Name;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
