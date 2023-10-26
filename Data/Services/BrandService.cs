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
    public class BrandService : IAllinterface<Brand>
    {
        private readonly ContextDb _dbContext;

        public BrandService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Brand item)
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
            var acc = _dbContext.Brands.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetById(Guid Id)
        {
            var temp = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Brand item)
        {
            var temp = _dbContext.Brands.FirstOrDefault(a => a.Id == item.Id);

            if (temp != null)
            {
                temp.Status = item.Status;
                temp.Name = item.Name;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
