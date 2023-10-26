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
    public class OrderService:IAllinterface<Order>
    {
        private readonly ContextDb _dbContext;

        public OrderService(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Order item)
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
            var acc = _dbContext.Orders.FirstOrDefault(a => a.Id == Id);

            if (acc != null)
            {
                _dbContext.Remove(acc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetById(Guid Id)
        {
            var temp = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            try
            {
                return temp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Order item)
        {
            var temp = _dbContext.Orders.FirstOrDefault(a => a.Id == item.Id);
            if (temp != null)
            {
              temp.Address = item.Address;
                temp.PhoneNumber = item.PhoneNumber;
                temp.Transportfee = item.Transportfee;
                temp.Id_Account = item.Id_Account;
                temp.Status = item.Status;
                temp.TotalMoney = item.TotalMoney;
                temp.Last_modified_date = DateTime.Now;
                _dbContext.Update(temp);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
