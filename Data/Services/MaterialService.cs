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
    public class MaterialService :IAllinterface<Material>
    {
        private readonly ContextDb _dbContext;

        public MaterialService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Material item)
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
            var acc = _dbContext.Materials.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Material>> GetAll()
        {
            return await _dbContext.Materials.ToListAsync();
        }

        public async Task<Material> GetById(Guid Id)
        {
            var temp = await _dbContext.Materials.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Material item)
        {
            var temp = _dbContext.Materials.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
              temp.Status = item.Status;
                temp.Name = item.Name;
                temp.Last_modified_date = DateTime.Now;
                temp.Status = temp.Status;              
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
