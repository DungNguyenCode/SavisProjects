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
    public class VorcherService:IAllinterface<Vorcher>
    {
        private readonly ContextDb _dbContext;

        public VorcherService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Vorcher item)
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
            var acc = _dbContext.Vorchers.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Vorcher>> GetAll()
        {
            return await _dbContext.Vorchers.ToListAsync();
        }

        public async Task<Vorcher> GetById(Guid Id)
        {
            var temp = await _dbContext.Vorchers.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Vorcher item)
        {
            var temp = _dbContext.Vorchers.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
                temp.StartDate = item.StartDate;
                temp.EndDate= item.EndDate;
                temp.Status = item.Status;
                temp.Code = item.Code;
                temp.Last_modified_date = DateTime.Now;
                temp.Quantity = item.Quantity;
                temp.Value = item.Value;
                temp.Name = item.Name;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
