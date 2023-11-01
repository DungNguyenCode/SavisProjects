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
    public class AddressService : IAllinterface<Address>
    {
        private readonly ContextDb _dbContext;

        public AddressService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Add(Address item)
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
            var acc = _dbContext.Addresses.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task<Address> GetById(Guid Id)
        {
            var temp = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Address item)
        {
            var temp = _dbContext.Addresses.FirstOrDefault(a => a.Id == item.Id);

            if (temp != null)
            {
                temp.User = item.User;
                temp.Wards = item.Wards;
                temp.City = item.City;
                temp.District = item.District;
                temp.Id_User = item.Id_User;
                temp.Detailed_address = item.Detailed_address;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
